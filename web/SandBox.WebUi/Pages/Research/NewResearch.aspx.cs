using System;
using System.Collections.Generic;
using SandBox.Db;
using SandBox.Log;
using SandBox.WebUi.Base;
using System.Linq;
using DevExpress.Web.ASPxGridView;
using System.Security.Cryptography;
using SandBox.Connection;
using System.Text;



namespace SandBox.WebUi.Pages.Research
{
    public partial class NewResearch : BaseMainPage
    {
        
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Создание нового исследования";
            PageMenu = "~/App_Data/SideMenu/Research/ResearchMenu.xml";
            //   List<string> vmList = IsUserInRole("Administrator") ? VmManager.GetVmReadyNameList() : VmManager.GetVmReadyNameList(UserId);
            List<string> vmList = IsUserInRole("Administrator") ? VmManager.GetVmReadyForResearch() : VmManager.GetVmReadyForResearch(UserId);
            //ASPxComboBox2.Items.Clear();
            if (ASPxComboBox3.SelectedIndex == -1)
            {
                ASPxTextBox3.Enabled = false;
            }
            else
            {
                ASPxTextBox3.Enabled = true;
            }

            if (vmList.Count < 1)
            {
                cbMachine.Enabled = false;
                linkCreateNewVm.Visible = true;
                linkRegisterNewVm.Visible = true;
            }
            else
            {
                linkCreateNewVm.Visible = false;
                linkRegisterNewVm.Visible = false;
                cbMachine.Enabled = true;
                cbMachine.DataSource = vmList;
                cbMachine.DataBind();
            }
            if (!IsPostBack)
            {
                //ASPxComboBox3.DataSource = from item in new List<string> { "HKEY_CLASSES_ROOT", "HKEY_CURRENT_USER", "HKEY_LOCAL_MACHINE", "HKEY_USERS", "HKEY_CURRENT_CONFIG" }
                //                           select new { hkey = item };
                //ASPxComboBox3.DataBind();

                CBNetActiv.Items.AddRange(TaskManager.GetTasksDescrByClassification(1).ToList());
                CBFileActiv.Items.AddRange(TaskManager.GetTasksDescrByClassification(2).ToList());
                CBRegActiv.Items.AddRange(TaskManager.GetTasksDescrByClassification(3).ToList());
                CBProcActiv.Items.AddRange(TaskManager.GetTasksDescrByClassification(4).ToList());
                //ASPxComboBox2.Items.AddRange(TaskManager.GetTasksDescrByClassification(1).ToList());

                ASPxComboBox1.DataSource = ReportManager.GetModules();
                ASPxComboBox1.DataBind();
                List<string> mlwrList = MlwrManager.GetMlwrPathList();

                cbMachine.DataSource = vmList;
                cbMachine.DataBind();
                if (vmList.Count > 0) cbMachine.SelectedIndex = 0;

                cbMalware.DataSource = mlwrList;
                cbMalware.DataBind();
                if (vmList.Count > 0) cbMalware.SelectedIndex = 0;

                spinTime.AllowNull = false;
                spinTime.AllowUserInput = false;
                spinTime.Value = 1;
                Session["tasks"] = new List<TaskStruct>();
            }
            else
            {
                //var test = this.GetTableLookFromSession(1);
            }
        }

        protected void BtnCreateClick(object sender, EventArgs e)
        {
           

            Vm vm = VmManager.GetVm(cbMachine.Value.ToString());
            Mlwr mlwr = MlwrManager.GetMlwr(cbMalware.Value.ToString());
            Int32 timeLeft = Convert.ToInt32(spinTime.Value);

            String NewName="";
            Int32 researchVmData = 0;
            Int32 researchId = 0;
            if (vm.Type == 1)
            {
                NewName = GenRandString(20);
                VmManager.AddVm(NewName, 2, vm.System, UserId, 0);
                Vm newvm = VmManager.GetVm(NewName);
                researchVmData = ResearchManager.AddResearchVmData(NewName, 2, newvm.System, 0, newvm.EnvMac, newvm.EnvIp, newvm.Description);
                researchId = ResearchManager.AddResearch(UserId, mlwr.Id, newvm.Id, researchVmData, timeLeft, tbLir.Text);
            }
            else
            {
                researchVmData = ResearchManager.AddResearchVmData(vm.Name, vm.Type, vm.System, vm.EnvType, vm.EnvMac, vm.EnvIp, vm.Description);
                researchId = ResearchManager.AddResearch(UserId, mlwr.Id, vm.Id, researchVmData, timeLeft, tbLir.Text);
            }
          

            AddTasks(researchId, vm.EnvId);

            #region добавление события на остановку исследования
            if (CbStopEvent.Checked)
            {
                //int sign = ASPxComboBox1.Text == "Критически важное" ? 0 : 1;
                int module = ResearchManager.GetModuleIdByDescr(ASPxComboBox1.Text);
                int evt = ResearchManager.GetEventIdByDescr(ASPxComboBox4.Text);
                string dest = ASPxTextBox4.Text;
                string who = ASPxTextBox5.Text;
                if (module != -1 && evt != -1 && dest != String.Empty && who != String.Empty)
                {
                    ReportManager.InsertStopEvent(researchId, module, evt, dest, who);

                }
            }
            #endregion

            MLogger.LogTo(Level.TRACE, false, "Create research '" + tbLir.Text + "' by user '" + UserManager.GetUser(UserId).UserName + "'");
            if (CreateOrStartVm(vm.Name,NewName))
                Current.StartResearch(String.Format("{0}", researchId));
            Response.Redirect("~/Pages/Research/Current.aspx");
        }

        public static string GenRandString(int length)
        {
            byte[] randBuffer = new byte[length];
            RandomNumberGenerator.Create().GetBytes(randBuffer);
            String prepare = System.Convert.ToBase64String(randBuffer).Remove(length).Replace("+", "p");
            prepare = prepare.Replace("/","s");
            prepare = prepare.Replace("\\","w");
            return prepare;
        }

        protected bool CreateOrStartVm(String VmName,String NewName)
        {
            Vm baseVm = VmManager.GetVm(VmName);

            if (baseVm.Type == 1)
            {

                String newName = NewName;// 
               
                Packet packet = new Packet { Type = PacketType.CMD_VM_CREATE, Direction = PacketDirection.REQUEST };
                packet.AddParameter(Encoding.UTF8.GetBytes(VmName));
                packet.AddParameter(Encoding.UTF8.GetBytes(newName));
                SendPacket(packet.ToByteArray());
                //Vm newVm = VmManager.GetVm(newName);
                VmManager.UpdateVmState(newName, (int)VmManager.State.UNAVAILABLE);
                return false;
            }
            else
            {
                if (baseVm.State == Convert.ToInt32(VmManager.State.STARTED))
                {
                    return true;
                }
                else
                {
                    Packet packet = new Packet { Type = PacketType.CMD_VM_START, Direction = PacketDirection.REQUEST };
                    packet.AddParameter(Encoding.UTF8.GetBytes(VmName));
                    SendPacket(packet.ToByteArray());
                    return false;
                }
            }

        }

        private void AddTasks(Int32 researchId, int EnvId)
        {
            //String hideFilePar = tbHideFile.Text;
            //String lockFilePar = tbLockDelete.Text;
            //String hideRegistryPar = tbHideRegistry.Text;
            //String hideProcessPar = tbHideProcess.Text;
            String setSignaturePar = tbSetSignature.Text;
            String setExtensionPar = tbSetExtension.Text;
            //String setBandwidthPar = tbSetBandwidth.Text;

            //if (hideFilePar != String.Empty) TaskManager.AddTask(researchId, 1, hideFilePar);
            //if (lockFilePar != String.Empty) TaskManager.AddTask(researchId, 2, lockFilePar);
            //if (hideRegistryPar != String.Empty) TaskManager.AddTask(researchId, 3, hideRegistryPar);
            //if (hideProcessPar != String.Empty) TaskManager.AddTask(researchId, 4, hideProcessPar);
            if (setSignaturePar != String.Empty) TaskManager.AddTask(researchId, 5, setSignaturePar);
            if (setExtensionPar != String.Empty) TaskManager.AddTask(researchId, 6, setExtensionPar);
            //if (setBandwidthPar != String.Empty) TaskManager.AddTask(researchId, 7, setBandwidthPar);
            if (ASPxTextBox2.Text != String.Empty) TaskManager.AddTask(researchId, 16, ASPxTextBox2.Text);
            if (ASPxComboBox3.SelectedIndex != -1)
            {
                int key = ASPxComboBox3.SelectedIndex;
                string subkey = "";
                if (ASPxTextBox3.Text != "") subkey = ASPxTextBox3.Text;
                TaskManager.AddTask(researchId, 17, String.Format("{0}{1}",key,subkey));
            }
            if (ASPxCheckBox1.Checked) TaskManager.AddTask(researchId, 15, "ON");

            var session = Session["tasks"] as List<TaskStruct>;
            foreach (var task in session)
            {
                if (task.Description != String.Empty && task.Value!=String.Empty/*ASPxTextBox1.Text != task.Value*/)
                    TaskManager.AddTask(researchId, TaskManager.GetTaskTypeByDescription(task.Description), task.Value);
            }
            //if (ASPxComboBox2.SelectedItem!=null)
            //if (ASPxComboBox2.SelectedItem.Text != String.Empty && ASPxTextBox1.Text != String.Empty)
            //    TaskManager.AddTask(researchId, TaskManager.GetTaskTypeByDescription(ASPxComboBox2.SelectedItem.Text), ASPxTextBox1.Text);
            try
            {
                
                TaskManager.AddCommand(EnvId, tbSetCommand.Text, tbSetCommandParams.Text, Int32.Parse(startEmulationTime.Text));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected void BAddNetTask_Click(object sender, EventArgs e)
        {
            if (CBNetActiv.SelectedItem.Text != String.Empty && TBNetTaskValue.Text != String.Empty)
            {
                BAddTask(CBNetActiv.SelectedItem.Text, TBNetTaskValue.Text);
                Page.DataBind();
            }
        }

        private void BAddTask(string description, string value)
        {
            var session = Session["tasks"] as List<TaskStruct>;
            if (session != null)
                session.Add(new TaskStruct { Description = description, Value = value });
            else
            {
                Session["tasks"] = new List<TaskStruct>();
                session.Add(new TaskStruct { Description = CBNetActiv.SelectedItem.Text, Value = TBNetTaskValue.Text });
            }
            switch (TaskManager.GetClassTypeByTaskType(TaskManager.GetTaskTypeByDescription(description)))
            {
                //case 1:
                //    {
                //        FillGridView(ASPxGridView1, 1);
                //        break;
                //    }
                //case 2:
                //    {
                //        FillGridView(ASPxGridView2, 2);
                //        break;
                //    }
                //case 3:
                //    {
                //        FillGridView(ASPxGridView3, 3);
                //        break;
                //    }
                //case 4:
                //    {
                //        FillGridView(ASPxGridView4, 4);
                //        break;
                //    }
                default:
                    {
                        FillGridView(ASPxGridView1, 1);
                        FillGridView(ASPxGridView2, 2);
                        FillGridView(ASPxGridView3, 3);
                        FillGridView(ASPxGridView4, 4);
                        break;
                    }
            }
            Page.DataBind();
        }

        private List<TaskStruct> GetTableLookFromSession(int taskClassType)
        {
            List<TaskStruct> res = new List<TaskStruct>();
            var session = Session["tasks"] as List<TaskStruct>;
            if (session == null) return null;
            var tasks = from t in session
                        select new { type = TaskManager.GetTaskTypeByDescription(t.Description), value = t.Value, description = t.Description };
            foreach (var t in tasks)
            {
                if (TaskManager.GetClassTypeByTaskType(t.type) == taskClassType)
                {
                    res.Add(new TaskStruct { Description = t.description, Value = t.value });
                }
            }
            return res;
        }

        private void FillGridView(ASPxGridView gv, int taskClassType)
        {
            try
            {
                IQueryable<TaskStruct> test = GetTableLookFromSession(taskClassType).AsQueryable();
                gv.DataSource = from t in test
                                select new { f1 = t.Description, f2 = t.Value };
                gv.DataBind();
            }
            catch { }
        }

        private void FillAllGridViews()
        {
            FillGridView(ASPxGridView1, 1);
            FillGridView(ASPxGridView2, 2);
            FillGridView(ASPxGridView3, 3);
            FillGridView(ASPxGridView4, 4);
        }

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string value = String.Format("{0}", e.Keys["Value"]);
            Page.DataBind();

        }

        protected void BAddFileTask_Click(object sender, EventArgs e)
        {
            if (CBFileActiv.SelectedItem.Text != String.Empty && TBNFileTaskValue.Text != String.Empty)
            {
                BAddTask(CBFileActiv.SelectedItem.Text, TBNFileTaskValue.Text);
                Page.DataBind();
            }

        }

        protected void BAddRegTask_Click(object sender, EventArgs e)
        {
            if (CBRegActiv.SelectedItem.Text != String.Empty && TBNRegTaskValue.Text != String.Empty)
            {
                BAddTask(CBRegActiv.SelectedItem.Text, TBNRegTaskValue.Text);
                Page.DataBind();
            }

        }

        protected void BAddProcTask_Click(object sender, EventArgs e)
        {
            if (CBProcActiv.SelectedItem.Text != String.Empty && TBProcTaskValue.Text != String.Empty)
            {
                BAddTask(CBProcActiv.SelectedItem.Text, TBProcTaskValue.Text);
                Page.DataBind();
            }
        }

        private TaskStruct GetSelectedTask(int p)
        {
            TaskStruct res = new TaskStruct();
            switch (p)
            {
                case 1:
                    {
                        res = GetTSFromGV(ASPxGridView1);
                        break;
                    }
                case 2:
                    {
                        res = GetTSFromGV(ASPxGridView2);
                        break;
                    }
                case 3:
                    {
                        res = GetTSFromGV(ASPxGridView3);
                        break;
                    }
                case 4:
                    {
                        res = GetTSFromGV(ASPxGridView4);
                        break;
                    }
                default:
                    {
                        return res;
                    }
            }
            return res;
        }

        private TaskStruct GetTSFromGV(ASPxGridView gridView)
        {
            TaskStruct res = new TaskStruct();
            res.Description = (string)gridView.GetRowValues(gridView.FocusedRowIndex, "f1");
            res.Value = (string)gridView.GetRowValues(gridView.FocusedRowIndex, "f2");
            Page.DataBind();
            return res;
        }

        private int DeleteTaskFromSession(TaskStruct task)
        {
            var session = Session["tasks"] as List<TaskStruct>;
            return session.RemoveAll(item => ((item.Description == task.Description)&&(item.Value==task.Value)));
        }

        protected void BDelNetTask_Click(object sender, EventArgs e)
        {
            TaskStruct task = GetSelectedTask(1);
            if (DeleteTaskFromSession(task) > 0)
            {
                //FillGridView(ASPxGridView1, 1);
                FillAllGridViews();
            }
            Page.DataBind();

        }

        protected void BDelFileTask_Click(object sender, EventArgs e)
        {
            TaskStruct task = GetSelectedTask(2);
            if (DeleteTaskFromSession(task) > 0)
            {
                FillAllGridViews(); //FillGridView(ASPxGridView2, 2);
            }
            Page.DataBind();
        }

        protected void BDelRegTask_Click(object sender, EventArgs e)
        {
            TaskStruct task = GetSelectedTask(3);
            if (DeleteTaskFromSession(task) > 0)
            {
                FillAllGridViews(); //FillGridView(ASPxGridView3, 3);
            }
            Page.DataBind();
        }

        protected void BDelProcTask_Click(object sender, EventArgs e)
        {
            TaskStruct task = GetSelectedTask(4);
            if (DeleteTaskFromSession(task) > 0)
            {
                FillAllGridViews(); //FillGridView(ASPxGridView4, 4);
            }
            Page.DataBind();
        }

        protected void ASPxComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ASPxComboBox2.Items.Clear();
            //if (ASPxComboBox1.SelectedIndex != -1)
            //{
            //    ASPxComboBox2.Text = "";
            //    int selInd = ASPxComboBox1.SelectedIndex + 1;
            //    ASPxComboBox2.Items.AddRange(TaskManager.GetTasksDescrByClassification(selInd).ToList());
            //    if (ASPxComboBox2.Items.Count > 0)
            //    {
            //        ASPxComboBox2.Items[0].Selected = true;
            //    }
            //}
            ASPxComboBox4.DataSource = ReportManager.GetEventsDescrByModule(ASPxComboBox1.Text);
            ASPxComboBox4.DataBind();
            if (ASPxComboBox4.Items.Count != 0) ASPxComboBox4.SelectedIndex = 0;
        }

        protected void ASPxTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void CbStopEvent_CheckedChanged(object sender, EventArgs e)
        {
            if (CbStopEvent.Checked)
            {
                ASPxComboBox1.Enabled = true;
                ASPxComboBox4.Enabled = true;
                ASPxTextBox5.Enabled = true;
                ASPxTextBox4.Enabled = true;
            }
            else
            {
                ASPxComboBox1.Enabled = false;
                ASPxComboBox4.Enabled = false;
                ASPxTextBox5.Enabled = false;
                ASPxTextBox4.Enabled = false;
            }
        }




       

        

         //Session["mlwrID"] = this.gridViewMalware.GetRowValues(this.gridViewMalware.FocusedRowIndex, "Id");
         //   Response.Redirect("~/Pages/Malware/MalwareCard.aspx");


    }//end class
}//end namespace