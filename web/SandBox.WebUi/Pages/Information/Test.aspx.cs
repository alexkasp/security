using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SandBox.Connection;
using SandBox.Db;
using SandBox.Log;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages.Information
{
    public partial class Test : BaseMainPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Тест";
            PageMenu = "~/App_Data/SideMenu/Information/InformationMenu.xml";

            if (!IsPostBack)
            {
                
            }
            else
            {
                Debug.Print("Postback");
            }

            UpdateInfo();

            gridViewMachines.KeyFieldName = "Id";
            gridViewMachines.DataSource = VmManager.GetVmsTableView();
            gridViewMachines.DataBind();
        }

        protected void UpdateInfo()
        {
            IQueryable<Vm> vms = IsUserInRole("Administrator") ? VmManager.GetVms() : VmManager.GetVms(UserId);

            Int32 freeVms = 0;
            Int32 usedVms = 0;

            foreach (var vm in vms)
            {
                if (vm.State == (Int32)VmManager.State.STARTED) usedVms++;
                if (vm.State == (Int32)VmManager.State.STOPPED) freeVms++;
            }

            String txt = "Используется " + usedVms + " ЛИР, " + freeVms + " ЛИР свободно";

            if ((usedVms == 0) && (freeVms == 0))
            {
                txt = "Информации по ЛИР недоступна";
            }
            else if (usedVms == 0)
            {
                txt = "Свободно " + freeVms + " ЛИР";
            }
            else if (freeVms == 0)
            {
                txt = "Используется " + usedVms + " ЛИР, свободных ЛИР нет";
            }

            labelVmInfo.Text = txt;
        }

        protected void CallbackPanelDeleteCallback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            Int32 machineId = Convert.ToInt32(e.Parameter);
            Session["machineId"] = machineId;
            if (machineId == 0) return;
            deleteText.Text = "Вы точно хотите удалить " + VmManager.GetVmName(machineId) + "?";
        }


        private void GetVmStatus(Int32 id)
        {
            String machineName = VmManager.GetVmName(id);
            MLogger.LogTo(Level.TRACE, false, "Get status for " + machineName);

            VmManager.UpdateVmState(machineName, (Int32)VmManager.State.UPDATING);

            Packet packet = new Packet { Type = PacketType.CMD_VM_STATUS, Direction = PacketDirection.REQUEST };
            packet.AddParameter(Encoding.UTF8.GetBytes(machineName));
            SendPacket(packet.ToByteArray());
        }

        private static void StartVm(Int32 id)
        {
            String machineName = VmManager.GetVmName(id);
            Debug.Print("Start vm: " + machineName);
            Packet packet = new Packet { Type = PacketType.CMD_VM_START, Direction = PacketDirection.REQUEST };
            packet.AddParameter(Encoding.UTF8.GetBytes(machineName));
            SendPacket(packet.ToByteArray());
        }

        private static void StopVm(Int32 id)
        {
            String machineName = VmManager.GetVmName(id);
            Packet packet = new Packet { Type = PacketType.CMD_VM_STOP, Direction = PacketDirection.REQUEST };
            packet.AddParameter(Encoding.UTF8.GetBytes(machineName));
            SendPacket(packet.ToByteArray());
        }

        private void DeleteVm(Int32 id)
        {
            String machineName = VmManager.GetVmName(id);
            Packet packet = new Packet { Type = PacketType.CMD_VM_DELETE, Direction = PacketDirection.REQUEST };
            packet.AddParameter(Encoding.UTF8.GetBytes(machineName));
            SendPacket(packet.ToByteArray());

            VmManager.DeleteVm(id);
            //UpdateTableView();
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            deleteText.Text = String.Empty;
            Int32 machineId = Convert.ToInt32(Session["machineId"]);
            if (machineId == 0) return;
            DeleteVm(machineId);
        }

        [System.Web.Services.WebMethod]
        public static void StartMachine(string id)
        {
            Int32 machineId = Convert.ToInt32(id);
            StartVm(machineId);
        }

        [System.Web.Services.WebMethod]
        public static void StopMachine(string id)
        {
            Int32 machineId = Convert.ToInt32(id);
            StopVm(machineId);
        }
    }//end class
}//end namespace