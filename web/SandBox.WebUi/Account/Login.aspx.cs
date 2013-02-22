using System;
using System.Diagnostics;
using System.Web.Security;
using SandBox.Db;
using SandBox.Log;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Account {
    public partial class Login : BaseEmptyPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "вход";

            if (DbManager.GetConnectionStatus())
            {
                if (!Communication)
                {
                    Response.Redirect("~/Pages/Error/CommunicationError.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Pages/Error/DbError.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            Debug.Print("Communication: " + Application.Get("Communication"));
            
                   if (Membership.ValidateUser(tbUserName.Text, tbPassword.Text))
                    {
                       FormsAuthentication.SetAuthCookie(tbUserName.Text, rememberme.Checked);
                        MLogger.LogTo(Level.TRACE, false, "New user logged as '" + tbUserName.Text + "'");
                        Response.Redirect("~/Pages/Research/Current.aspx");
                    }
                    else
                    {
//                        tbUserName.ErrorText = " ";
                        tbUserName.IsValid = false;
                    }
        }
    }//end class
}//end namespace