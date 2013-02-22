using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using SandBox.Connection;
using SandBox.Db;
using SandBox.Log;
using SandBox.WebUi.Base;
using DevExpress.XtraCharts;
using System.Collections.Generic;
using System.Collections;

namespace SandBox.WebUi.Pages.Information
{
    public partial class DirectoryOfEvents : BaseMainPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Справочник событий";
            PageMenu = "~/App_Data/SideMenu/Information/InformationMenu.xml";
            ASPxGridView1.DataSource = ReportManager.GetRowDirectoriesOfEvents();
            ASPxGridView1.DataBind();
            if (!IsPostBack)
            {
                
                ASPxComboBox2.DataSource = ReportManager.GetModules();
                ASPxComboBox2.DataBind();
                //if (ASPxComboBox2.Items.Count != 0) ASPxComboBox2.SelectedIndex = 0;
            }

        }

        protected void ASPxComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASPxComboBox3.DataSource = ReportManager.GetEventsDescrByModule(ASPxComboBox2.Text);
            ASPxComboBox3.DataBind();
            if (ASPxComboBox3.Items.Count != 0) ASPxComboBox3.SelectedIndex = 0;
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            int sign =  ASPxComboBox1.Text == "Критически важное"? 0: 1;
            int module = ResearchManager.GetModuleIdByDescr(ASPxComboBox2.Text);
            int evt = ResearchManager.GetEventIdByDescr(ASPxComboBox3.Text);
            string dest = ASPxTextBox1.Text;
            string who = ASPxTextBox2.Text;
            if (module != -1 && evt != -1 && dest != String.Empty && who != String.Empty)
            {
                ReportManager.InsertRowDirectoriesOfEvents(sign, module, evt, dest, who);
                
            }
            ASPxGridView1.DataSource = ReportManager.GetRowDirectoriesOfEvents();
            ASPxGridView1.DataBind();

        }

        protected void ASPxButton2_Click(object sender, EventArgs e)
        {
            long id = (long)this.ASPxGridView1.GetRowValues(this.ASPxGridView1.FocusedRowIndex, "Id");
            ReportManager.DeleteDirectorysOfEvent(id);
            ASPxGridView1.DataSource = ReportManager.GetRowDirectoriesOfEvents();
            ASPxGridView1.DataBind();
        }
    }
}