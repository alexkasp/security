using System;
using SandBox.Db;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages.Research
{
    public partial class ReportList : BaseMainPage
    {
        public Db.Research Rs;

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Отчет";
            PageMenu  = "~/App_Data/SideMenu/Research/ResearchMenu.xml";

            Int32 researchId = Convert.ToInt32(Request.QueryString["research"]);
            //Session.Clear();
            Session["rsch"] = researchId;
            Rs = ResearchManager.GetResearch(researchId);
            if (Rs == null)
            {
                Response.Redirect("~/Error");
            }

            gridViewReports.DataSource = ReportManager.GetReports(researchId);
            gridViewReports.DataBind();

            if (!IsPostBack)
            {
                
            }
        }
    }//end class
}//end namespace