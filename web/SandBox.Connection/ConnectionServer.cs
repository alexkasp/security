using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace SandBox.Connection
{
    public enum ConnectionServerEventType
    {
        STARTED,
        STOPED,
        RESETED,
        MESSAGE_RECEIVED,
        MESSAGE_SENDING,
        CLIENT_CONNECTED,
        CLIENT_DISCONNECTED,
        ERROR_OCCURED
    }//end ConnectionServerEventType enum
    
    public class ConnectionServerEventArgs : EventArgs
    {
        public ConnectionServerEventType EventType;
        public Object EventData;

        public ConnectionServerEventArgs(ConnectionServerEventType eventType, Object eventData)
        {
            EventType = eventType;
            EventData = eventData;
        }
    }//end ConnectionServerEventArgs class

    public class ConnectionServer : IDisposable
    {
        private Socket _serverSocket;
        private Socket _clientSocket;
        private byte[] _bytesToReceive;
        private Int32  _port;

        public delegate void ConnectionServerEventHandler(ConnectionServerEventArgs args);
        public event ConnectionServerEventHandler OnConnectionServerEvent;

        public int IncomingMessageCount { get; private set; }
        public int OutgoingMessageCount { get; private set; }

        public void InvokeConnectionServerEvent(ConnectionServerEventArgs args)
        {
            ConnectionServerEventHandler handler = OnConnectionServerEvent;
            if (handler != null) handler(args);
        }

        public void Start(Int32 port)
        {
            _port = port;
            try
            {
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                var ipEndPoint = new IPEndPoint(IPAddress.Loopback, _port);
                _serverSocket.Bind(ipEndPoint);
                _serverSocket.Listen(1);
                InvokeConnectionServerEvent(new ConnectionServerEventArgs(ConnectionServerEventType.STARTED, "Server started on " + IPAddress.Loopback + ":" + _port));
                _serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);
            }
            catch (Exception ex)
            {
                InvokeConnectionServerEvent(new ConnectionServerEventArgs(ConnectionServerEventType.ERROR_OCCURED, "Server error | [Start]: " + ex));
                ResetSocket(false);
            }
        }


        private void OnAccept(IAsyncResult ar)
        {
            try
            {
                if ((_serverSocket != null) && (_serverSocket.IsBound))
                {
                    _clientSocket = _serverSocket.EndAccept(ar);
                    InvokeConnectionServerEvent(new ConnectionServerEventArgs(ConnectionServerEventType.CLIENT_CONNECTED, null));

                    _bytesToReceive = new byte[_clientSocket.ReceiveBufferSize];
                    _clientSocket.BeginReceive(_bytesToReceive, 0, _bytesToReceive.Length, SocketFlags.None, new AsyncCallback(OnReceive), _clientSocket);
                }
            }
            catch (ObjectDisposedException oex)
            {
                InvokeConnectionServerEvent(new ConnectionServerEventArgs(ConnectionServerEventType.ERROR_OCCURED, "Error during connection  (OnAccept, Disposed): " + oex.Message));
            }
            catch (Exception ex)
            {
                InvokeConnectionServerEvent(new ConnectionServerEventArgs(ConnectionServerEventType.ERROR_OCCURED, "Server error | [OnAccept]: " + ex));
                ResetSocket(false);
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
                    foreach (var packet in PacketReceiver.GetPackets(_bytesToReceive.Take(rLen).ToArray(), _clientSocket.ReceiveBufferSize))
                    {
                        IncomingMessageCount++;
                        InvokeConnectionServerEvent(new ConnectionServerEventArgs(ConnectionServerEventType.MESSAGE_RECEIVED, packet));
                    }

                    _bytesToReceive = new byte[_clientSocket.ReceiveBufferSize];
                    _clientSocket.BeginReceive(_bytesToReceive, 0, _bytesToReceive.Length, SocketFlags.None, new AsyncCallback(OnReceive), _clientSocket);
                }
                else
                {
                    InvokeConnectionServerEvent(new ConnectionServerEventArgs(ConnectionServerEventType.CLIENT_DISCONNECTED, "Client disconnected [normal]"));
                    ResetSocket(true);
                }
            }
            catch (ObjectDisposedException oex)
            {
                InvokeConnectionServerEvent(new ConnectionServerEventArgs(ConnectionServerEventType.ERROR_OCCURED, "Error during connection  (OnReceive, Disposed): " + oex.Message));
            }
            catch (Exception ex)
            {
                InvokeConnectionServerEvent(new ConnectionServerEventArgs(ConnectionServerEventType.CLIENT_DISCONNECTED, "Client disconnected [unnormal]: " + ex));
                ResetSocket(true);
            }
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                _clientSocket = (Socket)ar.AsyncState;
                _clientSocket.EndSend(ar);
            }
            catch (ObjectDisposedException oex)
            {
                InvokeConnectionServerEvent(new ConnectionServerEventArgs(ConnectionServerEventType.ERROR_OCCURED, "Error during connection  (OnSend, Disposed): " + oex.Message));
            }
            catch (Exception ex)
            {
                InvokeConnectionServerEvent(new ConnectionServerEventArgs(ConnectionServerEventType.ERROR_OCCURED, "Server error | [OnSend]: " + ex.Message));
                ResetSocket(false);
            }
        }


        public void Stop()
        {
            ResetSocket(false);
            InvokeConnectionServerEvent(new ConnectionServerEventArgs(ConnectionServerEventType.STOPED, null));
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
                        InvokeConnectionServerEvent(
                            new ConnectionServerEventArgs(ConnectionServerEventType.MESSAGE_SENDING, data));
                        _clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(OnSend),
                                                _clientSocket);
                        
                    }
                    else
                    {
                        InvokeConnectionServerEvent(
                            new ConnectionServerEventArgs(ConnectionServerEventType.ERROR_OCCURED,
                                                          "Attempt to send null message"));
                    }
                }
                else
                {
                    InvokeConnectionServerEvent(new ConnectionServerEventArgs(ConnectionServerEventType.ERROR_OCCURED, "Error during send message [Socket not ready]"));
                    ResetSocket(true);
                }
            }
            catch (SocketException)
            {
                InvokeConnectionServerEvent(new ConnectionServerEventArgs(ConnectionServerEventType.ERROR_OCCURED, "Error during send message [Socket Exception]"));
                ResetSocket(true);
            }
        }

        private void ResetSocket(Boolean isReconnected)
        {
            if (_clientSocket != null)
            {
                _clientSocket.Close();
                _clientSocket = null;
            }

            if (_serverSocket != null)
            {
                _serverSocket.Close();
                _serverSocket = null;
            }

            InvokeConnectionServerEvent( new ConnectionServerEventArgs(ConnectionServerEventType.RESETED, null));

            if (isReconnected) Start(_port);
        }

        public void Dispose()
        {
            Stop();
        }

    }//end ConnectionServer class
}//end namespace
