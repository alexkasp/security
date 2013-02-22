using System;
using System.Collections.Generic;
using SandBox.Db;
using SandBox.Log;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Account
{
    public partial class AddUser : BaseMainPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Добавление нового пользователя";
            PageMenu = "~/App_Data/SideMenu/Settings/SettingsMenu.xml";

            if (!IsPostBack)
            {
                List<string> rolesList = UserManager.GetRoleList();
                cbRole.DataSource = rolesList;
                cbRole.DataBind();
            }
        }

        protected void BtnCreateClick(object sender, EventArgs e)
        {
            String username = tbUserName.Text;
            String password = tbPassword.Text;
            String confirmPassword = tbPasswordConfirm.Text;
            String role = cbRole.Value.ToString();
            Int32 roleId = UserManager.GetRole(role).RoleId;
            UserManager.CreateUser(username, password, roleId);
            Response.Redirect("~/Pages/Settings/Users.aspx");
        }
    }//end class
}//end namespace