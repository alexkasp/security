using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SandBox.Db;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages.Research
{
    public partial class SetMonitor : BaseMainPage
    {
        bool h = false;
        private string _fclass = "Реестр";
        private string _sclass = "Создание ветки реестра";

        private string SessionFC
        {
            set 
            {
                if (Session["MonFC"] == null)
                    Session.Add("MonFC", _fclass);
                else Session["MonFC"] = value;
            }
            get 
            {
                return (string)Session["MonFC"];
            }
        }

        private string SessionSC
        {
            set
            {
                if (Session["MonSC"] == null)
                    Session.Add("MonSC", _sclass);
                else Session["MonSC"] = value;
            }
            get
            {
                return (string)Session["MonSC"];
            }
        }

        private void ApdateDDLSClass(string fc)
        {
            DDLSClass.Items.Clear();
            DDLSClass.ClearSelection();
            if (fc == "") fc = _fclass;
            string[] fortest = MlwrManager.GetSCByFCName(DropDownList3.SelectedValue).ToArray();
            foreach (var sc in fortest)
            {
                DDLSClass.Items.Add(new ListItem(sc));
            }
            //Label1.Text = (string)Session["MonFC"];
            //Label2.Text = DDLSClass.Text;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Таблица критериев для исследования";
            PageMenu = "~/App_Data/SideMenu/Research/ResearchMenu.xml";
            if (!Page.IsPostBack)
            {
                if (Session["MonFC"] == null)
                    Session.Add("MonFC", _fclass);
                //Label2.Text = (string)Session["MonFC"];
                ApdateDDLSClass((string)Session["MonFC"]);
            }
            this.ASPxGridView1.DataSource = ResearchManager.GetMonitoringEvents((int)Session["rsch"]);
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            h = true;
            Session["MonSC"] = _sclass;
            int sessionId = (int)Session["rsch"];
            string MFC = (string)Session["MonFC"];
            string MSC = DDLSClass.Text;//(string)Session["MonSC"];
            ResearchManager.InsertMonEvent(sessionId, MFC, MSC, ASPxTextBox1.Text);
            //Page.DataBind();

            string[] fortest = MlwrManager.GetSCByFCName(DropDownList3.SelectedValue).ToArray();
            this.ASPxGridView1.DataSource = ResearchManager.GetMonitoringEvents((int)Session["rsch"]);
        }

        protected void ASPxButton2_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = (int)this.ASPxGridView1.GetRowValues(this.ASPxGridView1.FocusedRowIndex, "Id");
                ResearchManager.DleteMonEvent(Id);
            }
            catch (Exception)
            {
            }
            finally
            {
                ASPxGridView1.DataBind();
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            _fclass = DropDownList3.SelectedValue;
            if (Session["MonFC"] == null)
                Session.Add("MonFC", _fclass);
            else Session["MonFC"] = DropDownList3.SelectedValue;
            DDLSClass.Items.Clear();
            string sc;
            if (Session["MonFC"] == null)
            {
                sc = _fclass;
            }
            else
            {
                sc = (string)Session["MonFC"];
            }
            ApdateDDLSClass((string)Session["MonFC"]);
            SessionSC = DDLSClass.Text;
           
        }

        protected void ASPxGridView1_PreRender(object sender, EventArgs e)
        {
            this.ASPxGridView1.DataSource = ResearchManager.GetMonitoringEvents((int)Session["rsch"]);
            this.ASPxGridView1.DataBind();
        }

        protected void DDLSClass_Load(object sender, EventArgs e)
        {
            //bool f = false;
            //if (DDLSClass.Text != "")
            //{
            //    SessionSC = DDLSClass.Text;
            //    f = true;
            //}
            //string sc;
            //if (Session["MonFC"] == null)
            //{
            //    sc = _fclass;
            //}
            //else
            //{
            //    sc = (string)Session["MonFC"];
            //}
            //string[] fortest = MlwrManager.GetSCByFCName(sc).ToArray();
            //DDLSClass.Items.Clear();
            //foreach (var s in fortest)
            //{
            //    DDLSClass.Items.Add(new ListItem(s));
            //}
            //if (!f)
            //{
            //    SessionSC = DDLSClass.SelectedValue;
            //}
            

        }

        protected void DDLSClass_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void DDLSClass_TextChanged(object sender, EventArgs e)
        {
           // ApdateDDLSClass((string)Session["MonFC"]);
            
        }

        protected void DDLSClass_PreRender(object sender, EventArgs e)
        {
            if (DDLSClass.Items.Count == 0)
            {
                ApdateDDLSClass((string)Session["MonFC"]);
            }
        }

        protected void DropDownList3_PreRender(object sender, EventArgs e)
        {
            SessionFC = DropDownList3.Text;
            //if (h)
            //{
            //    Session["MonSC"] = _sclass;
            //    int sessionId = (int)Session["rsch"];
            //    string MFC = (string)Session["MonFC"];
            //    string MSC = DDLSClass.Text;//(string)Session["MonSC"];
            //    ResearchManager.InsertMonEvent(sessionId, MFC, MSC, ASPxTextBox1.Text);
            //    //Page.DataBind();

            //    string[] fortest = MlwrManager.GetSCByFCName(DropDownList3.SelectedValue).ToArray();
            //    this.ASPxGridView1.DataSource = ResearchManager.GetMonitoringEvents((int)Session["rsch"]);
            //}
        }

    }
}