using System;
using System.Text;
using SandBox.Connection;
using SandBox.Db;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages.Information
{
    public partial class CreateMachine : BaseMainPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Создание ВЛИР";
            PageMenu = "~/App_Data/SideMenu/Information/InformationMenu.xml";

            if (!IsPostBack)
            {
                cbEtalon.DataSource = VmManager.GetVmsEtalonList();
                cbEtalon.DataBind();
                cbEtalon.SelectedIndex = 0;
            }
        }

        protected void BtnCreateClick(object sender, EventArgs e)
        {
            if (tbLir.Text != String.Empty)
            {
                Vm etalon = VmManager.GetVmsEtalonByNumber(cbEtalon.SelectedIndex);
                String etalonName = etalon.Name;
                Int32 etalonEnvType = Convert.ToInt32(etalon.EnvType);
                String newName = (tbLir.Text).Replace(" ", "_");
                if (!IsNameInBase(newName))
                {
                    VmManager.AddVm(newName, 2, etalon.System, UserId, etalonEnvType);
                    Packet packet = new Packet { Type = PacketType.CMD_VM_CREATE, Direction = PacketDirection.REQUEST };
                    packet.AddParameter(Encoding.UTF8.GetBytes(etalonName));
                    packet.AddParameter(Encoding.UTF8.GetBytes(newName));
                    SendPacket(packet.ToByteArray());
                    //Vm newVm = VmManager.GetVm(newName);
                    VmManager.UpdateVmState(newName, (int)VmManager.State.UNAVAILABLE);
                    Response.Redirect("~/Pages/Information/Resources.aspx");
                }
                else
                {
                    LValidation.Visible = true;
                    LValidation.ForeColor = System.Drawing.Color.Red;
                    LValidation.Text = "Такое имя уже сущемтвует!";
                }
            }
            else
            {
                LValidation.Visible = true;
                LValidation.ForeColor = System.Drawing.Color.Red;
                LValidation.Text = "Имя не может быть пустым!";
            }
        }

        protected void tbLir_TextChanged(object sender, EventArgs e)
        {
            if (LValidation.Visible) LValidation.Visible = false;
        }

        private bool IsNameInBase(string p)
        {
            var vm = VmManager.GetVmNameList();
            return vm.Contains(p);
        }
    }
}