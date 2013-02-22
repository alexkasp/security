using System;

namespace SandBox.WebUi
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (User.IsInRole("Administrator"))
                {
                    ((MainMaster)Master).SetMenuFile("~/App_Data/SideMenu/Settings/SettingsMenu.xml");
                    Response.Redirect("~/Pages/Settings/Users.aspx");
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
        }
    }
}