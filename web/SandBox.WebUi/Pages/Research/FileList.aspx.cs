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
    public partial class FileList : BaseMainPage
    {      
        protected new void Page_Load(object sender, EventArgs e)
        {

            base.Page_Load(sender, e);
            PageTitle = "Образ файловой системы";
            PageMenu  = "~/App_Data/SideMenu/Research/ResearchMenu.xml";
            ASPxLabel1.Text = "Исследование: "+ ResearchManager.GetResearch((int)Session["rsch"]).ResearchName;
            GVFileList.DataSource = ReportManager.GetFiles2((int)Session["rsch"]);
            GVFileList.DataBind();
            if (!IsPostBack)
            {
                Debug.Print("Get research id = " + Request.QueryString["research"]);
            }
        }
    }//end class
}//end namespace