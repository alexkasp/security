using System;
using System.Windows.Forms;
using SandBox.Connection;

namespace SandBox.TestConnection
{
    public partial class MainForm
    {
        private Int32 _counter = 0;
        
        private void OnServerEvent(ConnectionServerEventArgs args)
        {
            switch (args.EventType)
            {
                case ConnectionServerEventType.STARTED:
                    AddToListBox(lbServerLog, "Started");
                    break;
                case ConnectionServerEventType.STOPED:
                    AddToListBox(lbServerLog, "Stoped");
                    break;
                case ConnectionServerEventType.RESETED:
                    break;
                case ConnectionServerEventType.MESSAGE_SENDING:
                    AddToTextBox(tbServerOutgoing, "Отправлено: " + _server.OutgoingMessageCount);
                    AddToListBox(lbServerLog, "[out] " + ShowMessage((byte[])args.EventData));
                    break;
                case ConnectionServerEventType.MESSAGE_RECEIVED:
                    AddToTextBox(tbServerIncoming, "Получено: " + _server.IncomingMessageCount);
                    AddToListBox(lbServerLog, "[in] " + ShowMessage((byte[])args.EventData));
                    AnalyzeMessageFromClient((byte[])args.EventData);
                    break;
                case ConnectionServerEventType.CLIENT_CONNECTED:
                    AddToListBox(lbServerLog, "Client connected");
                    break;
                case ConnectionServerEventType.CLIENT_DISCONNECTED:
                    AddToListBox(lbServerLog, "Client disconnected");
                    break;
                case ConnectionServerEventType.ERROR_OCCURED:
                    AddToListBox(lbServerLog, args.EventData.ToString());
                    break;
            }
        }

        private void BtnStartServerClick(object sender, EventArgs e)
        {
            _server.Start(Convert.ToInt32(tbServerPort.Value));
        }

        private void BtnStopServerClick(object sender, EventArgs e)
        {
            _server.Stop();
        }

        private void BtnServerClearClick(object sender, EventArgs e)
        {
            lbServerLog.Items.Clear();
        }

        private void AnalyzeMessageFromClient(byte[] data)
        {
            Packet resPacket = Packet.ToPacket(data);
            resPacket.Direction = PacketDirection.RESPONSE;

            if (resPacket.Type == PacketType.CMD_VM_STATUS)
            {
                resPacket.AddParameter(GetRandomNumber(0, 1) == 0 ? new byte[] { 0xf0 } : new byte[] { 0xf1 });

                if (_counter == 0)
                {
                    resPacket.AddParameter(new byte[] { 0x09, 0x08, 0x07, 0x06, 0x05, 0x00 });
                    _counter++;
                }
                else if (_counter == 1)
                {
                    resPacket.AddParameter(new byte[] { 0x09, 0x08, 0x07, 0x06, 0x05, 0x01 });
                    _counter++;
                }
                else if (_counter == 2)
                {
                    resPacket.AddParameter(new byte[] { 0x09, 0x08, 0x07, 0x06, 0x05, 0x02 });
                    _counter = 0;
                }

                _server.Send(Packet.ToByteArray(resPacket));
            }



            
        }

        public Int32 GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            Double dbl = random.NextDouble() * (maximum - minimum) + minimum;
            return Convert.ToInt32(dbl);
        }
    }//end class
}//end namespace
