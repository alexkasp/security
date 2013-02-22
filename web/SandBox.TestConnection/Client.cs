using System;
using System.Text;
using SandBox.Connection;
using SandBox.Db;

namespace SandBox.TestConnection
{
    public partial class MainForm
    {
        private void OnClientEvent(ConnectionClientEventArgs args)
        {
            switch (args.EventType)
            {
                case ConnectionClientEventType.CONNECTED:
                    AddToListBox(lbClientLog, "connected");
                    foreach (var machine in VmManager.GetVms())
                    {
                        GetVmStatus(machine.Id);
                    }
                    break;
                case ConnectionClientEventType.CONNECTING:
                    AddToListBox(lbClientLog, "Connecting");
                    break;
                case ConnectionClientEventType.DISCONNECTED:
                    AddToListBox(lbClientLog, "Disconnected");
                    break;
                case ConnectionClientEventType.MESSAGE_SENDING:
                    AddToTextBox(tbClientOutgoing, "Отправлено: " + _client.OutgoingMessageCount);
                    AddToListBox(lbClientLog, "[out] " + ShowMessage((byte[])args.EventData));
                    break;
                case ConnectionClientEventType.MESSAGE_RECEIVED:
                    AddToTextBox(tbClientIncoming, "Получено: " + _client.IncomingMessageCount);
                    AddToListBox(lbClientLog, "[in] " + ShowMessage((byte[])args.EventData));
                    break;
                case ConnectionClientEventType.RESETED:
                    AddToListBox(lbClientLog, "Reseted");
                    break;
                case ConnectionClientEventType.RESETING:
                    break;
                case ConnectionClientEventType.ERROR_OCCURED:
                    AddToListBox(lbClientLog, args.EventData.ToString());
                    break;
            }
        }

        private void BtnConnectClick(object sender, EventArgs e)
        {
            _client.Start(tbHost.Text, Convert.ToInt32(tbClientPort.Value), true);
        }

        private void BtnDisconnectClick(object sender, EventArgs e)
        {
            _client.Stop();
        }

        private void BtnClientSendClick(object sender, EventArgs e)
        {
            Packet packet = new Packet { Type = PacketType.CMD_VM_STATUS, Direction = PacketDirection.REQUEST };
            packet.AddParameter(Encoding.UTF8.GetBytes(tbClientMessage.Text));
            _client.Send(packet.ToByteArray());
        }

        private void BtnClientClearClick(object sender, EventArgs e)
        {
            lbClientLog.Items.Clear();
        }


        private void GetVmStatus(Int32 id)
        {
            String machineName = VmManager.GetVmName(id);
            Packet packet = new Packet { Type = PacketType.CMD_VM_STATUS, Direction = PacketDirection.REQUEST };
            packet.AddParameter(Encoding.UTF8.GetBytes(machineName));
            _client.Send(packet.ToByteArray());
        }
    }//end class
}//end namespace
