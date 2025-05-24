using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Solution.Util;

namespace Solution.EventSystem
{
    /// <summary>
    /// 跟 CustomEventArgs.cs && EventName.cs 配合使用
    /// CustomEventArg 自定义需要传输的参数
    /// EventName 作为事件的唯一标记
    /// </summary>
    public static class EventTriggerExt
    {
        public static void TriggerEvent(this object sender, string eventName)
        {
            EventManager.Instance.TriggerEvent(eventName, sender);
        }

        public static void TriggerEvent(this object sender, string eventName, EventArgs eventArgs)
        {
            EventManager.Instance.TriggerEvent(eventName, sender, eventArgs);
        }
    }

    public class EventManager : SingleTonBase<EventManager>
    {
        private Dictionary<string, EventHandler> handlerDic = new Dictionary<string, EventHandler>();

        public void AddListener(string eventName, EventHandler handler)
        {
            if (handlerDic.ContainsKey(eventName))
                handlerDic[eventName] += handler;
            else
                handlerDic.Add(eventName, handler);
        }
        
        public void RemoveListener(string eventName, EventHandler handler)
        {
            if (handlerDic.ContainsKey(eventName))
                handlerDic[eventName] -= handler;
        }
        
        public void TriggerEvent(string eventName, object sender)
        {
            if (handlerDic.ContainsKey(eventName))
                handlerDic[eventName]?.Invoke(sender, EventArgs.Empty);
        }
        
        public void TriggerEvent(string eventName, object sender, EventArgs args)
        {
            if (handlerDic.ContainsKey(eventName))
                handlerDic[eventName]?.Invoke(sender, args);
        }
        
        public void Clear()
        {
            handlerDic.Clear();
        }
    }
}