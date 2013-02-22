using System;
using System.Diagnostics;
using System.Text;
using DevExpress.Web.ASPxGridView;
using SandBox.Connection;
using SandBox.Db;

namespace SandBox.WebUi.Pages.Research
{
    public partial class History : System.Web.UI.Page
    {
        private static ConnectionClientEx _client;

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "*** | История исследований";
            _client = ConnectionClientEx.Instance;

            if (Master != null) ((MainMaster)Master).SetMenuFile("~/App_Data/SideMenu/Research/ResearchMenu.xml");

            gridViewMalware.Settings.ShowHeaderFilterButton = true;
            gridViewMalware.KeyFieldName = "Id";

            foreach (var column in gridViewMalware.Columns)
            {
                if (column.GetType() == typeof(GridViewDataColumn))
                {
                    ((GridViewDataColumn)column).Settings.HeaderFilterMode = HeaderFilterMode.List;
                }
            }

            if (!IsPostBack)
            {
                ReportManager.ClearAllReports(); // Убрать потом!!!
                UpdateTableView();
            }
        }


        private void UpdateTableView()
        {
            gridViewMalware.DataSource = MlwrManager.GetMlwrsTableView();
            gridViewMalware.DataBind();
        }

        protected void UpdateTimerTick(object sender, EventArgs e)
        {
           // UpdateTableView();
        }

        protected void GridViewMalwareHeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();

            if (e.Column.FieldName == "Class")
            {
                foreach (var cls in MlwrManager.GetMlwrClassList())
                {
                    e.AddValue(cls, cls);
                }
            }

            if (e.Column.FieldName == "Name")
            {
                foreach (var cls in MlwrManager.GetMlwrNameList())
                {
                    e.AddValue(cls, cls);
                }
            }

            if (e.Column.FieldName == "Path")
            {
                foreach (var cls in MlwrManager.GetMlwrPathList())
                {
                    e.AddValue(cls, cls);
                }
            }

            if (e.Column.FieldName == "Loaded")
            {
                foreach (var cls in MlwrManager.GetMlwrLoadedList())
                {
                    e.AddValue(cls, cls);
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static void FolowMalware(string id)
        {
            Int32 malwareId = Convert.ToInt32(id);
            String malwarePath = MlwrManager.GetPath(malwareId);
            Packet packet = new Packet { Type = PacketType.OBJECT_FOLLOW, Direction = PacketDirection.REQUEST };
            packet.AddParameter(new byte[] { 0x02 });
            packet.AddParameter(Encoding.UTF8.GetBytes(malwarePath));
            _client.Send(packet.ToByteArray());
        }

        protected void CallbackPanelLoadCallback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            Int32 malwareId = Convert.ToInt32(e.Parameter);
            Session["malwareId"] = malwareId;
            if (malwareId == 0) return;

            litText.Text = "Выберите влир для загрузки: ";
            ddMachines.DataSource = MachineManager.GetMachinesName();
            ddMachines.DataBind();
        }

        protected void BtnLoadClick(object sender, EventArgs e)
        {
            Int32 malwareId = Convert.ToInt32(Session["malwareId"]);
            if (malwareId == 0) return;
            String malwarePath = MlwrManager.GetPath(malwareId);
            Packet packet = new Packet { Type = PacketType.MALWARE_LOAD, Direction = PacketDirection.REQUEST };
            packet.AddParameter(new byte[] { 0x02 });
            packet.AddParameter(Encoding.UTF8.GetBytes(malwarePath));
            _client.Send(packet.ToByteArray());
        }

        protected void ddMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] machines = MachineManager.GetMachinesName();
            Debug.Print("machine:" + machines[ddMachines.SelectedIndex]);
        }
    }//end class
}//end namespace