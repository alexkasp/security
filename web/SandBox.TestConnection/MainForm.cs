using System;
using System.Windows.Forms;
using SandBox.Connection;
using SandBox.Data;
using SandBox.Db;
using SandBox.Log;
using System.Text;
using System.Linq;


namespace SandBox.TestConnection
{
    public partial class MainForm : Form
    {
        private readonly ConnectionServer   _server;
        private readonly ConnectionClientEx _client;
        
        SandBoxDataContext db2 = new SandBoxDataContext();
        private static ConnectionClientEx _client2 = ConnectionClientEx.Instance;
        
        private void AddToListBox(ListBox listBox, String item)
        {
            if (listBox.InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => listBox.Items.Add(item)));
            }
            else
            {
                listBox.Items.Add(item);
            }
        }

        private void AddToTextBox(TextBox textBox, String text)
        {
            if (textBox.InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => textBox.Text = text));
            }
            else
            {
                textBox.Text = text;
            }
        }

        private static String ShowMessage(byte[] ba)
        {
            return "Length: " + ba.Length + ", Data: " + DataUtils.ByteArrayToHexString(ba);
        }

        public MainForm()
        {
            InitializeComponent();
            Application.ApplicationExit += ApplicationApplicationExit;

            _server = new ConnectionServer();
            _server.OnConnectionServerEvent += OnServerEvent;

            _client = ConnectionClientEx.Instance;
            _client.OnConnectionClientExEvent += OnClientEvent;

            tbDb.Text = DbManager.GetConnectionStatus() ? "connected" : "not connected";

            tbTime_1.Text = DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss");
        }

        private void ApplicationApplicationExit(object sender, EventArgs e)
        {
            if (_server != null) _server.Dispose();
            if (_client != null) _client.Dispose();
        }

        private void btnSendReport_Click(object sender, EventArgs e)
        {
            CommandForm commandForm = new CommandForm(_server);
                        commandForm.ShowDialog();
        }

        private void BtnCheckTimeClick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            tbTime_2.Text = dt.ToString("yyyy-MM-dd HH':'mm':'ss"); ;

            DateTime startTime;
            DateTime.TryParse(tbTime_1.Text, out startTime);

            var dif = (dt - startTime).TotalSeconds;
            labelTime.Text = (Int32)dif + " секунд";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var rschs = ResearchManager.GetExecutingResearches();
            foreach (var rsch in rschs)
            {
                var evnts = ResearchManager.GetEventsByRschId(rsch.Id);
                if (ContainsStopEvent(evnts, rsch.Id))
                {
                    StopResearch(String.Format("{0}", rsch.Id));
                    ResearchManager.InsertToStopRschSatus(rsch.Id, "остановлено по наступлению события");
                }
            }
        }




        private bool ContainsStopEvent(IQueryable<events> evnts, int rschId)
        {
            var stopEvrnt = ResearchManager.GetStopEventForRsch(rschId);
            if (stopEvrnt == null) return false;
            //if (evnts.Count() == 0) return false;
            foreach (var e in evnts)
            {
                if ((e.dest == stopEvrnt.dest) && (e.module == stopEvrnt.module) && (e.@event == stopEvrnt.@event) && (e.who == stopEvrnt.who))
                    return true;
            }
            return false;
        }




        public static void StopResearch(string id)
        {
            Int32 researchId = Convert.ToInt32(id);
            //Приведение таблтцы [dbo].[events] в актуальное состояние
            //int res1 = ResearchManager.UpdateEnents(researchId);
            if (ResearchManager.GetResearch(researchId).State == (Int32)ResearchState.EXECUTING)
            {
                SandBox.Db.Research research = ResearchManager.GetResearch(researchId);
                MLogger.LogTo(Level.TRACE, false, "Stop research '" + ResearchManager.GetResearch(researchId).ResearchName + "' by stop event '");
                ResearchManager.UpdateResearchState(researchId, ResearchState.COMPLETING);

                //Останаливаем виртуалку
                String machineName = VmManager.GetVmName(research.VmId);
                Packet packet = new Packet { Type = PacketType.CMD_VM_STOP, Direction = PacketDirection.REQUEST };
                packet.AddParameter(Encoding.UTF8.GetBytes(machineName));
                SendPacket(packet.ToByteArray());

                //Добавил---
                ResearchManager.UpdateResearchStopTime(research.Id);
                ResearchManager.UpdateResearchState(research.Id, ResearchState.COMPLETED);
                //----------
                int res1 = ResearchManager.UpdateEnents(researchId);
            }
            else
            {
                MLogger.LogTo(Level.TRACE, false, "Unsuccessful attempt to stop research '" /*+ ResearchManager.GetResearch(researchId).ResearchName + "' by user '" + UserManager.GetUser(_userId).UserName + "' , research already stopped"*/);
            }
            //Приведение таблтцы [dbo].[events] в актуальное состояние
            int res = ResearchManager.UpdateEnents(researchId);

        }


        protected static void SendPacket(byte[] data)
        {
            _client2.Send(data);
        }

        protected static void SendPacket(Packet packet)
        {
            _client2.Send(Packet.ToByteArray(packet));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }//end form
}
