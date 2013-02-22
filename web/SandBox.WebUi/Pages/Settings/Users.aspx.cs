using System;
using System.Diagnostics;
using SandBox.Db;
using NLog;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages.Settings
{
    public partial class Users : BaseMainPage
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "управление пользователями";
            PageMenu  = "~/App_Data/SideMenu/Settings/SettingsMenu.xml";
          
            if (!IsUserInRole("Administrator")) 
                Response.Redirect("~/Account/Login.aspx");

            if (!IsPostBack)
            {
                UpdateTableView();
            }  
        }

        private void UpdateTableView()
        {
            gridViewUsers.DataSource = UserManager.GetUsersTableView();
            gridViewUsers.DataBind();
        }

        protected void gridViewUsers_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            Int32 userId = 0;
            try
            {
                userId = (Int32)gridViewUsers.GetRowValues(e.VisibleIndex, new[] { "UserId" });
            }
            catch (Exception)
            {
                userId = 0;
            }

            if (userId == 0) return;
            if (e.ButtonID == "cbEdit")
            {
                EditUser(userId);
                return;
            }
            if (e.ButtonID == "cbDelete")
            {
                DeleteUser(userId);
                gridViewUsers.DataSource = UserManager.GetUsersTableView();
                gridViewUsers.DataBind();
                return;
            }
        }

        private void EditUser(Int32 userId)
        {
            Debug.Print("Edit user id: " + userId);
        }

        private void DeleteUser(Int32 userId)
        {
            UserManager.DeleteUser(userId);
        }

        protected void gridViewUsers_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
        {
            
        }


    }//end class
}//end namespace