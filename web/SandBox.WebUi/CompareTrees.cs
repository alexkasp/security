using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using SandBox.Db;

namespace SandBox.WebUi
{
    public class CompareTrees
    {
        private class PrtAndCld
        {
            public string P;
            public string C;
        }
        private Dictionary<int, string> RootElementsCodes = new Dictionary<int, string>();
        string taskParams = "Test\\Вася";
        private void FillRootDict()
        {

            RootElementsCodes.Add(1, "HKEY_CLASSES_ROOT");
            RootElementsCodes.Add(2, "HKEY_CURRENT_USER");
            RootElementsCodes.Add(3, "HKEY_LOCAL_MACHINE");
            RootElementsCodes.Add(4, "HKEY_USERS");
            RootElementsCodes.Add(5, "HKEY_CURRENT_CONFIG");
        }
        public Dictionary<Research, List<string>> _rschFullPathesDict = new Dictionary<Research, List<string>>();
        TreeNode _LRschNode = new TreeNode();
        TreeNode _LRschToCompareNode = new TreeNode();
        //TreeNode _rootRschToCompareNode = new TreeNode();

        public TreeView GetRschTree(int rschId, int rschToCompareId, string splitter = "\\")
        {
            FillRootDict();
            List<string> rschList = new List<string>();
            List<string> rschToCompareList = new List<string>();
            Task rschTask = TaskManager.GetRegTasksForRsch(rschId);
            Task rschToCompareTask = TaskManager.GetRegTasksForRsch(rschToCompareId);
            string rschConvertedTask = GetConvertedTask(rschTask);
            string rschToCompareConvertedTask = GetConvertedTask(rschToCompareTask);
            TreeNode rschRootNode = GetRschRootNode(rschId, rschList, rschConvertedTask);

            TreeNode rschToCompareRootNode = GetRschRootNode(rschToCompareId,rschToCompareList, rschToCompareConvertedTask);

            List<string> CommonElementsList = GetCommonElements(rschList, rschToCompareList);
            List<string> OnlyInRschList = NotCommonInList(rschList, CommonElementsList);
            List<string> OnlyInRschToCompareList = NotCommonInList(rschToCompareList, CommonElementsList);
            List<PrtAndCld> pAndCList = GetPrntAndChld(CommonElementsList, OnlyInRschToCompareList, rschRootNode, rschToCompareRootNode);

            try
            {
                //_rschFullPathesDict.Add(ResearchManager.GetResearch(rschId), rschList);
                //_rschFullPathesDict.Add(ResearchManager.GetResearch(rschToCompareId), rschToCompareList);
            }
            catch
            {
                //todo: убрать слепой catch
            }

            foreach (var item in pAndCList)
            {
                TreeNode rschItem = GetTreeNodeByFullPath(item.P, rschRootNode);
                TreeNode rschToCompareItem = GetTreeNodeByFullPath(item.C, rschToCompareRootNode);
                rschToCompareItem.Text += ":1(-)2(+)";
                rschItem.ChildNodes.Add(rschToCompareItem);
                List<TreeNode> tll = new List<TreeNode>();
                GetNodesList(rschToCompareItem, tll);
                //LinkedNodes.AddRange(tll);
            }
            List<TreeNode> CommonInRsch = GetCommonInNode(CommonElementsList, rschRootNode);
            List<TreeNode> OnliInRsch = GetCommonInNode(OnlyInRschList, rschRootNode);
            foreach(var c in CommonInRsch)
            {
                c.Text += ":1(+)2(+)";
            }
            foreach (var o in OnliInRsch)
            {
                o.Text += ":1(+)2(-)";
            }

            //NotLinkedNodes = GetNotLinkedNodes(LinkedNodes, rschToCompareRootNode);

            //List<string> nlp = GetNotLinkedPathes(CommonElementsList, OnlyInRschToCompareList);
            //List<TreeNode> additional = GetUnRots
            TreeView res = new TreeView();
            res.Nodes.Add(rschRootNode);
            return res;
        }

        public void AddRschToTreeView(TreeView destTreeView, Research sourseRsch)
        {
            FillRootDict();

            List<string> destList = new List<string>();
            List<string> sourseRschList = new List<string>();

            List<TreeNode> destListNodes = new List<TreeNode>();
            GetNodesList(destTreeView.Nodes[0], destListNodes);
            foreach (TreeNode tn in destListNodes)
            {
                string path = String.Empty;
                GetFullPathForNode(tn, ref path, true);
                destList.Add(path);
            }

            Task sourseRschTask = TaskManager.GetRegTasksForRsch(sourseRsch.Id);
            string rschConvertedTask = GetConvertedTask(sourseRschTask);
            TreeNode sourseRschRootNode = GetRschRootNode(sourseRsch.Id, sourseRschList, rschConvertedTask);

            
            //_rootRschToCompareNode = rschToCompareRootNode;
            List<string> CommonElementsList = GetCommonElements(destList, sourseRschList);
            List<string> OnlyInDestRschList = NotCommonInList(destList, CommonElementsList);
            List<string> OnlyInSourseRschList = NotCommonInList(sourseRschList, CommonElementsList);
            List<PrtAndCld> pAndCList = GetPrntAndChld(CommonElementsList, OnlyInSourseRschList, destTreeView.Nodes[0], sourseRschRootNode);
            
            foreach (var item in pAndCList)
            {
                TreeNode rschItem = GetTreeNodeByFullPath(item.P, destTreeView.Nodes[0]);
                TreeNode rschToCompareItem = GetTreeNodeByFullPath(item.C, sourseRschRootNode);
                rschItem.ChildNodes.Add(rschToCompareItem);
                List<TreeNode> tll = new List<TreeNode>();
                GetNodesList(rschToCompareItem, tll);
            }
            List<TreeNode> CommonInRsch = GetCommonInNode(CommonElementsList, destTreeView.Nodes[0]);
            List<TreeNode> OnliInRsch = GetCommonInNode(OnlyInDestRschList, destTreeView.Nodes[0]);

            //NotLinkedNodes = GetNotLinkedNodes(LinkedNodes, rschToCompareRootNode);

            //List<string> nlp = GetNotLinkedPathes(CommonElementsList, OnlyInRschToCompareList);
            //List<TreeNode> additional = GetUnRots
            //

        }

        public Dictionary<Research, List<string>> GetRrschsRegPaths(List<Research> rschs)
        {
            Dictionary<Research, List<string>> res = new Dictionary<Research, List<string>>();
            foreach (var rsch in rschs)
            {
                List<string> rschList = new List<string>();
                Task rschTask = TaskManager.GetRegTasksForRsch(rsch.Id);
                string rschConvertedTask = GetConvertedTask(rschTask);
                TreeNode rschRootNode = GetRschRootNode(rsch.Id, rschList, rschConvertedTask);
                if (!res.ContainsKey(rsch)) res.Add(rsch, rschList);
            }
            return res;
        }

        private List<TreeNode> GetCommonInNode(List<string> CommonElementsList, TreeNode rschRootNode)
        {
            List<TreeNode> additional = new List<TreeNode>();
            foreach (string s in CommonElementsList)
            {
                additional.Add(GetTreeNodeByFullPath(s, rschRootNode));
            }
            return additional;
        }

        private List<TreeNode> GetNotLinkedNodes(List<TreeNode> LinkedNodes, TreeNode rschToCompareRootNode)
        {
            return null;
        }

        private List<string> GetNotLinkedPathes(List<string> CommonElementsList, List<string> OnlyInRschToCompareList)
        {
            List<string> nlp = new List<string>();
            foreach (var i in OnlyInRschToCompareList)
            {
                bool isNlp = true;
                foreach (var comm in CommonElementsList)
                {
                    if (i.StartsWith(comm))
                    {
                        isNlp = false;
                        break;
                    }
                }
                if (isNlp) nlp.Add(i);
            }
            return nlp;
        }

        private List<PrtAndCld> GetPrntAndChld(List<string> CommonElementsList, List<string> OnlyInRschToCompareList, TreeNode rootNode, TreeNode rootNodeToCompare)
        {
           

            List<PrtAndCld> res = new List<PrtAndCld>();
            foreach (var item in OnlyInRschToCompareList)
            {
                foreach (var commonItem in CommonElementsList)
                {
                    if (item.StartsWith(commonItem))
                    {
                        string tempStr = string.Empty;
                        if (item.Length > (commonItem.Length + 2))
                            tempStr = item.Substring(commonItem.Length + 1, item.Length - (commonItem.Length + 1));
                        else break;
                        if(!tempStr.Contains("\\"))
                        {
                            res.Add(new PrtAndCld(){ P=commonItem, C=item}); 
                            break;
                        }
                    }
                }
            }
            //List<TreeNode> tnlc = new List<TreeNode>();
            //GetNodesList(rootNodeToCompare, tnl);
            //foreach (var node in OnlyInRschToCompareList)
            //{

            //}
            //GetFullPathForNode(tn, ref path, true);
            return res;

        }

        private TreeNode GetTreeNodeByFullPath(string fullPath, TreeNode tn)
        {
            List<TreeNode> tnl = new List<TreeNode>();
            GetNodesList(tn, tnl);
            foreach (TreeNode item in tnl)
            {
                string fp = string.Empty;
                GetFullPathForNode(item, ref fp, true);
                if (fullPath == fp) return item; 
            }
            return null;
            
        }

        private List<string> GetCommonElements(List<string> list1, List<string> list2)
        {
            List<string> res = new List<string>();
            foreach (var item1 in list1)
            {
                foreach (var item2 in list2)
                {
                    if (item1 == item2)
                    {
                        res.Add(item1);
                        break;
                    }
                }
            }
            return res;
        }

        private List<string> NotCommonInList(List<string> list, List<string> commonElements)
        {
            List<string> res = new List<string>();
            foreach (var item in list)
            {
                bool notcommon = true;
                foreach (var commonItem in commonElements)
                {
                    if (item == commonItem)
                    {
                        notcommon = false;
                        break;
                    }
                }
                if (notcommon) res.Add(item);
            }
            return res;
        }

        private string GetConvertedTask(Task rschToCompareTask)
        {
            string res = string.Empty;
            if ((rschToCompareTask == null) || (rschToCompareTask.Value == null) || (rschToCompareTask.Value == String.Empty))
            {
                return String.Empty;
            }
            else
            {
                int numberRoot;
                bool result = Int32.TryParse(rschToCompareTask.Value.Substring(0, 1), out numberRoot);
                if(result)
                {
                    if(RootElementsCodes.Keys.Contains(numberRoot))
                    {
                        res = RootElementsCodes[numberRoot];
                        if (rschToCompareTask.Value.Length == 1) return res;
                        res+=("\\"+ rschToCompareTask.Value.Substring(1, (rschToCompareTask.Value.Length-1)));
                        return res;
                    }
                    else return rschToCompareTask.Value;

                }
                return rschToCompareTask.Value;

            }
        }

        /// <summary>
        /// Строит узел по для исследования, содержащий иерархическую структуру записей из таблицы
        /// </summary>
        /// <param name="rschId"></param>
        /// <param name="pathList">лист с идентификаторами вершин</param>
        /// <param name="taskParams"></param>
        /// <param name="splitter"></param>
        /// <returns></returns>
        private TreeNode GetRschRootNode(int rschId, List<string> pathList, string taskParams, string splitter = "\\")
        {
            #region строим дерево с добавлением вершин из таска и строим лист путей
            TreeViewBuilder tvb = new TreeViewBuilder();
            TreeNode rootNodeForRsch = new TreeNode();
            TreeNode containerForRegTree = ParseTaskParams(taskParams, "\\");//полуцили из таска для исследования цепь
            if (containerForRegTree == null)
            {
                //if(taskParams==null||taskParams=string)
                //rootNodeForRsch = new TreeNode(taskParams);
                
                if(taskParams==null||taskParams==string.Empty)
                    rootNodeForRsch = new TreeNode("mainRoot");
                    //rootNodeForRsch.ChildNodes.Add(new TreeNode(taskParams));

            }
            else
            {
                rootNodeForRsch = new TreeNode("mainRoot");
                //rootNodeForRsch =containerForRegTree; - в старой вермии только эта строка
                rootNodeForRsch.ChildNodes.Add(containerForRegTree);
            }
            TreeNode tLeaf = GetFirstLeaf(rootNodeForRsch);//получили конечный элемент для цепи параметров
            var commonTreeItems = tvb.GetRawCommonTreeItemsFromRegs(rschId);
            var rootElements = tvb.GetRootElements(commonTreeItems);
            tvb.TreeListNodeGenerator(tLeaf, commonTreeItems, rootElements);
            List<TreeNode> res = new List<TreeNode>();
            GetNodesList(rootNodeForRsch, res);
            //List<string> lisr = new List<string>();
            foreach (TreeNode tn in res)
            {
                string path = String.Empty;
                GetFullPathForNode(tn, ref path, true);
                pathList.Add(path);
            }
            try
            {
                _rschFullPathesDict.Add(ResearchManager.GetResearch(rschId), pathList);
            }
            catch
            {
                //убрать слепой catch
            }
                return rootNodeForRsch;
            #endregion
        }

        private void GetNodesPashValues(TreeNode rootNodeForRsch, List<string> resList)
        {
            resList.Add(rootNodeForRsch.ValuePath);
            if (rootNodeForRsch.ChildNodes.Count != 0) //дочерние элементы есть
            {
                foreach (TreeNode childNode in rootNodeForRsch.ChildNodes) //не зацикливаеться ли от родителя к 1 ребенку и обратно?
                {
                    rootNodeForRsch = childNode; //посетить ребенка
                    GetNodesPashValues(rootNodeForRsch, resList);
                }
            }
        }

        public void GetFullPathForNode(TreeNode treeNode, ref string path, bool isFirst = false)
        {
            if (!isFirst)
                path = treeNode.Text + "\\" + path;
            else path = treeNode.Text;
            if (treeNode.Depth != 0)
            {

                treeNode = treeNode.Parent;
                GetFullPathForNode(treeNode, ref path);
            }
        }

        //public void MergeNodes(TreeNode tn1, TreeNode tn2)
        //{

        //}





        private TreeNode ParseTaskParams(string taskParams, string splitter)
        {
            TreeNode treeNode = new TreeNode();
            if (!taskParams.Contains(splitter)) return null;
            string[] sparr = new string[] { splitter };
            string[] subStrings = taskParams.Split(sparr, StringSplitOptions.RemoveEmptyEntries);
            if (subStrings.Length > 0)
            {
                for (int i = 0; i < subStrings.Length; i++)
                {
                    if (i == 0) treeNode.Text = subStrings[i];
                    else
                    {
                        AddNodeToChain(treeNode, subStrings[i]);
                    }
                }
                return treeNode;
            }
            else return null;
        }

        /// <summary>
        /// добавляем в первый лист новый дочерний узел(используем этот метод для построения цепи)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="newNodeText"></param>
        private void AddNodeToChain(TreeNode a, string newNodeText)
        {

            if (/*a.ChildNodes != null ||*/ a.ChildNodes.Count != 0) //дочерние элементы есть
            {
                foreach (TreeNode childNode in a.ChildNodes) //не зацикливаеться ли от родителя к 1 ребенку и обратно?
                {
                    a = childNode; //посетить ребенка
                    AddNodeToChain(a, newNodeText);
                }
            }
            else
            {
                a.ChildNodes.Add(new TreeNode(newNodeText));
            }
        }

        /// <summary>
        /// Получаем первый лист(использоват для цепи)
        /// </summary>
        /// <param name="tn"></param>
        /// <returns></returns>
        private TreeNode GetFirstLeaf(TreeNode tn)
        {
            if (tn.ChildNodes != null) //дочерние элементы есть
            {
                foreach (TreeNode childNode in tn.ChildNodes) //не зацикливаеться ли от родителя к 1 ребенку и обратно?
                {
                    tn = childNode; //посетить ребенка
                    GetFirstLeaf(tn);
                }
            }
            return tn;
        }

        public void GetNodesList(TreeNode tn, List<TreeNode> tnList)
        {
            if (!tnList.Contains(tn))
            {
                tnList.Add(tn);
                if (tn.ChildNodes.Count!=0)// != null) //дочерние элементы есть
                {
                    foreach (TreeNode childNode in tn.ChildNodes) //не зацикливаеться ли от родителя к 1 ребенку и обратно?
                    {
                        tn = childNode; //посетить ребенка
                        if (!tnList.Contains(tn))
                        GetNodesList(tn, tnList);
                    }
                }
            }
        }

        private int AddNodeToEnotherNode(TreeNode sourseNode, TreeNode destanationNode)
        {
            //if (destanationNode.Checked == true) return 0;
            //if ((destanationNode.Text == sourseNode.Text) && (destanationNode.ValuePath == sourseNode.ValuePath)) {sourseNode.Checked = true; return 0;}
            //if (destanationNode. == sourseNode.Text)
            //{
            //    if()
            //}
            return -1;
        }

    }
}