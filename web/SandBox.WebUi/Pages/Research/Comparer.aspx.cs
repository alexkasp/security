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
using System.Drawing;
using System.Collections.Generic;

namespace SandBox.WebUi.Pages.Research
{
    public partial class Comparer : BaseMainPage
    {
        int i = 0;
        public Db.Research Rs, EtalonRs;
        public Db.Research etalonRsch = null;
        CompareTrees ct = new CompareTrees();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Сравнение веток реестра";
            PageMenu = "~/App_Data/SideMenu/Research/ResearchMenu.xml";
            Int32 researchId = Convert.ToInt32(Request.QueryString["research"]);
            Rs = ResearchManager.GetResearch(researchId);
            var etalonRsch = ResearchManager.GetEtalonRsch(researchId);
            UpdateTreeView(true);
            if (Rs == null)
            {
                Response.Redirect("~/Error");
            }
            if (etalonRsch == null)
            {
                lEtalonEx.Text = "Эталонного ислледования не найдено";
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
             //LHeader.Text = String.Format("Исследлвание (№{0}): {1}", Rs.Id, Rs.ResearchName);

        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            //if (etalonRsch == null)
            //{
            //    ASPxTreeList1.Columns.Clear();

            //    //tl.Columns[0].Caption = "Customer";

            //    TreeView1.Nodes.Clear();
            //    int rschId;
            //    if (ASPxComboBox1.SelectedItem.Text != "")
            //    {
            //        ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn("KeyName", "Имя ключя"));
            //        ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn("IsNodeInRsch", Rs.ResearchName));
            //        ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn("IsNodeInCompared", ASPxComboBox1.SelectedItem.Text));
            //    }
            //    Int32.TryParse((string)ASPxComboBox1.SelectedItem.Value, out rschId);
            //    CompareTrees ct = new CompareTrees();
            //    try
            //    {
            //        ASPxTreeList1.ClearNodes();
            //        var nodes = ct.GetRschTree(Rs.Id, rschId);
            //        for (int i = 0; i < nodes.Nodes.Count; i++) //TreeNode tn in nodes.Nodes)
            //            TreeView1.Nodes.Add(nodes.Nodes[i]);
            //        //TreeView1.Nodes[0].Text = "Сравнение ветвей реестра";
            //        List<TreeNode> res = new List<TreeNode>();
            //        ct.GetNodesList(nodes.Nodes[0], res);
            //        List<string> pathList = new List<string>();
            //        foreach (TreeNode tn in res)
            //        {
            //            string path = String.Empty;
            //            ct.GetFullPathForNode(tn, ref path, true);
            //            pathList.Add(path);
            //        }

            //        ConvertTreeViewToTreeList(pathList, TreeView1.Nodes[0], null, ct);
            //        ASPxTreeList1.ExpandAll();
            //    }
            //    catch
            //    {
            //        TreeView1.Nodes.Add(new TreeNode("Нет этементов для сравнения"));
            //    }
            //}
            //else
            //{
                
            //}
        }


        private void UpdateTreeView(bool testMode = false)
        {
            #region testing


            #endregion

            if (etalonRsch == null&&!testMode)
            {
               
                if (ASPxComboBox1.SelectedItem.Text != "")
                {
                    #region
                    TreeView1.Nodes.Clear();
                    int rschId;

                    ASPxTreeList1.Columns.Clear();
                    ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn("KeyName", "Имя ключя"));
                    ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn("IsNodeInRsch", Rs.ResearchName));
                    ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn("IsNodeInCompared", ASPxComboBox1.SelectedItem.Text));
                   
                    
                    ct._rschFullPathesDict.Clear();
                    try
                    {

                        ASPxTreeList1.ClearNodes();
                        Int32.TryParse((string)ASPxComboBox1.SelectedItem.Value, out rschId);
                        var nodes = ct.GetRschTree(Rs.Id, rschId);
                        for (int i = 0; i < nodes.Nodes.Count; i++) //TreeNode tn in nodes.Nodes)
                            TreeView1.Nodes.Add(nodes.Nodes[i]);

                        List<TreeNode> res = new List<TreeNode>();
                        ct.GetNodesList(nodes.Nodes[0], res);
                        List<string> pathList = new List<string>();
                        foreach (TreeNode tn in res)
                        {
                            string path = String.Empty;
                            ct.GetFullPathForNode(tn, ref path, true);
                            pathList.Add(path);
                        }

                        ConvertTreeViewToTreeList(pathList,TreeView1.Nodes[0], null, ct);
                        ASPxTreeList1.ExpandAll();
                    }
                    catch
                    {
                        TreeView1.Nodes.Add(new TreeNode("Нет этементов для сравнения"));
                    }
                    #endregion
                }
               
            }
            else
            {
                if (/*ASPxComboBox1.SelectedItem.Text == "" &&*/ !testMode)
                {
                    #region
                    ct._rschFullPathesDict.Clear();
                    TreeView1.Nodes.Clear();
                    int rschId = etalonRsch.Id;

                    ASPxTreeList1.Columns.Clear();
                    ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn("KeyName", "Имя ключя"));
                    ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn("IsNodeInRsch", Rs.ResearchName));
                    ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn("IsNodeInCompared", ASPxComboBox1.SelectedItem.Text));

                    try
                    {
                        ASPxTreeList1.ClearNodes();
                        var nodes = ct.GetRschTree(Rs.Id, rschId);
                        for (int i = 0; i < nodes.Nodes.Count; i++) //TreeNode tn in nodes.Nodes)
                            TreeView1.Nodes.Add(nodes.Nodes[i]);
                        //TreeView1.Nodes[0].Text = "Сравнение ветвей реестра";

                        List<TreeNode> res = new List<TreeNode>();
                        ct.GetNodesList(nodes.Nodes[0], res);
                        List<string> pathList = new List<string>();
                        foreach (TreeNode tn in res)
                        {
                            string path = String.Empty;
                            ct.GetFullPathForNode(tn, ref path, true);
                            pathList.Add(path);
                        }

                        ConvertTreeViewToTreeList(pathList, TreeView1.Nodes[0], null, ct);
                        ASPxTreeList1.ExpandAll();
                    }
                    catch
                    {
                        TreeView1.Nodes.Add(new TreeNode("Нет этементов для сравнения"));
                    }
                    #endregion
                }
                else
                { 
                    
                    #region
                    ct._rschFullPathesDict.Clear();
              
                    TreeView1.Nodes.Clear();
                    int rschId;
                    if (!testMode)
                        rschId = etalonRsch.Id;
                    else
                    {
                        rschId = 105;
                        Rs = ResearchManager.GetResearch(104);
                        etalonRsch = ResearchManager.GetResearch(105);

                    }
                    ASPxTreeList1.Columns.Clear();
                    ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn("KeyName", "Имя ключя"));
                    ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn(Rs.ResearchName, Rs.ResearchName));
                    ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn(etalonRsch.ResearchName, etalonRsch.ResearchName));

                    try
                    {
                        ASPxTreeList1.ClearNodes();
                        var nodes = ct.GetRschTree(Rs.Id, etalonRsch.Id);
                        for (int i = 0; i < nodes.Nodes.Count; i++) //TreeNode tn in nodes.Nodes)
                            TreeView1.Nodes.Add(nodes.Nodes[i]);

                        if (!testMode) Int32.TryParse((string)ASPxComboBox1.SelectedItem.Value, out rschId);
                        else rschId = 105;
                        ct.AddRschToTreeView(nodes, ResearchManager.GetResearch(rschId));
                        ASPxTreeList1.Columns.Add(new DevExpress.Web.ASPxTreeList.TreeListDataColumn( ResearchManager.GetResearch(rschId).ResearchName, ResearchManager.GetResearch(rschId).ResearchName));

                        List<TreeNode> res = new List<TreeNode>();
                        ct.GetNodesList(nodes.Nodes[0], res);
                        List<string> pathList = new List<string>();
                        foreach (TreeNode tn in res)
                        {
                            string path = String.Empty;
                            ct.GetFullPathForNode(tn, ref path, true);
                            pathList.Add(path);
                        }

                        ConvertTreeViewToTreeList(pathList, nodes.Nodes[0], null, ct);

                        ASPxTreeList1.ExpandAll();
                    }
                    catch
                    {
                        TreeView1.Nodes.Add(new TreeNode("Нет этементов для сравнения"));
                    }
                    #endregion
                }

            }
        }

        TreeListNode CreateNodeCore(List<string> treeFullPathes, TreeNode tvn, TreeListNode tln, CompareTrees ctt)
        {
            i++;
            TreeListNode node = ASPxTreeList1.AppendNode(i, tln);
            node["KeyName"] = tvn.Text;
            foreach (var key in ct._rschFullPathesDict.Keys)
            {
                string test = key.ResearchName;
                string nodeFullPath=String.Empty;
                ct.GetFullPathForNode(tvn, ref nodeFullPath);
                if(ct._rschFullPathesDict[key].Contains(nodeFullPath))
                    node[test] = "+";
                else
                    node[test] = "-";
            }
            return node;
        }

        void ConvertTreeViewToTreeList(List<string> treeFullPathes, TreeNode tvn, TreeListNode tln, CompareTrees ctt)
        {
            var ttln = CreateNodeCore(treeFullPathes, tvn, tln,ct);
            if (tvn.ChildNodes != null) //дочерние элементы есть
            {
                foreach (TreeNode childNode in tvn.ChildNodes) //не зацикливаеться ли от родителя к 1 ребенку и обратно?
                {
                    tln = ttln;
                    tvn = childNode; //посетить ребенка
                    ConvertTreeViewToTreeList(treeFullPathes, tvn, tln, ct);
                }

            }
        }

        protected void ASPxTreeList1_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
        {
            string value = (string)e.CellValue;
            if (value == "+")
            {
                e.Cell.BackColor = Color.Green;
            }
            if (value == "-")
            {
                e.Cell.BackColor = Color.Red;
            }
        }
    }
}