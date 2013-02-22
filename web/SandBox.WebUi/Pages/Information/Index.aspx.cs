using System;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages.Information
{
    public partial class Index : BaseMainPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Информация";
            PageMenu = "~/App_Data/SideMenu/Information/InformationMenu.xml";

            Response.Redirect("~/Pages/Information/Resources.aspx");
        }
    }
}