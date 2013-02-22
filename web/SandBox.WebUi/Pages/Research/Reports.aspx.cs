using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web;
using SandBox.Connection;
using SandBox.Db;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages.Research
{
    public partial class Reports : BaseMainPage
    {      
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Отчеты";
            PageMenu  = "~/App_Data/SideMenu/Research/ResearchMenu.xml";

            if (!IsPostBack)
            {
                var researches      = ResearchManager.GetResearchNameList(UserId);
                var researchesReady = ResearchManager.GetReadyResearchNameList(UserId);
                
                if (researches.Count == 0)
                {
                    labelNoItems.Text = "У вас нет исследований для просмотра отчетов";
                }
                else
                {
                    if (researchesReady.Count == 0)
                    {
                        labelNoItems.Text = "У вас нет завершенных исследований, для просмотра отчета дождитесь завершения исследования";
                    }
                    else
                    {
                        cbResearch.DataSource = researchesReady;
                        cbResearch.DataBind();
                    }
                }
                Session["researchId"] = 0;
            }

            UpdatePanelReports.Visible = false;
            gridViewReports.DataSource = ReportManager.GetReports(Convert.ToInt32(Session["researchId"]));        
            gridViewReports.DataBind();
        }

        protected void BtnGetClick(object sender, EventArgs e)
        {
            var research = ResearchManager.GetResearch(UserId, cbResearch.Text);       
            if (research == null) return;
            Session["researchId"] = research.Id;
            try
            {
                Session["rsch"] = research.Id;
            }
            catch
            {
                Session.Add("rsch", research.Id);
            }
            UpdatePanelReports.Visible = true;
            gridViewReports.DataSource = ReportManager.GetReports(Convert.ToInt32(Session["researchId"]));
            gridViewReports.DataBind();



            linkGetTraffic.NavigateUrl = "javascript;";
            if (research.TrafficFileReady == (Int32)TrafficFileReady.COMPLETE)
            {
                String link = research.TrafficFileName;
                linkGetTraffic.NavigateUrl = link;
            }

            String path = Request.Path;
            String root = path.Substring(0, path.LastIndexOf("/"));
            linkGetProcessList.NavigateUrl = root + "/ProcessList.aspx?research=" + research.Id;
            linkGetRegistryList.NavigateUrl = root + "/RegistryList.aspx?research=" + research.Id;
            linkGetFileList.NavigateUrl = root + "/FileList.aspx?research=" + research.Id;
            

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
        }
            

        [System.Web.Services.WebMethod]
        public static void GetTraffic()
        {
            /*Int32 researchId = Convert.ToInt32(HttpContext.Current.Session["researchId"]);
            Db.Research research = ResearchManager.GetResearch(researchId);
            
            if (research.TrafficFileReady != (Int32)TrafficFileReady.COMPLETE) return;
            String link = research.TrafficFileName;

            using (WebClient ftpClient = new WebClient())
            {
                ftpClient.DownloadFile(link, "test.asp");
            }*/
        }
    }//end class
}//end namespace