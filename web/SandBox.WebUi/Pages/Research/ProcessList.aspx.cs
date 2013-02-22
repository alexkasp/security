using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.Web.ASPxEditors;
using SandBox.Connection;
using SandBox.Db;
using SandBox.Log;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages.Research
{
    public partial class ProcessList : BaseMainPage
    {
        private static Int32 _userId;

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Список процессов";
            PageMenu  = "~/App_Data/SideMenu/Research/ResearchMenu.xml";
            ASPxLabel1.Text = "Исследование: " + ResearchManager.GetResearch((int)Session["rsch"]).ResearchName;
            GVProcesses.DataSource = ReportManager.GetProcesses2((int)Session["rsch"]);
            GVProcesses.DataBind();
            if (!IsPostBack)
            {
                Debug.Print("Get research id = " + Request.QueryString["research"]);

            }

        }

        protected void GVProcesses_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }

       
    }//end class
}//end namespace