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
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTreeList;

namespace SandBox.WebUi.Pages.Research
{
    public partial class Comparer : BaseMainPage
    {
        int i = 0;
        public Db.Research Rs;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Сравнение веток реестра";
            PageMenu = "~/App_Data/SideMenu/Research/ResearchMenu.xml";
            Int32 researchId = Convert.ToInt32(Request.QueryString["research"]);
            Rs = ResearchManager.GetResearch(researchId);
            if (TreeView1.Nodes.Count > 0)
            {
                ASPxTreeList1.Columns.Clear();
                ASPxTreeList1.ClearNodes();
                ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn("KeyName", "Имя ключя"));
                ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn("IsNodeInRsch", Rs.ResearchName));
                ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn("IsMoneInCompared", ASPxComboBox1.SelectedItem.Text));
                ConvertTreeViewToTreeList(TreeView1.Nodes[0], null);
            }
            if (Rs == null)
            {
                Response.Redirect("~/Error");
            }
             if (!IsPostBack)
             {
                 
                 var rschs = ResearchManager.GetReadyResearches();
                 foreach (var r in rschs)
                 {
                     string text  = String.Format("{0}: id = {1}",r.ResearchName, r.Id);
                     ASPxComboBox1.Items.Add(new ListEditItem(text, r.Id));
                 }
             }
             if (ASPxTreeList1.Summary.Count == 0)
             {
                 TreeListSummaryItem siPrice = new TreeListSummaryItem();
                 siPrice.FieldName = "KeyName";
                 siPrice.ShowInColumn = "KeyName";
                 ASPxTreeList1.Summary.Add(siPrice);
             }
             LHeader.Text = String.Format("Исследлвание (№{0}): {1}", Rs.Id, Rs.ResearchName);

        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            ASPxTreeList1.Columns.Clear();
            
            //tl.Columns[0].Caption = "Customer";

            TreeView1.Nodes.Clear();
            int rschId;
            if (ASPxComboBox1.SelectedItem.Text != "")
            {
                //ASPxTreeList1.BeginUpdate();
                //ASPxTreeList1.Columns.Add();
                //tl.Columns[0].Caption = "Customer";
                //tl.Columns[0].VisibleIndex = 0;
                //tl.Columns.Add();
                //tl.Columns[1].Caption = "Location";
                //tl.Columns[1].VisibleIndex = 1;
                //tl.Columns.Add();
                //tl.Columns[2].Caption = "Phone";
                //tl.Columns[2].VisibleIndex = 2;
                //tl.EndUpdate();

                ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn("KeyName", "Имя ключя"));
                ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn("IsNodeInRsch", Rs.ResearchName));
                ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn("IsMoneInCompared", ASPxComboBox1.SelectedItem.Text));
                //var r = CreateNodeCore( "test root","1", "2", null);
                //CreateNodeCore("child", "2", "1", r);
                //CreateNodeCore("test root2", "3", "3", null);
                
                
                //ASPxTreeList1.AppendNode(1,new { IsNodeInRsch = "1", IsMoneInCompared = "2" }, null);
                //ASPxLabel3.Text = ASPxComboBox1.SelectedItem.Text;
                //LComparePie.Text = ASPxComboBox1.SelectedItem.Text;
            }
            //rschId = (int)ASPxComboBox1.SelectedItem.Value;
            Int32.TryParse((string)ASPxComboBox1.SelectedItem.Value, out rschId);
            CompareTrees ct = new CompareTrees();
            try
            {
                ASPxTreeList1.ClearNodes();
                var nodes = ct.GetRschTree(Rs.Id, rschId);
                for (int i = 0; i < nodes.Nodes.Count; i++) //TreeNode tn in nodes.Nodes)
                    TreeView1.Nodes.Add(nodes.Nodes[i]);
               TreeView1.Nodes[0].Text = "Сравнение ветвей реестра";
               ConvertTreeViewToTreeList(TreeView1.Nodes[0], null);
               ASPxTreeList1.ExpandAll();
            }
            catch
            {
                TreeView1.Nodes.Add(new TreeNode("Нет этементов для сравнения"));
            }
        }
        TreeListNode CreateNodeCore(string keyName, string text, string text1, TreeListNode parentNode)
        {
            i++;
            TreeListNode node = ASPxTreeList1.AppendNode(i, parentNode);
            node["KeyName"] = keyName;
            node["IsNodeInRsch"] = text;
            node["IsMoneInCompared"] = text1;
            return node;
        }

        void ConvertTreeViewToTreeList(TreeNode tvn, TreeListNode tln)
        {
            string fc = string.Empty;
            string sc = string.Empty;
            if(tvn.Text.Contains("1(+)"))
            {
                fc = "+";
            }
            if (tvn.Text.Contains("1(-)"))
            {
                fc = "-";
            }
            if (tvn.Text.Contains("2(+)"))
            {
                sc = "+";
            }
            if (tvn.Text.Contains("2(-)"))
            {
                sc = "-";
            }

            var ttln = CreateNodeCore(tvn.Text, fc, sc,tln);
            if (tvn.ChildNodes != null) //дочерние элементы есть
            {
                foreach (TreeNode childNode in tvn.ChildNodes) //не зацикливаеться ли от родителя к 1 ребенку и обратно?
                {
                    tln = ttln;
                    tvn = childNode; //посетить ребенка
                    ConvertTreeViewToTreeList(tvn, tln);
                }

            }
        }
    }
}