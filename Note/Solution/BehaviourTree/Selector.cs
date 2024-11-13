using System;
using System.Collections.Generic;

namespace Note.Solution.BehaviourTree
{
    // 选择器 当其中一个子节点执行成功就算运行成功
    public class Selector : Sequence
    {
        protected override EStatus OnUpdate()
        {
            while(true)
            {
                var s = currentChild.Value.Tick();
                if( s != EStatus.Failure)
                    return s;
                currentChild = currentChild.Next;
                if(currentChild == null)
                    return EStatus.Failure;
            }
        }
    }
}
