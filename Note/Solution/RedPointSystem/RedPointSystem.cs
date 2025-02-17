using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Solution.RedPointSystem
{
    public class RedPointSystem
    {
        public delegate void OnPointNumChange(RedPointNode node);
        private RedPointNode rootNode;
        private static List<string> listRedPointTreeList =  new List<string>
        {
            RedPointConst.Main,
            RedPointConst.Mail,
            RedPointConst.MailSystem,
            RedPointConst.MailTeam,
            RedPointConst.MailAlliance,
            RedPointConst.Task,
            RedPointConst.Alliance,
        };

        public void InitRedPointTreeNode()
        {
            rootNode = new RedPointNode();
            rootNode.nodeName = RedPointConst.Main;
            foreach (string s in listRedPointTreeList)
            {
                RedPointNode node = rootNode;
                string[] treeNodeArray = s.Split(".");
                if (treeNodeArray[0] != rootNode.nodeName)
                {
                    Debug.Log("error ")
                    continue;
                }
                if (treeNodeArray.Length > 1)
                {
                    for (int i = 0; i < treeNodeArray.Length; ++i)
                    {
                        if (!node.children.ContainsKey(treeNodeArray[i]))
                        {
                            node.children.Add(treeNodeArray[i], new RedPointNode())
                        }
                        node.children[treeNodeArray[i]].nodeName = treeNodeArray[i];
                        node.children[treeNodeArray[i]].parent = node;
                        node = node.children[treeNodeArray[i]];
                    }
                }
            }
        }

        public void SetRedPointNodeCallBack(string strNode, RedPointSystem.OnPointNumChange callBack)
        {
            List<string> nodeList = strNode.Split(".");
            if (nodeList.Length == 1)
            {
                if (nodeList[0] != RedPointConst.main)
                {
                    Debug.LogError("");
                    return;
                }
            }

            RedPointNode node = rootNode;
            for (int i = 0; i < nodeList.Length; ++i)
            {
                if (!node.children.ContainsKey(nodeList[i]))
                {
                    Debug.LogError("");
                    return;
                }
                node = node.children[nodeList[i]];
                if (i == nodeList.Length - 1)
                {
                    node.numChangeFunc = callBack;
                    return;
                }
            }
        }
    }
}