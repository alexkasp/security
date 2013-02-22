using System;
using SandBox.Db;

namespace SandBox.WebUi
{
    public partial class RoleTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Administrator"))
                {
                    TestLabel.Text = "User authenticated, user id=" + UserManager.GetUser(User.Identity.Name).ProviderUserKey + ", role=Administrator";
                }
                else
                {
                    TestLabel.Text = "User authenticated, user id=" + UserManager.GetUser(User.Identity.Name).ProviderUserKey + ", role=Unknown";
                }
            }
            else
            {
                TestLabel.Text = "User not authenticated";
            }
        }
    }
}