using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Solution.RedPointSystem
{
    public class RedPointCtrl
    {
        public void SetInvoke(string strNode, int rpNode)
        {
            List<string> nodeList = strNode.Split(".");
            if (nodeList.Length == 1)
            {
                if (nodeList[0] != RedPointConst.Main)
                {
                    Debug.LogError("");
                    return;
                }
            }

            RedPointNode node = RedPointSystem.rootNode;
            for (int i = 0; i < nodeList.Length; ++i)
            {
                if (!node.children.ContainsKey(nodeList[i]))
                {
                    Debug.LogError("" + nodeList[])
                    return;
                }
                node = node.children[nodeList[i]];
                if (i == nodeList.Length - 1)
                {
                    node.SetRedPointNum(rpNum);
                }
            }
        }
    }
}