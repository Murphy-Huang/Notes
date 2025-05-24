using System;
using System.Collections.Generic;

namespace Solution.Util
{
    public class Manager:using UnityEngine;
    
    public class Manager : MonoBehaviour 
    {
        protected bool _init = false;

        public bool Init { get { return _init; } }

        private void Start()
        {
            _init = true;
        }
    }

    public class ManageManager
    {
        // 管理所有Manager类
        private static List<Manager> lManagers = new List<Manager>();

        // 初始化管理器
        public static void Init(Action initfunc)
        {
            lManagers.Clear();
            // lManagers.Add(AddManager<CoroutineManager>());     // 协程
            // lManagers.Add(AddManager<TimerManager>());         // 定时器
            // lManagers.Add(AddManager<ABManager>());            // AssetBundle资源加载管理器
            // lManagers.Add(AddManager<NetManager>());           // Socket网络管理器
            // lManagers.Add(AddManager<HotCheckManager>());      // 检测热更资源路径                                               
            // lManagers.Add(AddManager<HotDownLoadManager>());   // 资源下载
            // lManagers.Add(AddManager<HotVersionManager>());    // 热更版本管理器
            // lManagers.Add(AddManager<LuaEnvManager>());        // XLua虚拟机
            // lManagers.Add(AddManager<UIManager>());            // UI管理器
            // lManagers.Add(AddManager<AudioMgr>());             // 音频管理器
            // lManagers.Add(AddManager<LuaScriptManager>());     // lua脚本管理器                                          
            // lManagers.Add(AddManager<SDKDelegate>());          // sdk管理器
            // lManagers.Add(AddManager<EventManager>());         // 事件管理器

            // long tid = 0;
            // tid = TimerManager.runTimeUpdate(0.01f, () =>
            // {
            //     if(isAllManagerInit())
            //     {
            //         TimerManager.RemoveTimer(tid);
            //         if (initfunc != null) initfunc();
            //     }
            // }, -1);
        }

        // 销毁管理类
        public static void Clear()
        {
            GameObject mainObj = GameObject.Find("Main");
            if (mainObj != null) UnityEngine.Object.Destroy(mainObj);
            lManagers.Clear();
            GameObject gameManagerObj = GameObject.Find("GameManagers");
            if (gameManagerObj != null) UnityEngine.Object.Destroy(gameManagerObj);
        }


        /// <summary>
        /// 添加管理器
        /// </summary>
        /// <typeparam name="T">管理器类</typeparam>
        private static T AddManager<T>() where T : Manager
        {
            GameObject mgrObj = new GameObject();
            mgrObj.name = typeof(T).ToString();
            T scriptMgr = mgrObj.AddComponent<T>();

            GameObject gameManagerObj = GameObject.Find("GameManagers");
            if (gameManagerObj == null)
            {
                gameManagerObj = new GameObject();
                gameManagerObj.name = "GameManagers";
                UnityEngine.Object.DontDestroyOnLoad(gameManagerObj);
            }
            mgrObj.transform.SetParent(gameManagerObj.transform);
            return scriptMgr;
        }

        /// <summary>
        /// 所有的管理器是否初始化完成
        /// </summary>
        /// <returns><c>true</c>, if all manager init was ised, <c>false</c> otherwise.</returns>
        public static bool isAllManagerInit()
        {
            bool isFlag = true;
            for (int i = 0; i < lManagers.Count; i++)
            {
                if (!lManagers[i].isInit)
                {
                    isFlag = false;
                    break;
                }
            }
            return isFlag;
        }
    }
}