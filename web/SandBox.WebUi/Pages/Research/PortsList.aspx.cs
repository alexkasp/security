using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SandBox.Db;
using SandBox.WebUi.Base;
using SandBox.Connection;
using System.Text;
using System.Drawing;

namespace SandBox.WebUi.Pages.Research
{
    public partial class PortsList : BaseMainPage
    {
        public Db.Research Rs;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Список портов";
            PageMenu = "~/App_Data/SideMenu/Research/ResearchMenu.xml";
            Int32 researchId = Convert.ToInt32(Request.QueryString["research"]);
            Rs = ResearchManager.GetResearch(researchId);
            if (Rs == null)
            {
                Response.Redirect("~/Error");
            }
            ASPxLabel1.Text =String.Format("Исследование: {0}",Rs.ResearchName);
            UpdateGridView();

        }

        protected void BUpdateView_Click(object sender, EventArgs e)
        {
            UpdateGridView();
        }

        private void UpdateGridView()
        {
            ASPxGridView1.DataSource =  ResearchManager.GetPortsViewForRsch(Rs.Id);
            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;
            PortList pl = ResearchManager.GetPortList((long)e.KeyValue);
            if (pl != null)
            {
                switch (pl.status)
                {
                    case "LISTENING":
                        {
                            e.Row.BackColor = Color.Salmon;
                            break;
                        }
                    case "ESTABLISHED":
                        {
                            e.Row.BackColor = Color.SandyBrown;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
    }
}