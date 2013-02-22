using SandBox.Network;

namespace SandBox.WebUi
{
    public class Communication
    {
            private static Communication        _instance = null;
            private static SocketClient         _communicationClient;
            private static SocketClientSettings _communicationClientSettings;

            private Communication()
            {
                //_communicationClientSettings = SocketClientSettings.Load("communication.config");
                _communicationClientSettings = new SocketClientSettings
                                                   {
                                                       KeepAlive = false,
                                                       KeepAliveTimeout = 10000,
                                                       Reconnect = true,
                                                       ReconnectionCount = 10,
                                                       ReconnectionTimeout = 10000,
                                                       RemoteHost = "127.0.0.1",
                                                       RemotePort = 11100,
                                                       DataFormat = SocketDataFormat.PACKET
                                                   };


                _communicationClient = new SocketClient(_communicationClientSettings);
                _communicationClient.OnSocketClientEvent += OnCommunicationClientEvent;
                _communicationClient.Start();
            }

            public static Communication Instance
            {
                get { return _instance ?? (_instance = new Communication()); }
            }

            private void OnCommunicationClientEvent(SocketClientEventArgs args)
            {
                //throw new NotImplementedException();
            }

            //public void SendPacket

            
            public void SendPacket(Packet packet)
            {
                _communicationClientSettings = new SocketClientSettings
                {
                    KeepAlive = false,
                    KeepAliveTimeout = 10000,
                    Reconnect = true,
                    ReconnectionCount = 10,
                    ReconnectionTimeout = 10000,
                    RemoteHost = "127.0.0.1",
                    RemotePort = 11100,
                    DataFormat = SocketDataFormat.PACKET
                };


                _communicationClient = new SocketClient(_communicationClientSettings);
                _communicationClient.OnSocketClientEvent += OnCommunicationClientEvent;
                _communicationClient.Start();
                _communicationClient.Send(packet);
            }
    }
}