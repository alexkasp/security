using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SandBox.Db;
using System.Collections.Generic;

namespace SandBox.WebUi {
    public partial class RootMaster : MasterPage
    {
        private const Int32 MENU_ADMIN          = 0;
        private const Int32 MENU_USER           = 1;
        private const Int32 MENU_EMPTY          = 2;
        private const Int32 MENU_FILEMANAGER    = 3;

        private Dictionary<string, int> _vpoDanger = new Dictionary<string, int>();
        string[] _vpoTopNames = new string[] { "", "", "", "", "" };
        int[] _vpoTopValues = new int[] { 0, 0, 0, 0, 0 };

        private Dictionary<string, List<int>> _osTopDict = new Dictionary<string, List<int>>();

        string[] _osTopNames = new string[] { "", "", "", "", "" };
        int[] _osRschTopValues = new int[] { 0, 0, 0, 0, 0 };
        int[] _osBadEventsRschTopValues = new int[] { 0, 0, 0, 0, 0 };

        public string GetOs(int i)
        {
            return _osTopNames[i];
        }
        protected void Page_Load(object sender, EventArgs e)
        {     
            if (!Page.User.Identity.IsAuthenticated)
            {
                SetMenu(MENU_EMPTY);
            }
            else
            {
                if (Page.User.IsInRole("Administrator"))
                {
                    SetMenu(MENU_ADMIN);
                }
                else 
                if (Page.User.IsInRole("User"))
                {
                    SetMenu(MENU_USER);
                }
                else
                if (Page.User.IsInRole("FileManager"))
                {
                    SetMenu(MENU_FILEMANAGER);
                }
            }
            foreach (MenuItem item in Menu1.Items)
                {
                   if (Request.Url.AbsoluteUri.ToLower().Contains(item.NavigateUrl.ToLower()))
                   {
                      item.Selected = true;
                   }
                }
            _vpoDanger = ReportManager.GetTopFiveMlwr();
            int i = 0;
            foreach (var v in _vpoDanger)
            {
                if (i == 5) break;
                _vpoTopNames[i] = v.Key;
                _vpoTopValues[i] = v.Value;
                i++;
            }
            _osTopDict = VmManager.GetOsChart();
            i = 0;
            foreach (var v in _osTopDict)
            {
                if (i == 5) break;
                _osTopNames[i] = v.Key;
                _osBadEventsRschTopValues[i] = v.Value[1];
                _osRschTopValues[i] = v.Value[0];
                i++;
            }
             //string someScript = "";
            string someScript2 = "";
            string someScript = "";
            //someScript = "<SCRIPT TYPE=\"text/javascript\">$(document).ready(function () { var r2 = new Raphael(\"chartOSuse\"); r2.hbarchart(0, 0, 130, 90, [[" + _osRschTopValues[0] + ", " + _osRschTopValues[1] + ", " + _osRschTopValues[2] + ", " + _osRschTopValues[3] + ", " + _osRschTopValues[4] + "]],{\"gutter\":\"30%\",\"colors\":[\"#ffffff\"],\"rtl\":true}).label([]);});  </SCRIPT>";
            someScript2 = " <SCRIPT TYPE=\"text/javascript\">$(document).ready(function () { var r =  new Raphael(\"chartCountEv\"); r.hbarchart(0, 0, 147, 90, [[" + _osBadEventsRschTopValues[0] + ", " + _osBadEventsRschTopValues[1] + ", " + _osBadEventsRschTopValues[2] + ", " + _osBadEventsRschTopValues[3] + ", " + _osBadEventsRschTopValues[4] + "]],{\"gutter\":\"30%\",\"colors\":[\"#ffffff\"]}).label([]);var r2 = new Raphael(\"chartOSuse\"); r2.hbarchart(0, 0, 130, 90, [[" + _osRschTopValues[0] + ", " + _osRschTopValues[1] + ", " + _osRschTopValues[2] + ", " + _osRschTopValues[3] + ", " + _osRschTopValues[4] + "]],{\"gutter\":\"30%\",\"colors\":[\"#ffffff\"],\"rtl\":true}).label([]);});var r3 = new Raphael(\"chartDanger\"); r3.hbarchart(0, 0, 152, 90, [[" + _vpoTopValues[0] + ", " + _vpoTopValues[1] + "," + _vpoTopValues[2] + ", " + _vpoTopValues[3] + ", " + _vpoTopValues[4] + "]], { \"gutter\": \"30%\", \"colors\": [\"#ffffff\"] }).label([\"" + _vpoTopNames[0] + "\", \"" + _vpoTopNames[1] + "\", \"" + _vpoTopNames[2] + "\", \"" + _vpoTopNames[3] + "\", \"" + _vpoTopNames[4] + "\"]);</SCRIPT>";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", someScript2);
        }

        private void SetMenu(Int32 menu)
        {
            switch (menu)
            {
                case MENU_ADMIN:
                    XmlDataSourceHeader.DataFile = "~/App_Data/MainMenu/AdminMenu.xml";
                    break;
                case MENU_USER:
                    XmlDataSourceHeader.DataFile = "~/App_Data/MainMenu/UserMenu.xml";
                    break;
                case MENU_EMPTY:
                    XmlDataSourceHeader.DataFile = "~/App_Data/MainMenu/EmptyMenu.xml";
                    break;
                case MENU_FILEMANAGER:
                    XmlDataSourceHeader.DataFile = "~/App_Data/MainMenu/FileManagerMenu.xml";
                    break;
            }
            ASPxMenu1.DataBind();
        }

        public Boolean MenuVisible
        {
            get { return ASPxMenu1.Visible; }
            set { ASPxMenu1.Visible = value; }
        }

        public Boolean MenuEnable
        {
            get { return ASPxMenu1.Enabled; }
            set { ASPxMenu1.Enabled = value; }
        }

        protected void AjaxScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {

        }

        protected void RefreshServerState()
        {
            stats stat = VmManager.GetFullStats();
            Lcpu.Text = VmManager.GetCpuInfo2();
            Lozu.Text = VmManager.GetMemImfo2();
            Lcpu0.Text = GetCpuInfoFromString(stat.cpu0);
            Lcpu1.Text = GetCpuInfoFromString(stat.cpu1);
            Lcpu2.Text = GetCpuInfoFromString(stat.cpu2);
            Lcpu3.Text = GetCpuInfoFromString(stat.cpu3);
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            RefreshServerState();
        }

        private string GetCpuInfoFromString(string p)
        {

            
            return String.Format("{0}%", p);
        }

    }
}