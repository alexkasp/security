using System;

namespace SandBox.WebUi
{
    public partial class Information : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((MainMaster)Master).SetMenuFile("~/App_Data/SideMenu/Information/InformationMenu.xml");
            Response.Redirect("~/Pages/Info/Resources.aspx");
        }
    }
}