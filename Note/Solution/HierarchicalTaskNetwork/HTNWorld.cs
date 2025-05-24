using System;
using System.Collections.Generic;

namespace Note.Solution.HierarchicalTaskNetwork
{
    // 世界状态
    // 借用UnityEngine.GetInstanceID()来设置字典字符
    public static class HTNWorld
    {
        // 读 世界状态字典
        private static readonly Dictionary<string,Func<object>> get_WorldState;
        // 写 世界状态字典
        private static readonly Dictionary<string,Action<object>> set_WorldState;

        static HTNWorld()
        {
            get_WorldState = new Dictionary<string, Func<object>>();
            set_WorldState = new Dictionary<string, Action<object>>();
        }

        // 添加某个世界状态，传入状态名、读取函数和写入函数
        public static void AddState(string key, Func<object> getter, Action<object> setter)
        {
            get_WorldState[key] = getter;
            set_WorldState[key] = setter;
        }

        // 根据状态名移除某个世界状态
        public static void RemoveState(string key)
        {
            get_WorldState.Remove(key);
            set_WorldState.Remove(key);
        }

        // 修改某个状态的值
        public static void UpdateState(string key, object value)
        {
            // 写入字典修改
            set_WorldState[key].Invoke(value);
            // set_WorldState[key](value);  // 调用委托绑定的方法可以直接通过"()"调用
        }

        // 读某个状态的值，利用泛型将object转化成指定类型
        public static T GetWorldState<T>(string key)
        {
            return (T)get_WorldState[key].Invoke();
        }

        // 复制一份当前世界状态的值（主要用于规划）
        public static Dictionary<string, object> CopyWorldState()
        {
            var copy = new Dictionary<string, object>();
            foreach(var state in get_WorldState)
            {
                copy.Add(state.Key, state.Value.Invoke());
            }
            return copy;
        }
    }
}