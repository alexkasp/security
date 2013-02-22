using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SandBox.Db;
using SandBox.WebUi.Base;
using System.Drawing;

namespace SandBox.WebUi.Pages.Research
{
    public partial class CurrentReportList : BaseMainPage
    {
        public Db.Research Rs;
        protected void Page_Load(object sender, EventArgs e)
        {
            PageTitle = "Отчет";
            PageMenu = "~/App_Data/SideMenu/Research/ResearchMenu.xml";
            Int32 researchId = Convert.ToInt32(Request.QueryString["research"]);
            Rs = ResearchManager.GetResearch(researchId);
            if (Rs == null)
            {
                Response.Redirect("~/Error");
            }
            ASPxLabel1.Text =String.Format("Исследование: (№{0}) {1}", Rs.Id, Rs.ResearchName);
        }

        protected void Unnamed1_Tick(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void UpdateTable()
        {
            EventsView.DataSource = ResearchManager.GetEventsTableViewByRschId(Rs.Id, true);
                                        
            EventsView.DataBind();
        }

        protected void EventsView_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;
            if (e.KeyValue != null)
            {
                long key = (long)e.KeyValue;
                var evt = ResearchManager.GetEventById(key);
                if (evt != null)
                {
                    int evtSignif = ReportManager.GetEvtSignif(evt);
                    switch (evtSignif)
                    {
                        case 0:
                            {
                                e.Row.BackColor = Color.Salmon;
                                break;
                            }
                        case 1:
                            {
                                e.Row.BackColor = Color.SandyBrown;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    //e.Row.BackColor = Color.Yellow;
                }
            }
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
        }
    }
}