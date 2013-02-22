using System;
using System.Web.Security;

namespace SandBox.WebUi.Account {
	public partial class ChangePassword : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
		
        }

        protected void btnChangePassword_Click(object sender, EventArgs e) {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            if (!Membership.ValidateUser(user.UserName, tbCurrentPassword.Text)) {
                tbCurrentPassword.ErrorText = "Old Password is not valid";
                tbCurrentPassword.IsValid = false;
            }
            else if (!user.ChangePassword(tbCurrentPassword.Text, tbPassword.Text)) {
                tbPassword.ErrorText = "Password is not valid";
                tbPassword.IsValid = false;
            }
            else
                Response.Redirect("~/");
        }
    }
}