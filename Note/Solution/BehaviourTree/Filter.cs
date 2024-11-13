using System;
using System.Collections.Generic;

namespace Note.Solution.BehaviourTree
{
    // 过滤器
    public class Filter : Sequence
    {
        // 将条件判断直接看作在同一个列表中，条件在前半段，真正运行的节点在后半段
        public void AddCondition(Behavior condition)//添加条件，就用头插入
        {
            children.AddFirst(condition);
        }
        public void AddAction(Behavior action)//添加动作，就用尾插入
        {
            children.AddLast(action);
        }
    }
}