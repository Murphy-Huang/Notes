using System;
using System.Collections.Generic;

namespace Note.Solution.HierarchicalTaskNetwork
{
    // 原子任务，最小单元任务
    // 条件判断和执行影响都要分两种情况，一种是规划时，一种是实际执行时，
    // 因为规划时我们使用的并不是真正的世界状态，而是一份模拟的世界状态副本
    public abstract class PrimitiveTask : IBaseTask
    {
        // 原子任务不可以再分解为子任务，所以AddNextTask方法不必实现
        void IBaseTask.AddNextTask(IBaseTask nextTask)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 执行前判断条件是否满足，传入null时直接修改HTNWorld
        /// </summary>
        /// <param name="worldState">用于plan的世界状态副本</param>
        public bool MetCondition(Dictionary<string, object> worldState = null)
        {
            if(worldState == null)//实际运行时
            {
                return MetCondition_OnRun();
            }
            else//模拟规划时，若能满足条件就直接进行Effect
            {
                if(MetCondition_OnPlan(worldState))
                {
                    Effect_OnPlan(worldState);
                    return true;
                }
                return false;
            }
        }
        protected virtual bool MetCondition_OnPlan(Dictionary<string, object> worldState)
        {
            return true;
        }
        protected virtual bool MetCondition_OnRun()
        {
            return true;
        }

        //任务的具体运行逻辑，交给具体类实现
        // 任务具有一定运行过程，可以用一个bool值在Operator函数内部判断是否完成动作
        public abstract EStatus Operator();

        /// <summary>
        /// 执行成功后的影响，传入null时直接修改HTNWorld
        /// </summary>
        /// <param name="worldState">用于plan的世界状态副本</param>
        public void Effect(Dictionary<string, object> worldState = null)
        {
            Effect_OnRun();
        }
        protected virtual void Effect_OnPlan(Dictionary<string, object> worldState)
        {
            
        }
        protected virtual void Effect_OnRun()
        {
            
        }
    }
}