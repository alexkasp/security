using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTreeList;
using SandBox.Db;

namespace SandBox.WebUi
{
    public static class ReportsBuilder
    {
        public static void RschPropsListBuilder(TreeView tw, int rschId)
        {
            var tasks = TaskManager.GetTasks(rschId).ToList<Task>();
            List<TaskNode> taskNodes = new List<TaskNode>();
            foreach (var task in tasks)
            {
                if (tw.Nodes.Count == 0)
                {
                    tw.Nodes.Add(new TreeNode(TaskManager.GetTaskDescription(task.Type)));
                    tw.Nodes[0].ChildNodes.Add(new TreeNode(task.Value));
                }
                else
                {
                    bool placed = false;
                    foreach (TreeNode n in tw.Nodes)
                    {
                        if (n.Text == TaskManager.GetTaskDescription(task.Type))
                        {
                            n.ChildNodes.Add(new TreeNode(task.Value));
                            placed = true; 
                        }
                    }
                    if (!placed)
                    {
                        TreeNode tnode = new TreeNode(TaskManager.GetTaskDescription(task.Type));
                        tnode.ChildNodes.Add(new TreeNode(task.Value));
                        tw.Nodes.Add(tnode);
                    }
                }
                //taskNodes.Add(new TaskNode(){Description = TaskManager.GetTaskDescription(task.Type), Value = task.Value});
            }
            //TaskManager.GetTaskDescription(1);
 
        }

    }

    public class TaskNode
    {
        public string Description;
        public string Value;
    }
    public class TreeListBuilder
    {
        List<string> _LColoms = new List<string>() { "Образ1", "Образ2", "Образ3" };
        List<string> TestData1 = new List<string>() {
        @"1\21\31",
        @"1\21\32",
        @"1\22\32"
    };
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    CreateColoms(ASPxTreeList1, _LColoms);
        //}

        public void CreateTreeFromStrings(List<string> sourse, string[] bounder)
        {
            List<string[]> splitedStrings = new List<string[]>();
            TreeView tv = new TreeView();
            foreach (var s in sourse)
            {
                splitedStrings.Add(s.Split(bounder, StringSplitOptions.RemoveEmptyEntries));
                foreach (var ss in splitedStrings)
                {
                    if (tv.Nodes.Count == 0)
                    {
                        if (ss.Length > 0)
                            tv.Nodes.Add(new TreeNode(ss[0]));
                        AddBranch(tv.Nodes[0], ss, 1);
                    }
                    bool plased = false;
                    for (int i = 0; i < ss.Length; i++)
                    {


                    }
                }
            }

        }

        private void AddBranch(TreeNode treeNode, string[] ss, int pos = 0)
        {
            TreeNode t = treeNode;
            for (int i = pos; pos < ss.Length - 1; pos++)
            {
                t = AddNodeToNode(t, ss[i]);
            }
        }

        private TreeNode AddNodeToNode(TreeNode treeNode, string s)
        {
            TreeNode tn = new TreeNode(s);
            treeNode.ChildNodes.Add(tn);
            return treeNode.ChildNodes[treeNode.ChildNodes.Count - 1];

        }

        private void CreateColoms(ASPxTreeList tl, List<string> coloms)
        {
            for (var i = 0; i < coloms.Count; i++)
            {
                var c = new TreeListDataColumn(coloms[i]);
                tl.Columns.Add(c);
            }
        }

        TreeNode FaindParrentNode(TreeNode node, string text)
        {
            if (node.Text == text) return node;
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                FaindParrentNode(node.ChildNodes[i], text);
            }
            return null;
        }
    }
}