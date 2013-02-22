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

namespace SandBox.WebUi.Pages.Research
{
    public partial class RegsTree : BaseMainPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Дерево реестра";
            PageMenu = "~/App_Data/SideMenu/Research/ResearchMenu.xml";
            var rsch = ResearchManager.GetResearch((int)Session["rsch"]);
            ASPxLabel1.Text = "Исследование: " + rsch.ResearchName;
            //GVProcesses.DataSource = ReportManager.GetProcesses2((int)Session["rsch"]);
            //GVProcesses.DataBind();
            if (!IsPostBack)
            {
                TreeViewBuilder tvb = new TreeViewBuilder();
                var commonTreeItems = tvb.GetCommonTreeItemsFromRegs((int)Session["rsch"]);
                var rootElements = tvb.GetRootElements(commonTreeItems);
                tvb.TreeListViewGenerator(TreeView1, commonTreeItems, rootElements, "Дерево реестра");

                //Debug.Print("Get research id = " + Request.QueryString["research"]);


            }

        }
    }
}