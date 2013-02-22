using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace SandBox.Connection
{
    public enum ConnectionClientEventType
    {
        CONNECTING,
        CONNECTED,
        RESETING,
        RESETED,
        DISCONNECTED,
        MESSAGE_RECEIVED,
        MESSAGE_SENDING,
        ERROR_OCCURED
    }//end ConnectionClientEventType enum

    public class ConnectionClientEventArgs : EventArgs
    {
        public ConnectionClientEventType EventType;
        public Object EventData;

        public ConnectionClientEventArgs(ConnectionClientEventType eventType, Object eventData)
        {
            EventType = eventType;
            EventData = eventData;
        }
    }//end ConnectionClientEventArgs class

    public class ConnectionClient : IDisposable
    {
        private static Socket   _clientSocket;
        private byte[]          _receivedBytes;
        private String          _remoteHost;
        private Int32           _remotePort;
        private Boolean         _reconnect;
        private IPEndPoint      _ipEndPoint;
        private Timer           _reconnectTimer;
        private Boolean         _needReset;
        private static readonly object ResetLocker = new object();

        public delegate void ConnectionClientEventHandler(ConnectionClientEventArgs args);
        public event ConnectionClientEventHandler OnConnectionClientEvent;

        public int IncomingMessageCount { get; private set; }
        public int OutgoingMessageCount { get; private set; }
        public Boolean IsConnected{ get; private set; }

        public void InvokeConnectionClientEvent(ConnectionClientEventArgs args)
        {
            ConnectionClientEventHandler handler = OnConnectionClientEvent;
            if (handler != null) handler(args);
        }

        public void Start(String remoteHost, Int32 remotePort, Boolean reconnect)
        {
            _remoteHost = remoteHost;
            _remotePort = remotePort;
            _reconnect  = reconnect;

            if (_reconnectTimer == null)
            {
                _reconnectTimer = new Timer(5000) { AutoReset = true };
                _reconnectTimer.Elapsed += ReconnectTimerElapsed;
            }

            IPAddress hostIpAddress;
            if (!IPAddress.TryParse(_remoteHost, out hostIpAddress))
            {
                InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.ERROR_OCCURED, "Invalid host IP address: " + _remoteHost));
                return;
            }
            _ipEndPoint = new IPEndPoint(hostIpAddress, _remotePort);

            Connect();
        }

        private void ReconnectTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _reconnectTimer.Stop();
            Connect();
        }

        public void Stop()
        {
            _reconnectTimer.Stop();
            ResetSocket(false);
        }

        private void Connect()
        {
            InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.CONNECTING, "Connecting to " + _remoteHost + ":" + _remotePort));
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.BeginConnect(_ipEndPoint, OnConnect, null);
            _needReset = true;
        }

        private void OnConnect(IAsyncResult ar)
        {
            try
            {
                if ((_clientSocket != null) && (_clientSocket.IsBound))
                {
                    _clientSocket.EndConnect(ar);
                    _clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

                    IsConnected = true;
                    InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.CONNECTED, "Connected to " + _remoteHost + ":" + _remotePort));

                    _receivedBytes = new byte[_clientSocket.ReceiveBufferSize];
                    _clientSocket.BeginReceive(_receivedBytes, 0, _receivedBytes.Length, SocketFlags.None, new AsyncCallback(OnReceive), _clientSocket);
                }
            }
            catch (ObjectDisposedException oex)
            {
                InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.ERROR_OCCURED, "Error during connection  (OnConnect, Disposed): " + oex.Message));
            }
            catch (SocketException ex)
            {
                InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.ERROR_OCCURED, "Error during connection -> " + ex.Message));
                ResetSocket(_reconnect);
            }
        }


        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                _clientSocket = (Socket)ar.AsyncState;
                int rLen = _clientSocket.EndReceive(ar);

                if (rLen > 0)
                {
                    foreach (var packet in PacketReceiver.GetPackets(_receivedBytes.Take(rLen).ToArray(), _clientSocket.ReceiveBufferSize))
                    {
                        IncomingMessageCount++;
                        InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.MESSAGE_RECEIVED, packet));
                    }
                    _receivedBytes = new byte[_clientSocket.ReceiveBufferSize];
                    _clientSocket.BeginReceive(_receivedBytes, 0, _receivedBytes.Length, SocketFlags.None, new AsyncCallback(OnReceive), _clientSocket);
                }
            }
            catch (ObjectDisposedException oex)
            {
                InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.ERROR_OCCURED, "Error during connection  (OnReceive, Disposed): " + oex.Message));
            }
            catch (Exception ex)
            {
                InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.ERROR_OCCURED, "Connection lost -> " + ex.Message));
                ResetSocket(_reconnect);
            }
        }


        private void OnSend(IAsyncResult ar)
        {
            try
            {
                _clientSocket.EndSend(ar);
            }
            catch (ObjectDisposedException oex )
            {
                InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.ERROR_OCCURED, "Error during data sending  (OnSend, Disposed): " + oex.Message));
            }
            catch (Exception ex)
            {
                InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.ERROR_OCCURED, "Error during data sending -> " + ex.Message));
                ResetSocket(_reconnect);
            }
        }

        public void Send(byte[] data)
        {
            try
            {
                if ((_clientSocket != null) && (_clientSocket.Connected))
                {
                    if (data != null)
                    {
                        OutgoingMessageCount++;
                        InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.MESSAGE_SENDING, data));
                        _clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(OnSend), _clientSocket);
                    }
                    else
                    {
                        InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.ERROR_OCCURED, "Attempt to send null message"));
                    }
                }
                else
                {
                    InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.ERROR_OCCURED, "Error during send message [Socket not ready]"));
                    ResetSocket(_reconnect);
                }
            }
            catch (SocketException)
            {
                InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.ERROR_OCCURED, "Error during send message [Socket Exception]"));
                ResetSocket(_reconnect);
            }
        }


        private void ResetSocket(Boolean isReconnected)
        {
            lock (ResetLocker)
            {
                if (_needReset)
                {
                    IsConnected = false;
                    InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.RESETING, null));

                    if (_clientSocket != null)
                    {
                        try
                        {
                            _clientSocket.Disconnect(false);
                            InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.DISCONNECTED, null));
                        }
                        catch (Exception ex)
                        {
                            InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.ERROR_OCCURED, "Disconnect imposible: " + ex.Message));
                        }

                        try
                        {
                            _clientSocket.Close();
                        }
                        catch (Exception ex)
                        {
                            InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.ERROR_OCCURED, "Error during reseting: " + ex.Message));
                        }
                        finally
                        {
                            _clientSocket = null;
                        }
                    }

                    _needReset = false;
                    InvokeConnectionClientEvent(new ConnectionClientEventArgs(ConnectionClientEventType.RESETED, "Socket reseted, Reconnect = " + _reconnect));

                    if (isReconnected)
                    {                   
                        _reconnectTimer.Start();
                    }
                    else
                    {
                        _reconnectTimer.Stop();
                    }
                }
            }
        }

        public void Dispose()
        {
            ResetSocket(false);
            if (_reconnectTimer == null) return;
            _reconnectTimer.Stop();
            _reconnectTimer.Dispose();
        }


    }//end class
}//end namespace
