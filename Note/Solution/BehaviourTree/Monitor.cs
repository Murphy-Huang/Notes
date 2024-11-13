using System;
using System.Collections.Generic;

namespace Note.Solution.BehaviourTree
{
    // 监视器 从逻辑上看，它有两个子树，一边负责条件，一边负责具体行为，
    // 防止同步和争用的问题，因为只有一个子树将运行对世界进行更改的操作。
   public class Monitor: Parallel
    {
        public Monitor(Policy mSuccessPolicy, Policy mFailurePolicy)
        : base(mSuccessPolicy, mFailurePolicy)
        {
        }
        public void AddCondition(Behavior condition)
        {
            children.AddFirst(condition);
        }
        public void AddAction(Behavior action)
        {
            children.AddLast(action);
        }
    }
}