using System;
using System.Collections.Generic;
using SandBox.Db;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages.Information
{
    public partial class CreateEtalonMachine : BaseMainPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Добавление новой ЛИР";
            PageMenu  = "~/App_Data/SideMenu/Information/InformationMenu.xml";

            if (!IsPostBack)
            {
                cbSystem.DataSource = VmManager.GetVmSystemDescriptionList();
                cbSystem.DataBind();

                cbEnvType.DataSource = VmManager.GetFreeEnvTypes();
                cbEnvType.DataBind();
            }
        }

        protected void BtnCreateClick(object sender, EventArgs e)
        {
             List<String> list = VmManager.GetVmSystemDescriptionList();
             List<Int32> nums = VmManager.GetFreeEnvTypes();
             String value = list[cbSystem.SelectedIndex];
             Int32 system = VmManager.GetSystem(value).System;
             String newName = (tbLir.Text).Replace(" ", "_");
             Int32 envType = Convert.ToInt32(cbEnvType.Value);
             VmManager.AddVm(newName, 1, system, UserId, envType);

            Response.Redirect("~/Pages/Information/Resources.aspx");
        }
    }//end class
}//end namespace