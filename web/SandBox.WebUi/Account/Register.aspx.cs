using System;
using System.Web;
using System.Web.Security;

namespace SandBox.WebUi.Account 
{
    public partial class Register : System.Web.UI.Page 
    {
        protected void Page_Load(object sender, EventArgs e) 
        {
            Title = "*** | Добавление пользователя";  
        }

        protected void btnCreateUser_Click(object sender, EventArgs e) {
            try {
                MembershipUser user = Membership.CreateUser(tbUserName.Text, tbPassword.Text);
                Response.Redirect("~/Pages/Settings/Users.aspx");
            }
            catch (MembershipCreateUserException exc) 
            {
                if (exc.StatusCode == MembershipCreateStatus.InvalidPassword) {
                    tbPassword.ErrorText = exc.Message;
                    tbPassword.IsValid = false;
                }
                else {
                    tbUserName.ErrorText = exc.Message;
                    tbUserName.IsValid = false;
                }
            }
        }
    }
}