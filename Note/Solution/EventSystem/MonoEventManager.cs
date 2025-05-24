using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.EventSystem
{
    public class MonoEventManager: using UnityEngine;
    
    public class MonoEventManager : Manager 
    {
        private static MonoEventManager _instance;

        public static MonoEventManager ins
        {
            get 
            {
                if (_instance == null)
                {
                    Debug.Debug.LogError("MonoEventManager not init");
                }
                return _instance;
            }
        }

        private Dictionary<string, List<Action>> _eventDic = new Dictionary<string, List<Action>>();
        private Dictionary<string, List<Action<object>>> _eventDics = new Dictionary<String, List<Action<object>>>();

        public void Awake()
        {
            _instance = this;
        }

        public void AddEvent(string eventKey, Action action)
        {
            if (!_eventDic.ContainsKey(eventKey))
            {
                _eventDic[eventKey] = new List<Action>();
            }
            _eventDic[eventKey].Add(action);
        }

        public void AddEvent(string eventKey, Action<object> action)
        {
            if (!_eventDics.ContainsKey(eventKey))
            {
                _eventDics[eventKey] = new List<Action>();
            }
            _eventDics[eventKey].Add(action);   
        }

        public void RemoveEvent(string eventKey, Action action)
        {
            if (_eventDic.ContainsKey(eventKey))
            {
                if (_eventDic[eventKey].ContainsKey(action))
                {
                    _eventDic[eventKey].Remove(action);
                }
            }
        }

        public void RemoveEvent(string eventKey, Action<object> action)
        {
            if (_eventDics.ContainsKey(eventKey))
            {
                if (_eventDics[eventKey].ContainsKey(action))
                {
                    _eventDics[eventKey].Remove(action);
                }
            }
        }

        public void Dispatch(string eventKey)
        {
            if (_eventDic.ContainsKey(eventKey))
            {
                var enumerator = _eventDic[eventKey];
                for (int i = 0; i < enumerator.Count; ++i)
                {
                    enumerator[i]();
                }
            }
        }

        public void Dispatch(string eventKey, object obj)
        {
            if (_eventDics.ContainsKey(eventKey))
            {
                var enumerator = _eventDics[eventKey];
                for (int i = 0; i < enumerator.Count; ++i)
                {
                    enumerator[i](obj);
                }
            }
        }

        private void OnDestroy() {
            _eventDic.Clear();    
            _eventDics.Clear();    
            _instance = null;
        }
    }
}