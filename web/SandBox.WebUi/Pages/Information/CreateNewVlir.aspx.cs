using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SandBox.Db;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages.Information
{
    public partial class CreateNewVlir : BaseMainPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Добавление новой ВЛИР";
            PageMenu = "~/App_Data/SideMenu/Information/InformationMenu.xml";
            cbSystem.DataSource = VmManager.GetVmSystemDescriptionList();
            cbSystem.DataBind();
        }

        protected void BAdd_Click(object sender, EventArgs e)
        {
            List<String> list = VmManager.GetVmSystemDescriptionList();
            String value = list[cbSystem.SelectedIndex];
            Int32 system = VmManager.GetSystem(value).System;
            String newName = (tbLir.Text).Replace(" ", "_");
            VmManager.AddVm(newName, 3, system, UserId, 0,"null", tbLirMac.Text);
        }
    }
}