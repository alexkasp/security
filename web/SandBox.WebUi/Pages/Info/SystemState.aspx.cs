using System;

namespace SandBox.WebUi.Pages.Info
{
    public partial class SystemState : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MainMaster)Master).SetMenuFile("~/App_Data/SideMenu/Information/InformationMenu.xml");
        }
    }
}