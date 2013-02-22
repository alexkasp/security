using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SandBox.Connection;
using SandBox.Db;

namespace SandBox.WebUi.Pages.Info
{
    public partial class RegisterMachine : System.Web.UI.Page
    {
        private Int32 _userId;
        private static ConnectionClientEx _client;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "*** | Добавление новой ЛИР";
            _client = ConnectionClientEx.Instance;

            if (Master != null) ((MainMaster)Master).SetMenuFile("~/App_Data/SideMenu/Information/InformationMenu.xml");

            ddList.DataSource = VmManager.GetVmSystemDescriptionList();
            ddList.DataBind();

            if (!IsPostBack)
            {
                _userId = (Int32)UserManager.GetUser(User.Identity.Name).ProviderUserKey;
            }  
        }

        protected void BtnCreateClick(object sender, EventArgs e)
        {
             String name = tbLir.Text == String.Empty ? "newVm" : tbLir.Text;
             List<String> list = VmManager.GetVmSystemDescriptionList();
             String value = list[ddList.SelectedIndex];
             Int32 system = VmManager.GetSystem(value).System;

             VmManager.AddVm(tbLir.Text, 1, system, _userId);
            
            
            /*Packet packet = new Packet { Type = PacketType.VM_CREATE, Direction = PacketDirection.REQUEST };
            packet.AddParameter(Encoding.UTF8.GetBytes(etalonName));
            packet.AddParameter(Encoding.UTF8.GetBytes(newName));
            _client.Send(packet.ToByteArray());*/

            Response.Redirect("~/Pages/Info/Resources.aspx");
        }
    }
}