using System;

namespace SandBox.Connection
{
    public class ConnectionClientEx
    {
        private static ConnectionClient _connectionClient;

        public delegate void ConnectionClientExEventHandler(ConnectionClientEventArgs args);
        public event ConnectionClientExEventHandler OnConnectionClientExEvent;

        public int IncomingMessageCount { get { return _connectionClient.IncomingMessageCount; }}
        public int OutgoingMessageCount { get { return _connectionClient.OutgoingMessageCount; }}
        public Boolean IsConnected { get { return _connectionClient.IsConnected; } }

        public void InvokeOnConnectionClientExEvent(ConnectionClientEventArgs args)
        {
            ConnectionClientExEventHandler handler = OnConnectionClientExEvent;
            if (handler != null) handler(args);
        }

        private static ConnectionClientEx _instance;
        private ConnectionClientEx()
        {
            _connectionClient = new ConnectionClient();
            _connectionClient.OnConnectionClientEvent += OnConnectionClientEvent;
        }

        public static ConnectionClientEx Instance
        {
            get { return _instance ?? (_instance = new ConnectionClientEx()); }
        }

        private void OnConnectionClientEvent(ConnectionClientEventArgs args)
        {
            InvokeOnConnectionClientExEvent(args);
        }

        public void Start(String remoteHost, Int32 remotePort, Boolean reconnect)
        {
            if (_connectionClient != null)
            {
                _connectionClient.Start(remoteHost, remotePort, reconnect);
            }
        }

        public void Stop()
        {
            if (_connectionClient != null)
            {
                _connectionClient.Stop();
            }
        }

        public void Send(byte[] data)
        {
            if (_connectionClient != null)
            {
                _connectionClient.Send(data);
            }
        }

        public void Dispose()
        {
            if (_connectionClient != null)
            {
                _connectionClient.Dispose();
            }
            _instance = null;
        }
    }//end ConnectionClientEx class
}//end namespace
