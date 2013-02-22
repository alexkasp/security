using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SandBox.Db;
using SandBox.Log;
using SandBox.Connection;

namespace StopRschByEvent
{
    public partial class Form1 : Form
    {
        SandBoxDataContext db = new SandBoxDataContext();
        private static ConnectionClientEx _client = ConnectionClientEx.Instance;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var rschs = ResearchManager.GetExecutingResearches();
            foreach (var rsch in rschs)
            {
                var evnts = ResearchManager.GetEventsByRschId(rsch.Id);
                if (ContainsStopEvent(evnts, rsch.Id))
                {
                    StopResearch(String.Format("{0}",rsch.Id));
                    ResearchManager.InsertToStopRschSatus(rsch.Id, "остановлено по наступлению события");
                }
            }
        }

        private bool ContainsStopEvent(IQueryable<events> evnts, int rschId)
        {
            var stopEvrnt = ResearchManager.GetStopEventForRsch(rschId);
            if(stopEvrnt == null) return false;
            if (evnts.Count() == 0) return false;
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
                MLogger.LogTo(Level.TRACE, false, "Unsuccessful attempt to stop research '" + ResearchManager.GetResearch(researchId).ResearchName + "' by user '" + UserManager.GetUser(_userId).UserName + "' , research already stopped");
            }
            //Приведение таблтцы [dbo].[events] в актуальное состояние
            int res = ResearchManager.UpdateEnents(researchId);

        }


        protected static void SendPacket(byte[] data)
        {
            _client.Send(data);
        }

        protected static void SendPacket(Packet packet)
        {
            _client.Send(Packet.ToByteArray(packet));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
