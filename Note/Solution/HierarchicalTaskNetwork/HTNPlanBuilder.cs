using System;
using System.Collections.Generic;

namespace Note.Solution.HierarchicalTaskNetwork
{
    public partial class HTNPlanBuilder
    {
        private HTNPlanner planner; 
        private HTNPlanRunner runner;
        private readonly Stack<IBaseTask> taskStack;

        public HTNPlanBuilder()
        {
            taskStack = new Stack<IBaseTask>();
        }

        private void AddTask(IBaseTask task)
        {
            if (planner != null)//当前计划器不为空
            {
                //将新任务作为构造栈顶元素的子任务
                taskStack.Peek().AddNextTask(task);
            }
            else //如果计划器为空，意味着新任务是根任务，进行初始化
            {
                planner = new HTNPlanner(task as CompoundTask);
                runner = new HTNPlanRunner(planner);
            }
            //如果新任务是原子任务，就不需要进栈了，因为原子任务不会有子任务
            if (task is not PrimitiveTask)
            {
                taskStack.Push(task);
            }
        }
        
        public void RunPlan()
        {
            runner.RunPlan();
        }
        public HTNPlanBuilder Back()
        {
            taskStack.Pop();
            return this;
        }
        public HTNPlanner End()
        {
            taskStack.Clear();
            return planner;
        }
        public HTNPlanBuilder CompoundTask()
        {
            var task = new CompoundTask();
            AddTask(task);
            return this;
        }
        public HTNPlanBuilder Method(System.Func<bool> condition)
        {
            var task = new Method(condition);
            AddTask(task);
            return this;
        }
    }
}