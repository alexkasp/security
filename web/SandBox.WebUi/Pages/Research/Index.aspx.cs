using System;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages.Research
{
    public partial class Index : BaseMainPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Исследования";
            PageMenu  = "~/App_Data/SideMenu/Research/ResearchMenu.xml";
            Response.Redirect("~/Pages/Research/Current.aspx");
        }
    }
}