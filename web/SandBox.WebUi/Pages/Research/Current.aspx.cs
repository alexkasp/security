using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.Web.ASPxEditors;
using SandBox.Connection;
using SandBox.Db;
using SandBox.Log;
using SandBox.WebUi.Base;
using System.Web.UI;
using DevExpress.Web.ASPxGridView;
using System.Collections.Generic;

namespace SandBox.WebUi.Pages.Research
{
    public partial class Current : BaseMainPage
    {
        int test;
        private static Int32 _userId;
        public Db.Research Rs;

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Текущие исследования";
            PageMenu = "~/App_Data/SideMenu/Research/ResearchMenu.xml";
            
            if (!IsPostBack)
            {
                gridViewResearches.KeyFieldName = "Id";
                gridViewResearches.Visible = false;
                labelNoItems.Text = "";
                _userId = UserId;
                
                // UpdateTableView();
                gridViewResearches.DataBind();
            }
        }

        private void UpdateTableView()
        {
            Int32 count = IsUserInRole("Administrator") ? ResearchManager.GetResearches().Count() : ResearchManager.GetResearches(UserId).Count();
            IQueryable rs = IsUserInRole("Administrator") ? ResearchManager.GetResearchesTableView() : ResearchManager.GetResearchesTableView(UserId);

            if (count > 0)
            {
                gridViewResearches.Visible = true;
                labelNoItems.Text = "";

                gridViewResearches.DataSource = rs;
                gridViewResearches.DataBind();
            }

            if (count == 0)
            {
                gridViewResearches.Visible = false;
                labelNoItems.Text = "У вас нет доступных исследований";
            }
        }

        protected void UpdateTimerTick(object sender, EventArgs e)
        {
             UpdateTableView();
        }

        protected void CallbackPanelDeleteCallback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            Int32 researchId = Convert.ToInt32(e.Parameter);
            Session["researchId"] = researchId;
            if (researchId == 0) return;
            deleteText.Text = "Вы точно хотите удалить исследование " + ResearchManager.GetResearch(researchId).ResearchName + "?";
        }


        [System.Web.Services.WebMethod]
        public static void WebDelResearch(string id)
        {
            Int32 researchId = Convert.ToInt32(id);
            MLogger.LogTo(Level.TRACE, false, "Delete research '" + ResearchManager.GetResearch(researchId).ResearchName + "' by user '" + UserManager.GetUser(_userId).UserName + "'");
            ResearchManager.DeleteResearch(researchId);
        }

        protected void BtnDeleteClick(Object sender, EventArgs e)
        {
            Int32 researchId = Convert.ToInt32(Session["researchId"]);
            //Int32 researchId = Convert.ToInt32(id);  
            if (researchId == 0) return;
            MLogger.LogTo(Level.TRACE, false, "Delete research '" + ResearchManager.GetResearch(researchId).ResearchName + "' by user '" + UserManager.GetUser(_userId).UserName + "'");
            ResearchManager.DeleteResearch(researchId);
        }

        [System.Web.Services.WebMethod]
        public static void StartResearch(string id)
        {
            Int32 researchId = Convert.ToInt32(id);         
            
            if (ResearchManager.GetResearch(researchId).State == (Int32)ResearchState.READY)
            {
                MLogger.LogTo(Level.TRACE, false, "Start research '" + ResearchManager.GetResearch(researchId).ResearchName + "' by user '" + UserManager.GetUser(_userId).UserName + "'");
                ResearchManager.UpdateResearchState(researchId, ResearchState.STARTING);

                //**---------------------------------------
                Vm vm = VmManager.GetVm(ResearchManager.GetResearch(researchId).VmId);
                byte[] envIdBytes = BitConverter.GetBytes(vm.EnvId);
                Mlwr mlwr = MlwrManager.GetMlwr(ResearchManager.GetResearch(researchId).MlwrId);

                Packet packet1 = new Packet { Type = PacketType.CMD_SET_TARGET, Direction = PacketDirection.REQUEST };
                packet1.AddParameter(new[] { envIdBytes[0] });
                packet1.AddParameter(Encoding.UTF8.GetBytes(mlwr.Path));
                SendPacket(packet1.ToByteArray());

                Packet packet2 = new Packet { Type = PacketType.CMD_SET_OBJECT, Direction = PacketDirection.REQUEST };
                packet2.AddParameter(new[] { envIdBytes[0] });
                packet2.AddParameter(Encoding.UTF8.GetBytes(mlwr.Path));
                SendPacket(packet2.ToByteArray());
                //***********************Посылаем пакет с иформацей о соответствии идентификатора среды и исследования
                //SendInfoRschIdEnvId(researchId, vm.EnvId);
                //****Установка дополнительных параметров

                IQueryable<Task> tasks = TaskManager.GetTasks(researchId);
                foreach (var task in tasks)
                {
                    Packet packet = new Packet {Direction = PacketDirection.REQUEST};

                    switch (task.Type)
                    {
                        case (Int32)TaskState.HIDE_FILE:
                            packet.Type = PacketType.CMD_HIDE_AND_LOCK;
                            packet.AddParameter(new[] { envIdBytes[0] });
                            packet.AddParameter(Encoding.UTF8.GetBytes(task.Value));
                            SendPacket(packet.ToByteArray());
                            break;
                        case (Int32)TaskState.LOCK_FILE:
                            packet.Type = PacketType.CMD_LOCK_DELETE;
                            packet.AddParameter(new[] { envIdBytes[0] });
                            packet.AddParameter(Encoding.UTF8.GetBytes(task.Value));
                            SendPacket(packet.ToByteArray());
                            break;
                        case (Int32)TaskState.HIDE_REGISTRY:
                            packet.Type = PacketType.CMD_HIDE_REGISTRY;
                            packet.AddParameter(new[] { envIdBytes[0] });
                            packet.AddParameter(Encoding.UTF8.GetBytes(task.Value));
                            SendPacket(packet.ToByteArray());
                            break;
                        case (Int32)TaskState.HIDE_PROCESS:
                            packet.Type = PacketType.CMD_HIDE_PROCESS;
                            packet.AddParameter(new[] { envIdBytes[0] });
                            packet.AddParameter(Encoding.UTF8.GetBytes(task.Value));
                            SendPacket(packet.ToByteArray());
                            break;
                        case (Int32)TaskState.SET_SIGNATURE:
                            packet.Type = PacketType.CMD_SET_SIGNATURE;
                            packet.AddParameter(new[] { envIdBytes[0] });
                            packet.AddParameter(Encoding.UTF8.GetBytes(task.Value));
                            SendPacket(packet.ToByteArray());
                            break;
                        case (Int32)TaskState.SET_EXTENSION:
                            packet.Type = PacketType.CMD_SET_EXTENSION;
                            packet.AddParameter(new[] { envIdBytes[0] });
                            packet.AddParameter(Encoding.UTF8.GetBytes(task.Value));
                            SendPacket(packet.ToByteArray());
                            break;
                        case (Int32)TaskState.SET_BANDWIDTH:
                            String ip = vm.EnvIp;
                            Int32 bandwidth = Convert.ToInt32(task.Value);
                            packet.Type = PacketType.CMD_SET_BANDWIDTH;
                            packet.AddParameter(Encoding.UTF8.GetBytes(ip));
                            packet.AddParameter(BitConverter.GetBytes(bandwidth));
                            SendPacket(packet.ToByteArray());
                            break;
                        case (Int32)TaskState.GET_PROCESS:
                            {
                                SendGetProcess(researchId);
                                break;
                            }
                        case (Int32)TaskState.GET_FILES:
                            {
                                SendGetFiles(researchId, task.Value);
                                break;
                            }
                        case (Int32)TaskState.GET_REGS:
                            {
                                Int32 key = Convert.ToInt32(task.Value.Substring(0,1));
                                string subKey = "";
                                if(task.Value.Length>1)
                                {
                                    subKey = task.Value.Substring(1, task.Value.Length-1);
                                }
                                SendGetRegs(researchId, subKey, key);
                                break;
                            }
                    }
                    //SendPacket(packet.ToByteArray());
                }

                //****
                Packet packet3 = new Packet { Type = PacketType.CMD_LOAD_MALWARE, Direction = PacketDirection.REQUEST };
                packet3.AddParameter(new[] { envIdBytes[0] });
                packet3.AddParameter(Encoding.UTF8.GetBytes(mlwr.Path));
                SendPacket(packet3.ToByteArray());
                //**---------------------------------------

                ResearchManager.UpdateResearchState(researchId, ResearchState.EXECUTING);
                ResearchManager.UpdateResearchStartTime(researchId); //?? Должно быть выше
                VmManager.UpdateVmState(vm.Id, VmManager.State.RESEARCHING);
                ResearchManager.UpdateResearchVmData(ResearchManager.GetResearch(researchId).ResearchVmData, vm.EnvMac, vm.EnvIp);
            }
            else
            {
                MLogger.LogTo(Level.TRACE, false, "Unsuccessful attempt to start research '" + ResearchManager.GetResearch(researchId).ResearchName + "' by user '" + UserManager.GetUser(_userId).UserName + "' , research not ready");
            } 
        }

        [System.Web.Services.WebMethod]
        public static void StopResearch(string id)
        {
            Int32 researchId = Convert.ToInt32(id);
            //Приведение таблтцы [dbo].[events] в актуальное состояние
            //int res1 = ResearchManager.UpdateEnents(researchId);
            if (ResearchManager.GetResearch(researchId).State == (Int32)ResearchState.EXECUTING)
            {
                Db.Research research = ResearchManager.GetResearch(researchId);
                MLogger.LogTo(Level.TRACE, false, "Stop research '" + ResearchManager.GetResearch(researchId).ResearchName + "' by user '" + UserManager.GetUser(_userId).UserName + "'");
                ResearchManager.UpdateResearchState(researchId, ResearchState.COMPLETING);

                //Останаливаем виртуалку
                String machineName = VmManager.GetVmName(research.VmId);
                Packet packet = new Packet { Type = PacketType.CMD_VM_STOP, Direction = PacketDirection.REQUEST };
                packet.AddParameter(Encoding.UTF8.GetBytes(machineName));
                SendPacket(packet.ToByteArray());

                //Добавил---
                ResearchManager.UpdateResearchStopTime(research.Id);
                ResearchManager.UpdateResearchState(research.Id, ResearchState.COMPLETED);
                //----------
                int res1 = ResearchManager.UpdateEnents(researchId);
            }
            else
            {
                MLogger.LogTo(Level.TRACE, false, "Unsuccessful attempt to stop research '" + ResearchManager.GetResearch(researchId).ResearchName + "' by user '" + UserManager.GetUser(_userId).UserName + "' , research already stopped");
            }
            //Приведение таблтцы [dbo].[events] в актуальное состояние
            int res = ResearchManager.UpdateEnents(researchId);

        }

        [System.Web.Services.WebMethod]
        public static void GetReport(string id)
        {
            Int32 researchId = Convert.ToInt32(id);

            Debug.Print("Get report: " + researchId);
        }

        protected void GridViewResearchesHtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            /*if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;

            //e.Row.BackColor = 
            //e.Row.ForeColor = Color.Red;
            e.Row.BackColor = Color.DeepPink;
            
            ASPxHyperLink link = gridViewResearches.FindRowCellTemplateControl(e.VisibleIndex, null, "linkA") as ASPxHyperLink;
            if (link == null) return;
            
            Int32 researchId = (Int32)e.KeyValue;
            Db.Research research = ResearchManager.GetResearch(researchId);

            if (research.State == (Int32)ResearchState.COMPLETED)
            {
                link.NavigateUrl = RootPath + "/ReportList.aspx?research=" + e.KeyValue;
                
            }
            else
            {
                link.Visible = false;
            }*/
        }

        protected void GridViewResearchesHtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;

            ASPxHyperLink link = gridViewResearches.FindRowCellTemplateControl(e.VisibleIndex, null, "linkA") as ASPxHyperLink;
            if (link == null) return;
            Int32 researchId = (Int32)e.KeyValue;
            Db.Research research = ResearchManager.GetResearch(researchId);
            ASPxHyperLink currentReportLink = gridViewResearches.FindRowCellTemplateControl(e.VisibleIndex, null, "HLCurrentReport") as ASPxHyperLink;
            if (currentReportLink != null)
            {
                switch (research.State)
                {
                    case (Int32)ResearchState.COMPLETED:
                        {
                            currentReportLink.Visible = false;
                            break;
                        }
                    case (Int32)ResearchState.EXECUTING:
                        {
                            currentReportLink.Visible = true;
                            currentReportLink.NavigateUrl = String.Format("{0}/CurrentReportList.aspx?research={1}", RootPath, e.KeyValue);
                            break;
                        }
                    default:
                        {
                            currentReportLink.Visible = false;
                            break;
                        }
                }
            }
            
            
            #region StartAndStopButtonsPreparing
            ASPxButton bStart = gridViewResearches.FindRowCellTemplateControl(e.VisibleIndex, null, "BStart") as ASPxButton;
            ASPxButton bStop = gridViewResearches.FindRowCellTemplateControl(e.VisibleIndex, null, "BStop") as ASPxButton;
            if ((bStart != null) && (bStop != null))
            {
                switch (research.State)
                {
                    case (int)ResearchState.COMPLETED:
                        {
                            bStart.Visible = false;
                            bStop.Visible = false;
                            break;
                        }
                    case (int)ResearchState.COMPLETING:
                        {
                            bStart.Visible = false;
                            bStop.Visible = true;
                            bStop.Enabled = false;
                            bStop.Text = "завершение";
                            break;
                        }
                    case (int)ResearchState.EXECUTING:
                        {
                            bStart.Visible = false;
                            bStop.Visible = true;
                            bStop.Text = "остановить";
                            break;
                        }
                    case (int)ResearchState.READY:
                        {
                            bStart.Visible = true;
                            bStart.Text = "запустить";
                            bStop.Visible = false;
                            break;
                        }
                    case (int)ResearchState.STARTING:
                        {
                            bStart.Visible = true;
                            bStart.Enabled = false;
                            bStart.Text = "запуск";
                            bStop.Visible = false;
                            break;
                        }
                }
            }

            #endregion

            if (research.State == (Int32)ResearchState.COMPLETED)
            {
                var valid = ResearchManager.GetResearch((int)e.KeyValue);
                if (valid == null && ResearchManager.GetResearch((int)e.KeyValue-1)!=null);           
                //link.NavigateUrl = RootPath + "/ReportList.aspx?research=" + ((int)e.KeyValue-1);
                var test = e.Row.Cells.Count;
                foreach(System.Web.UI.WebControls.TableCell cell in e.Row.Cells)
                {
                    var test1 = cell.Text;
                }
                link.NavigateUrl = RootPath + "/ReportList.aspx?research=" + e.KeyValue;
//                e.Row.BackColor = Color.FromArgb(0xDB, 0xFA, 0xA5);
            }
            else
            {
                link.Visible = false;
            }

            if (research.State == (Int32)ResearchState.EXECUTING)
            {
                e.Row.BackColor = Color.FromArgb(0xDB, 0xFA, 0xA5);
//                e.Row.BackColor = Color.Teal;
            }
        }

        /// <summary>
        /// Отправка пакета с информацией о паре id исследования и id среды
        /// </summary>
        /// <param name="rschId">id исследования</param>
        /// <param name="envId">id среды</param>
        protected static void SendInfoRschIdEnvId(int rschId, int envId)
        {
            Packet packet = new Packet { Type = PacketType.INF_RSCHID_ENVID, Direction = PacketDirection.REQUEST };
            byte[] rschIdBytes = BitConverter.GetBytes(rschId);
            byte[] envIdBytes = BitConverter.GetBytes(envId);
            packet.AddParameter(new[] { rschIdBytes[0] });
            packet.AddParameter(new[] { envIdBytes[0] });
            SendPacket(packet.ToByteArray());
        }

        /// <summary>
        /// Отправка пакета с запросом для исследования на начало записи списка процессов в базу
        /// </summary>
        /// <param name="id">идентификатор исследования</param>
        protected static void SendGetProcess(int id)
        {
            Vm vm = VmManager.GetVm(ResearchManager.GetResearch(id).VmId);
            byte[] envIdBytes = BitConverter.GetBytes(vm.EnvId);
            Packet packet1 = new Packet { Type = PacketType.CDM_LOAD_PROCESSES, Direction = PacketDirection.REQUEST };
            packet1.AddParameter(new[] { envIdBytes[0] });
            SendPacket(packet1.ToByteArray());
        }

        /// <summary>
        /// Отправка пакета с запросом для исследования на начало списка файлов в базу
        /// </summary>
        /// <param name="id">идентификатор исследования</param>
        /// <param name="root">начальный каталог для получения списка файлов</param>
        protected static void SendGetFiles(int id, String root = "c:\\windows\\*")
        {
            Vm vm = VmManager.GetVm(ResearchManager.GetResearch(id).VmId);
            byte[] envIdBytes = BitConverter.GetBytes(vm.EnvId);
            Packet packet1 = new Packet { Type = PacketType.CDM_LOAD_FILES, Direction = PacketDirection.REQUEST };
            packet1.AddParameter(new[] { envIdBytes[0] });
            packet1.AddParameter(Encoding.UTF8.GetBytes(root));
            SendPacket(packet1.ToByteArray());
        }

        /// <summary>
        /// Отправка пакета с запросом для исследования на начало списка записей реестра в базу
        /// </summary>
        /// <param name="id">идентификатор исследования</param>
        /// <param name="subkey">подветка реестра (здесь уже если надо указать нужно писать строкой к примеру "Software")</param>
        /// <param name="rowStartKeyBytes">ветка реестра (0 - HKEY_CLASSES_ROOT, 1- HKEY_CURRENT_USER, 2 - HKEY_LOCAL_MACHINE, 3 - HKEY_USERS, 4 -HKEY_CURRENT_CONFIG;)</param>
        protected static void SendGetRegs(int id, String subkey = "",int rowStartKeyBytes = 2)
        {
            Vm vm = VmManager.GetVm(ResearchManager.GetResearch(id).VmId);
            byte[] envIdBytes = BitConverter.GetBytes(vm.EnvId);
            byte[] startKeyBytes = BitConverter.GetBytes(rowStartKeyBytes);
            Packet packet1 = new Packet { Type = PacketType.CDM_LOAD_REGS, Direction = PacketDirection.REQUEST };
            packet1.AddParameter(new[] { envIdBytes[0] });
            packet1.AddParameter(new[] { startKeyBytes[0] });
            packet1.AddParameter(Encoding.UTF8.GetBytes(subkey));
            SendPacket(packet1.ToByteArray());
        }

        protected void BStop_Click(object sender, EventArgs e)
        {

        }

        //protected void gridViewResearches_BeforePerformDataSelect(object sender, EventArgs e)
        //{

        //    Session["TestID"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        //}

        //protected void gridViewResearches_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        //{
        //    if (e.Column.FieldName == "Id")
        //    {

 
        //    }

        //}

        // protected void detail_DataBinding(object sender, EventArgs e)
        protected void gridViewResearches_DataSelect(object sender, EventArgs e)
        {
            Int32 count = IsUserInRole("Administrator") ? ResearchManager.GetResearches().Count() : ResearchManager.GetResearches(UserId).Count();
            IQueryable rs = IsUserInRole("Administrator") ? ResearchManager.GetResearchesTableView() : ResearchManager.GetResearchesTableView(UserId);

            if (count > 0)
            {
                gridViewResearches.Visible = true;
                labelNoItems.Text = "";

                gridViewResearches.DataSource = rs;               
            }

            if (count == 0)
            {
                gridViewResearches.Visible = false;
                labelNoItems.Text = "У вас нет доступных исследований";
            }
        }
        

        protected void Detail_DataSelect(object sender, EventArgs e)            
        {
          try
            {
                int rschIdx = 0;
                int[] FileEvs = new int[2];
                int[] RegistryEvs = new int[2];
                int[] NetEvs = new int[2];
                int[] ProcessEvs = new int[2];
                ASPxGridView grid = (ASPxGridView)sender;
                rschIdx = (int)grid.GetMasterRowKeyValue();
                grid.DataSource = TaskManager.GetTasksViewForRsch(rschIdx);//dataX;

                var db = new SandBoxDataContext();
                var rdof = from d in db.EventsChartCounts
                           where d.Id == rschIdx
                           select d;
                foreach (var rr in rdof)
                {
                    switch (rr.Module)
                    {
                        case "Файловая система":
                            switch (rr.Sign)
                            {
                                case 1: FileEvs[0] += (int)rr.Count; break;
                                default: FileEvs[1] += (int)rr.Count; break;
                            } break;
                        case "Реестр":
                            switch (rr.Sign)
                            {
                                case 1: RegistryEvs[0] += (int)rr.Count; break;
                                default: RegistryEvs[1] += (int)rr.Count; break;
                            } break;
                        case "Процессы":
                            switch (rr.Sign)
                            {
                                case 1: ProcessEvs[0] += (int)rr.Count; break;
                                default: ProcessEvs[1] += (int)rr.Count; break;
                            } break;
                        case "TDIMON":
                        case "NDISMON":
                            switch (rr.Sign)
                            {
                                case 1: NetEvs[0] += (int)rr.Count; break;
                                default: NetEvs[1] += (int)rr.Count; break;
                            } break;
                    }
                }

                grid.ClientSideEvents.Init = "function(s, e) { drawHalfPie('chartHolder1',Array(" + FileEvs[1].ToString() + ","
                     + FileEvs[0].ToString() + ")); drawHalfPie('chartHolder2',Array(" + RegistryEvs[1].ToString() + ","
                     + RegistryEvs[0].ToString() + ")); drawHalfPie('chartHolder3',Array(" + ProcessEvs[1].ToString() + ","
                     + ProcessEvs[0].ToString() + ")); drawHalfPie('chartHolder4',Array(" + NetEvs[1].ToString() + ","
                     + NetEvs[0].ToString() + "));"
                     + "$('.detailrowreport > a').attr('href', 'ReportList.aspx?researchId=" + rschIdx + "')}";
            }
            catch (Exception)
            {
                
                throw;
            }

            int t = 0999;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Pages/Research/ReportList.aspx");
        }

        protected void gridViewResearches_FocusedRowChanged(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            int s = (int)grid.GetRowValues(grid.FocusedRowIndex, "Id");
            Session["rsId"] = s;
            
        }

        protected void gridViewResearches_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "DeleteSelected")
            {
                List<object> keyValues = gridViewResearches.GetSelectedFieldValues("Id");
                foreach (object key in keyValues)
                {
                    int testkey = (int)key;
                    ResearchManager.DeleteResearch(testkey);
                }
                UpdateTableView();
                gridViewResearches.Selection.UnselectAll();
            }
        }

        protected void gridViewResearches_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Research/NewResearch.aspx");
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {

        }
        

     
    }//end class
}//end namespace