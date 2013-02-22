using System;
using System.Diagnostics;
using System.Text;
using SandBox.Connection;
using SandBox.Db;
using SandBox.Log;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages.Info
{
    public partial class Resources : BaseMainPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Ресурсы";
            PageMenu = "~/App_Data/SideMenu/Information/InformationMenu.xml";
            
            if (!IsPostBack)
            {
                UpdateTable();
                foreach (var machine in VmManager.GetVms())
                {
                    GetVmStatus(machine.Id);
                }
            }  
        }

        private void UpdateTable()
        {
            gridViewMachines.KeyFieldName = "Id";
            gridViewMachines.DataSource = VmManager.GetVmsTableView();
            gridViewMachines.DataBind();
        }

        protected void UpdateTimerTick(object sender, EventArgs e)
        {
            UpdateTable();
        }

        protected void CallbackPanelCreateCallback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            Int32 machineId = Convert.ToInt32(e.Parameter);
            Session["machineId"] = machineId;
            if (machineId == 0) return;
            litText.Text = "Создание влир на основе " + VmManager.GetVmName(machineId);
            tbNewVmName.Text = String.Empty;
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
            Packet packet = new Packet {Type = PacketType.VM_STATUS, Direction = PacketDirection.REQUEST};
            packet.AddParameter(Encoding.UTF8.GetBytes(machineName));
            SendPacket(packet.ToByteArray());
        }

        private static void StartVm(Int32 id)
        {  
            String machineName = VmManager.GetVmName(id);
            Debug.Print("Start vm: " + machineName);
            Packet packet = new Packet {Type = PacketType.VM_START, Direction = PacketDirection.REQUEST};
            packet.AddParameter(Encoding.UTF8.GetBytes(machineName));
            SendPacket(packet.ToByteArray());
        }

        private static void StopVm(Int32 id)
        {
            String machineName = VmManager.GetVmName(id);
            Packet packet = new Packet {Type = PacketType.VM_STOP, Direction = PacketDirection.REQUEST};
            packet.AddParameter(Encoding.UTF8.GetBytes(machineName));
            SendPacket(packet.ToByteArray());
        }

        private void DeleteVm(Int32 id)
        {
            String machineName = VmManager.GetVmName(id);
            Packet packet = new Packet { Type = PacketType.VM_DELETE, Direction = PacketDirection.REQUEST };
            packet.AddParameter(Encoding.UTF8.GetBytes(machineName));
            SendPacket(packet.ToByteArray());
            VmManager.DeleteVm(id);
            UpdateTable();
        }

        private void CreateVm(String etalonName, String newName, Int32 type, Int32 system, Int32 userId)
        {
            VmManager.AddVm(newName, type, system, userId);
            UpdateTable();
            Packet packet = new Packet { Type = PacketType.VM_CREATE, Direction = PacketDirection.REQUEST };
            packet.AddParameter(Encoding.UTF8.GetBytes(etalonName));
            packet.AddParameter(Encoding.UTF8.GetBytes(newName));
            SendPacket(packet.ToByteArray());
        }

        protected void BtnCreateClick(object sender, EventArgs e)
        {
            litText.Text = String.Empty;
            Int32 machineId = Convert.ToInt32(Session["machineId"]);
            String machineName = VmManager.GetVmName(machineId);
            String newMachineName   = tbNewVmName.Text == String.Empty ? "newVm" : tbNewVmName.Text;
            tbNewVmName.Text = String.Empty;
            if (machineId == 0) return;
            CreateVm(machineName, newMachineName, 2, VmManager.GetVm(machineId).System, UserId);
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