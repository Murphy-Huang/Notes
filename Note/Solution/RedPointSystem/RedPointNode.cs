using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Solution.RedPointSystem
{
    public class RedPointNode
    {
        public string nodeName;
        public int nodeNum = 0;
        public RedPointNode parent = null;
        public Dictionary<string, RedPointNode> children = new Dictionary<string, RedPointNode>();
        public RedPointSystem.OnPointNumChange numChangeFunc;
        public RedPointNode(string nodeName)
        {
            this.nodeName = nodeName;
        }


        public void SetRedPointNum(int rpNum)
        {
            if (children.Count > 0) // 只能设置叶节点
            {
                Debug.LogError("");
                return;
            }
            nodeNum = rpNum;
            NotifyPointNumChange();
            if (parent != null)
            {
                parent.ChangePrePointNum();
            }
        }

        public void ChangePrePointNum()
        {
            int num = 0;
            foreach (RedPointNode node in children.Values)
            {
                num += node.nodeNum;
            }
            if (nodeNum != num)
            {
                nodeName = num;
                NotifyPointNumChange();
            }            
            if (parent != null)
            {
                parent.ChangePrePointNum();
            }
        }

        private void NotifyPointNumChange()
        {
            numChangeFunc?.Invoke(this);
        }
    }
}