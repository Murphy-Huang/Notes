using System;
using System.Collections.Generic;

namespace Note.Solution.HierarchicalTaskNetwork
{
    //用于描述运行结果的枚举
    public enum EStatus
    {
        Invalid, Failure, Success, Running
    }

    // 任务类接口
    public interface IBaseTask
    {
        // 是否满足条件
        bool MetCondition(Dictionary<string, object> worldState);
        // 添加子任务
        void AddNextTask(IBaseTask nextTask);
    }
}