using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SandBox.Connection;

namespace SandBox.TestConnection
{
    public partial class CommandForm : Form
    {
        private readonly ConnectionServer _server;

        public CommandForm(ConnectionServer server)
        {
            InitializeComponent();
            _server = server;
        }

        private void BtnExitClick(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnEventReportClick(object sender, EventArgs e)
        {
            Int32 envId = Convert.ToInt32(tbEnvId.Text);
            Int32 modId = Convert.ToInt32(tbModId.Text);
            Int32 actId = Convert.ToInt32(tbActId.Text);
            String obj  = tbObject.Text;
            String trgt = tbTarget.Text;

            byte[] objBt = Encoding.UTF8.GetBytes(obj);
            byte[] actBt = BitConverter.GetBytes(actId);
            byte[] trgBt = Encoding.UTF8.GetBytes(trgt);
            
            byte[] data = new byte[306];
            data[0] = (BitConverter.GetBytes(envId))[0];
            data[1] = (BitConverter.GetBytes(modId))[0];
            Buffer.BlockCopy(objBt, 0, data, 2, objBt.Length);
            Buffer.BlockCopy(actBt, 0, data, 102, 4);
            Buffer.BlockCopy(trgBt, 0, data, 106, trgBt.Length);


            Packet packet = new Packet {Type = PacketType.ANS_REPORT, Direction = PacketDirection.RESPONSE};
                   packet.AddParameter(data);
            _server.Send(Packet.ToByteArray(packet));

        }

        private void BtnReadyReportClick(object sender, EventArgs e)
        {
            Int32 id = Convert.ToInt32(tb_1_id.Text);
            Int32 type = Convert.ToInt32(tb_1_type.Text);
            
            List<String> ipTetrStr = tb_1_ip.Text.Split('.').ToList();          
            List<Int32> ipTetr = ipTetrStr.Select(i => Convert.ToInt32(i)).ToList();
            byte[] ipAddr = new[] { BitConverter.GetBytes(ipTetr[0])[0], BitConverter.GetBytes(ipTetr[1])[0], BitConverter.GetBytes(ipTetr[2])[0], BitConverter.GetBytes(ipTetr[3])[0] };
            byte[] macAddr = StringToByteArray(tb_1_mac.Text);

            byte[] data = new byte[12];
            data[0] = (BitConverter.GetBytes(id))[0];
            data[1] = (BitConverter.GetBytes(type))[0];
            Buffer.BlockCopy(ipAddr, 0, data, 2, 4);
            Buffer.BlockCopy(macAddr, 0, data, 6, 6);

            Packet packet = new Packet { Type = PacketType.ANS_VM_READY, Direction = PacketDirection.RESPONSE };
            packet.AddParameter(data);
            _server.Send(Packet.ToByteArray(packet));
        }

        private static byte[] StringToByteArray(String hex)
        {
            String hx = hex.Replace("-", String.Empty);

            return Enumerable.Range(0, hx.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hx.Substring(x, 2), 16))
                             .ToArray();
        }
    }//end class
}//end namespace
