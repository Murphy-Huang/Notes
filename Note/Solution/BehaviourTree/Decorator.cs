using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Solution.BehaviourTree
{
    // 修饰节点 基类
    public class Decorator: Behavior
    {
        protected Behaviour child;

        public override void AddChild(Behaviour child)
        {
            this.child = child;
        }
    }
}