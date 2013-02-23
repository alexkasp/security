using System;
using SandBox.Db;
using SandBox.WebUi.Base;
using SandBox.Connection;
using System.Text;
using System.Drawing;

namespace SandBox.WebUi.Pages.Research
{
    public partial class ReportList : BaseMainPage
    {
        public Db.Research Rs;

        protected new void Page_Load(object sender, EventArgs e)
        {

            gridViewReports.KeyFieldName = "Id";
            base.Page_Load(sender, e);
            PageTitle = "Отчет";
            PageMenu  = "~/App_Data/SideMenu/Research/ResearchMenu.xml";
            Int32 researchId = -1;
            try
            {
                researchId = (int)Session["rsId"];
            }
            catch
            {
                researchId =  Convert.ToInt32(Request.QueryString["researchId"]);
            }
            /*Convert.ToInt32(Request.QueryString["research"]);*/
            Rs = ResearchManager.GetResearch(researchId);           
            if (Rs == null)
            {
                Response.Redirect("~/Error");
            }
            LOS.Text = ResearchManager.GetRschOS(researchId);
            LStartTime.Text = Rs.CreatedDate.ToString("dd MMM HH:mm:ss");
            LStopTime.Text = ResearchManager.GetElapsedTimeInMinutes(Rs.StartedDate, Rs.StoppedDate, Rs.Duration);
            LTimeToWork.Text =ResearchManager.GetLeftTimeInMinutes(Rs.StartedDate, Rs.Duration);
            LHeader.Text = String.Format("Исследлвание (№{0}): {1}", Rs.Id, Rs.ResearchName);
            HLPorts.NavigateUrl += ("?research=" + researchId);
            ASPxHyperLink4.NavigateUrl += ("?research=" + researchId);
            Session["rsch"] = researchId;

            gridViewReports.DataSource = ResearchManager.GetEventsForRsch(Rs.Id);
            var newPageSize = (Int32)CBPagingSize.SelectedItem.Value;
            gridViewReports.SettingsPager.PageSize = newPageSize;
            gridViewReports.DataBind();

            if (Rs.TrafficFileReady == (Int32)TrafficFileReady.COMPLETE)
            {
                
                String link = Rs.TrafficFileName;
                linkGetTraffic.NavigateUrl = link;
                linkGetTraffic.Visible = true;
                linkGetTraffic.Enabled = true;
                ASPxButton1.Visible = false;
                ASPxButton1.Visible = false;
            }

            if (!IsPostBack)
            {
                ReportsBuilder.RschPropsListBuilder(TreeView1, Rs.Id);
                ASPxHyperLink5.NavigateUrl += ("?research=" + researchId);
                //if (Rs.TrafficFileReady == (Int32)TrafficFileReady.NOACTION)
                //{
                //    var researchVmData = ResearchManager.GetResearchVmData(Rs.ResearchVmData);
                //    if (researchVmData == null) return;

                //    String ip = researchVmData.VmEnvIp;
                //    String beginTime = Rs.StartedDate.HasValue ? Rs.StartedDate.Value.ToString("yyyy-MM-dd HH':'mm':'ss") : DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss");
                //    String endTime = Rs.StoppedDate.HasValue ? Rs.StoppedDate.Value.ToString("yyyy-MM-dd HH':'mm':'ss") : DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss");

                //    Packet packet = new Packet { Type = PacketType.CMD_LOAD_TRAFFIC, Direction = PacketDirection.REQUEST };
                //    packet.AddParameter(Encoding.UTF8.GetBytes(ip));
                //    packet.AddParameter(Encoding.UTF8.GetBytes(beginTime));
                //    packet.AddParameter(Encoding.UTF8.GetBytes(endTime));
                //    SendPacket(packet);

                //    String filename = ip + beginTime + ".pcap";
                //    ResearchManager.UpdateTrafficInfo(Rs.Id, TrafficFileReady.EXECUTING, filename);
                //}
                
            }
        }

        protected void CBPagingSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newPageSize = (Int32)CBPagingSize.SelectedItem.Value;
            gridViewReports.SettingsPager.PageSize = newPageSize;
            gridViewReports.DataBind();

        }

        protected void BtnGetClick(object sender, EventArgs e)
        {
            var research = ResearchManager.GetResearch(UserId, Rs.ResearchName);
            if (research == null) return;
            Session["researchId"] = research.Id;

            // UpdatePanelReports.Visible = true;
            gridViewReports.DataSource = ReportManager.GetReports(Convert.ToInt32(Session["researchId"]));
            gridViewReports.DataBind();



            linkGetTraffic.NavigateUrl = "javascript;";
            if (research.TrafficFileReady == (Int32)TrafficFileReady.COMPLETE)
            {
                String link = research.TrafficFileName;
                linkGetTraffic.NavigateUrl = link;
                linkGetTraffic.Visible = true;
                linkGetTraffic.Enabled = true;
                ASPxButton1.Visible = false;
            }

            // это наверно не надо, не пойму зачем тут этот код у него вставлен

                        //String path = Request.Path;
                        //String root = path.Substring(0, path.LastIndexOf("/"));
                        //linkGetProcessList.NavigateUrl = root + "/ProcessList.aspx?research=" + research.Id;
                        //linkGetRegistryList.NavigateUrl = root + "/RegistryList.aspx?research=" + research.Id;
                        //linkGetFileList.NavigateUrl = root + "/FileList.aspx?research=" + research.Id;

            //*/
            if (research.TrafficFileReady == (Int32)TrafficFileReady.NOACTION)
            {
                var researchVmData = ResearchManager.GetResearchVmData(research.ResearchVmData);
                if (researchVmData == null) return;

                String ip = researchVmData.VmEnvIp;
                String beginTime = research.StartedDate.HasValue ? research.StartedDate.Value.ToString("yyyy-MM-dd HH':'mm':'ss") : DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss");
                String endTime = research.StoppedDate.HasValue ? research.StoppedDate.Value.ToString("yyyy-MM-dd HH':'mm':'ss") : DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss");

                Packet packet = new Packet { Type = PacketType.CMD_LOAD_TRAFFIC, Direction = PacketDirection.REQUEST };
                packet.AddParameter(Encoding.UTF8.GetBytes(ip));
                packet.AddParameter(Encoding.UTF8.GetBytes(beginTime));
                packet.AddParameter(Encoding.UTF8.GetBytes(endTime));
                SendPacket(packet);

                String filename = ip + beginTime + ".pcap";
                ResearchManager.UpdateTrafficInfo(research.Id, TrafficFileReady.EXECUTING, filename);
                
            }
            ASPxButton1.Enabled = false;
            ASPxButton1.Text = "Запрос на получение трафика отправлен";

            gridViewReports.DataSource = ResearchManager.GetEventsForRsch(Rs.Id);
            var newPageSize = (Int32)CBPagingSize.SelectedItem.Value;
            gridViewReports.SettingsPager.PageSize = newPageSize;
            gridViewReports.DataBind();
        }

        protected void ASPxButton2_Click(object sender, EventArgs e)
        {
            int sign = ASPxComboBox1.Text == "Критически важное" ? 0 : 1;
            string module = (string)this.gridViewReports.GetRowValues(this.gridViewReports.FocusedRowIndex, "ModuleId");
            string evnt = (string)this.gridViewReports.GetRowValues(this.gridViewReports.FocusedRowIndex, "EventCode");
            string dest = (string)this.gridViewReports.GetRowValues(this.gridViewReports.FocusedRowIndex, "Dest");
            string who = (string)this.gridViewReports.GetRowValues(this.gridViewReports.FocusedRowIndex, "Who");
            int moduleCode = ResearchManager.GetModuleIdByDescr(module);
            int eventCode = ResearchManager.GetEventIdByDescr(evnt);
            ReportManager.InsertRowDirectoriesOfEvents(sign, moduleCode, eventCode, dest,who);

        }

        protected void gridViewReports_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;
            if (e.KeyValue != null)
            {
                long key = (long)e.KeyValue;
                var evt = ResearchManager.GetEventById(key);
                if (evt != null)
                {
                    int evtSignif = ReportManager.GetEvtSignif(evt);
                    switch (evtSignif)
                    {
                        case 0:
                            {
                                e.Row.BackColor = Color.Salmon;
                                break;
                            }
                        case 1:
                            {
                                e.Row.BackColor = Color.SandyBrown;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    //e.Row.BackColor = Color.Yellow;
                }
            }
        }

        //private void ApdateTraficLinq()
        //{
        //    var research = ResearchManager.GetResearch(UserId, Rs.ResearchName);
        //    if (research == null) return;
        //    Session["researchId"] = research.Id;

        //    // UpdatePanelReports.Visible = true;
        //    gridViewReports.DataSource = ReportManager.GetReports(Convert.ToInt32(Session["researchId"]));
        //    gridViewReports.DataBind();



        //    linkGetTraffic.NavigateUrl = "javascript;";
        //    if (research.TrafficFileReady == (Int32)TrafficFileReady.COMPLETE)
        //    {
        //        String link = research.TrafficFileName;
        //        linkGetTraffic.NavigateUrl = link;
        //        linkGetTraffic.Visible = true;
        //        linkGetTraffic.Enabled = true;
        //        ASPxButton1.Visible = false;
        //    }
        //}
    }//end class
}//end namespace