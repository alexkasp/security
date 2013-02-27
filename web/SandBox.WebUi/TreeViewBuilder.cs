using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SandBox.Db;
using System.Web.UI.WebControls;

namespace SandBox.WebUi
{
    public class CommonTreeItem
    {
        public long ParentID = -1;
        public long ID = -1;
        public string Text = String.Empty;
        public bool IsInTree = false;
        public string ParentText = String.Empty;
    }
    class TreeViewBuilder
    {
        //pid2 идентификатор родительского процесса
        public List<CommonTreeItem> GetCommonTreeItemsFromProcs(int rschId)
        {
            List<CommonTreeItem> res = new List<CommonTreeItem>();
            var rProcs = ReportManager.GetRowProcesses(rschId);
            foreach (var p in rProcs)
            {
                var item = new CommonTreeItem() 
                {
                    ID = p.Pid1,
                    ParentID = p.Pid2!=null? (int)p.Pid2: -1,
                    Text = String.Format("{0} (pid={1}; число потоков={2})", p.Name, p.Pid1,p.Count)
                };
                res.Add(item);
            }
            return res;
        }

        public List<CommonTreeItem> GetCommonTreeItemsFromRegs(int rschId)
        {
            List<CommonTreeItem> res = new List<CommonTreeItem>();
            var rRegs = ReportManager.GetRowRegs(rschId);
            foreach (var r in rRegs)
            {
                var item = new CommonTreeItem()
                {
                    ID = r.KeyIndex,
                    ParentID = r.Parent != null ? (int)r.Parent : -1,
                    Text = String.Format("{0} (Индекс ключа={1})",r.KeyName, r.KeyIndex)
                };
                res.Add(item);
            }
            return res;
        }

        public List<CommonTreeItem> GetRawCommonTreeItemsFromRegs(int rschId)
        {
            List<CommonTreeItem> res = new List<CommonTreeItem>();
            var rRegs = ReportManager.GetRowRegs(rschId);
            foreach (var r in rRegs)
            {
                var item = new CommonTreeItem()
                {
                    ID = r.KeyIndex,
                    ParentID = r.Parent != null ? (int)r.Parent : -1,
                    Text = r.KeyName
                };
                res.Add(item);
            }
            return res;
        }

        public List<CommonTreeItem> GetRootElements(List<CommonTreeItem> commonTreeItems)
        {
            List<CommonTreeItem> res = new List<CommonTreeItem>();
            foreach (var item in commonTreeItems)
            {
                bool isRoot = true;
                foreach (var itm in commonTreeItems)
                {
                    if (itm.ID == item.ParentID && (item.Text!=itm.Text && item.ID!=itm.ID)) { isRoot = false; break; }
                }
                if (isRoot) res.Add(item);
            }
            return res;
        }

        public void TreeListViewGenerator(TreeView tv, List<CommonTreeItem> commonTreeItems, List<CommonTreeItem> rootItems, string header = "Дерево процессов")
        {
            tv.Nodes.AddAt(0, new TreeNode(header));
            for(int j=0; j<rootItems.Count(); j++)
            {
                tv.Nodes[0].ChildNodes.Add(new TreeNode(rootItems[j].Text));
                for (int k = 0; k < commonTreeItems.Count; k++ )
                {
                    if (commonTreeItems[k].ID == rootItems[j].ID) { commonTreeItems[k].IsInTree = true; }
                }
            }
            while (!IsReady(commonTreeItems))
            {
                foreach (var item in commonTreeItems)
                {
                    if (!item.IsInTree)
                    {
                        if (item.ParentText == String.Empty)
                        {
                            foreach (var it in commonTreeItems)
                            {
                                if (item.ParentID == it.ID)
                                {
                                    item.ParentText = it.Text;
                                }
                            }
                        }
                        ObxodDereva(tv.Nodes[0], item);
                    }
                }
            }
        }

        
        public void TreeListNodeGenerator(TreeNode tn, List<CommonTreeItem> commonTreeItems, List<CommonTreeItem> rootItems)
        {
           
            for (int j = 0; j < rootItems.Count(); j++)
            {
                tn.ChildNodes.Add(new TreeNode(rootItems[j].Text));
                for (int k = 0; k < commonTreeItems.Count; k++)
                {
                    if (commonTreeItems[k].ID == rootItems[j].ID) { commonTreeItems[k].IsInTree = true; }
                }
            }
            while (!IsReady(commonTreeItems))
            {
                foreach (var item in commonTreeItems)
                {
                    if (!item.IsInTree)
                    {
                        if (item.ParentText == String.Empty)
                        {
                            foreach (var it in commonTreeItems)
                            {
                                if (item.ParentID == it.ID)
                                {
                                    item.ParentText = it.Text;
                                }
                            }
                        }
                        ObxodDereva(tn, item);
                    }
                }
            }
        }    
        
        
        
        private bool IsReady(List<CommonTreeItem> commonTreeItems)
        {
            foreach (var item in commonTreeItems)
            {
                if (!item.IsInTree) return false;
            }
            return true;
        }

        private void ObxodDereva(TreeNode a, CommonTreeItem item)
        {
            
            if (a.Text == item.ParentText)
            {
                a.ChildNodes.Add(new TreeNode(item.Text));
                item.IsInTree = true;
                return;
            }
            if (a.ChildNodes.Count!=null)//a.ChildNodes != null) //дочерние элементы есть
            {
                foreach (TreeNode childNode in a.ChildNodes) //не зацикливаеться ли от родителя к 1 ребенку и обратно?
                {
                    a = childNode; //посетить ребенка
                    ObxodDereva(a, item);
                }
            }
        }
    }
}
