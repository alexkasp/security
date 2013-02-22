using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SandBox.Db;

namespace SandBox.WebUi {
    public partial class RootMaster : MasterPage
    {
        private const Int32 MENU_ADMIN          = 0;
        private const Int32 MENU_USER           = 1;
        private const Int32 MENU_EMPTY          = 2;
        private const Int32 MENU_FILEMANAGER    = 3;

        
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
            //string someScript = "";
            string someScript2 = "";
            //someScript = "<SCRIPT TYPE=\"text/javascript\">$(document).ready(function () { var r2 = new Raphael(\"chartOSuse\"); r2.hbarchart(0, 0, 130, 90, [[155, 55, 55, 32, 5]],{\"gutter\":\"30%\",\"colors\":[\"#ffffff\"],\"rtl\":true}).label([]);});  </SCRIPT>";
            someScript2 = " <SCRIPT TYPE=\"text/javascript\">$(document).ready(function () { var r =  new Raphael(\"chartCountEv\"); r.hbarchart(0, 0, 147, 90, [[155, 55, 55, 32, 5]],{\"gutter\":\"30%\",\"colors\":[\"#ffffff\"]}).label([]);var r2 = new Raphael(\"chartOSuse\"); r2.hbarchart(0, 0, 130, 90, [[35, 85, 15, 2, 51]],{\"gutter\":\"30%\",\"colors\":[\"#ffffff\"],\"rtl\":true}).label([]);});var r3 = new Raphael(\"chartDanger\"); r3.hbarchart(0, 0, 152, 90, [[5, 7, 3, 15, 5]], { \"gutter\": \"30%\", \"colors\": [\"#ffffff\"] }).label([\"calc\", \"regedit\", \"injector\", \"testvpo\", \"explorer\"]);</SCRIPT>";
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