using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Solution.BehaviourTree
{
    // 用于行为树节点信息交流
    public class Backboard
    {
        public Backboard()
        {
            // 记录当前行为，方便废弃（Abort()）
            public Behavior currentBehavior;
        }
    }
}