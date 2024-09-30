using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using SimpleJson;

namespace Solution.AssetManage
{
    // HACK: 需要配合AssetPath来定位ab包的位置
    public class ABManager : MonoBehaviour 
    {
        // HACK: 不使用单例，让每一个模块有一份自已的模块资源包依赖表，再使用单例统一所有模块
        //       独立的模块可不再使用AppConst.game_tag来区分Game目录下的每一个模块
        // private static ABManager _instance;
        // public static ABManager instance 
        // {
        //     get 
        //     {
        //         if (instance == null)
        //         {
        //             Debug.LogError("ABManager not initialize");
        //         }
        //         else
        //         {
        //             return _instance;
        //         }
        //     }
        // }
        // private void Awake()
        // {
        //     _instance = this;
        // }

        /// <summary>
        /// 保存场景包
        /// </summary>
        private AssetBundle sceneAss;
        /// <summary>
        /// 存储已加载的AB资源
        /// </summary>
        public Dictionary<string, AssetBundle> dicAB = new Dictionary<string, AssetBundle>();
        /// <summary>
        /// 模块资源包依赖关系表
        /// </summary>
        public AssetBundleManifest manifest = null;

        public void OnDestroy()
        {
            Clear();
        }

        /// <summary>
        /// 保存场景包
        /// </summary>
        /// <param name="sceneAss"></param> 场景包资源<summary>
        public void SetSceneAss(AssetBundle sceneAss)
        {
            this.sceneAss = sceneAss;
        }

        /// <summary>
        /// 初始化资源包依赖信息
        /// </summary>
        public void InitManifest(string modName)
        {
        #if UNITY_EDITOR

        #else
            string manifestpath = "";
            string mainAssName  = modName + "/" + modName;
            if (HotCheckManager.ins.IsHot(mainAssName))
            {
                manifestpath = AssetPath.CachePath + mainAssName;
            }
            else
            {
                manifestpath = AssetPath.StreamingAssetPath + mainAssName;
            }

            if(!File.Exists(manifestpath))
            {
                manifestpath = AssetPath.StreamingAssetPath + mainAssName;
            }

            AssetBundle bundle = AssetBundle.LoadFromFile(manifestpath);
            manifest = bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            // 压缩包释放掉
            bundle.Unload(false);
            bundle = null;
        #endif
        }

        /// <summary>
        /// 已经加载过的资源
        /// </summary>
        /// <param name="asspath">资源路径</param>
        /// <returns></returns>
        public bool HasLoadAss(string assPath)
        {
            return dicAB.ContainsKey(assPath);
        }

        /// <summary>
        /// 清除所有资源包
        /// </summary>
        public void Clear()
        {
            foreach(KeyValuePair<string, AssetBundle> item in dicAB)
            {
                item.Value.Unload(true);
            }
            dicAB.Clear();
            if(sceneAss != null)
            {
                sceneAss.Unload(true);
                sceneAss = null;
            }
        }

        /// <summary>
        /// 加载材质球资源
        /// </summary>
        /// <param name="assPath">资源路径,带Asset的路径</param>
        /// /// <param name="oldPath">模块的非相对路径，用于获取资源在模块的相对路径</param> /// 
        /// <returns></returns>
        public Material LoadMaterial(string assPath, string oldPath)
        {
            Material mobj = null;
        #if UNITY_EDITOR
            mobj = UnityEditor.AssetDatabase.LoadAssetAtPath<Material>(assPath);
        #else
            path = assPath.Replace("\\", "/").Replace(oldPath, "");
            string[] array = Path.GetDirectoryName(path).Split('/');
            string moudulename = array[array.Length - 1].ToLower();  // 资源包名字
            string filename = Path.GetFileNameWithoutExtension(path); // 材质文件名
            string abundlepath = Path.GetDirectoryName(path).Replace(array[array.Length - 1], moudulename + ".matass"); // 材质包路径
            AssetBundle bundle = LoadAB(abundlepath); // 加载材质包
            mobj = bundle.LoadAsset<Material>(filename); // 获取对应材质
        #endif
            return mobj;
        }

#region 异步资源加载方案
        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="abPath">资源路径</param>
        /// <param name="assExt">资源包后缀名".prefabass"|".pngass"|".ttfass"</param>
        /// <param name="callback">加载完成回调函数</param>
        public void LoadABAsync(string abPath, string assExt, System.Action<AssetBundle> callback)
        {
            string asspath = abPath + assExt;  // 含后缀的路径

            if (dicAB.ContainsKey(asspath)) // 资源已经存在
            {
                callback(dicAB[asspath]);
            }
            else
            {
                // 获取资源
                List<string> abPathList = new List<string>();
                LoadABDependenciesAsync(abPathList, asspath);
                abPathList.Add(asspath);
                // CoroutineManager.ins.StartLoader(LoadABListAsyncCoroutine(abPathList, null, () =>
                // {
                //     callback(dicAB[asspath]); // 加载成功回调
                // }));
                Coroutine coroutine = LoadABListAsyncCoroutine(abPathList, null, () =>
                {
                    callback(dicAB[asspath]); // 加载成功回调
                });
                StartCoroutine(coroutine);
            }
        }

        /// <summary>
        /// 根据模块的资源依赖表加载依赖的资源路径
        /// </summary>
        /// <param name="list"></param>
        /// <param name="asspath">资源依赖表的相对路径</param>
        public void LoadABDependenciesAsync(List<string> list, string asspath)
        {
            string[] bundles = manifest.GetAllDependencies(asspath);
            foreach(string b in bundles)
            {
                list.Add(b);
            }
        }

        /// <summary>
        /// 异步加载资源包
        /// </summary>
        /// <param name="abPathList">需要加载的资源列表</param>
        /// <param name="callupdateback">每加载完一个回调</param>
        /// <param name="callback">全部加载完回调函数</param>
        /// <returns></returns>
        public IEnumerator LoadABListAsyncCoroutine(List<string> abPathList, System.Action callupdateback, System.Action callback)
        {
            for(int i=0; i < abPathList.Count; i++)
            {
                // 计算真实路径
                string assPath = abPathList[i].ToLower();  // 键值必须小写(打包生成的资源包是小写)
                string trueAssPath = ""; // 真实路径
                if (HotCheckManager.ins.IsHot(assPath))
                {
                    trueAssPath = AssetPath.CachePath + assPath;
                    Debug.Log("读取缓存abPath:<color=#00ff00>" + trueAssPath + "</color>");
                }
                else
                {
                    trueAssPath = AssetPath.StreamingAssetPath + assPath;
                }

                if (!dicAB.ContainsKey(assPath)) // 资源不存在，继续加载
                {
                    AssetBundleCreateRequest abcr = AssetBundle.LoadFromFileAsync(trueAssPath);
                    yield return abcr;
                    dicAB[assPath] = abcr.assetBundle; // 存储下来
                    Debug.LogWarning("预载入资源asyn:<color=#ffff00>" + assPath + "</color>");
                }
                else
                {
                    Debug.LogError("已存在资源:" + asskey);
                }
                if(callupdateback!=null) callupdateback(); // 每加载完一个回调
            }
            callback(); // 加载完成回调
        }
#endregion

#region 同步资源加载方案
        /// <summary>
        /// 加载对应的预设资源包
        /// </summary>
        /// <param name="abName">ab路径，不含后缀</param>
        /// <param name="prefabName">预设名称</param>
        /// <returns>返回预设对象</returns>
        public Object LoadAbPrefab(string assPath, string prefabName)
        {
            assPath = assPath + ".prefabass";

            // 获取依赖关系包，并加载进来
            List<string> list = new List<string>();
            this.LoadDependenciesABList(list, assPath);
            foreach(string b in list)
            {
                LoadAB(b);
            }

            // 加载目标包
            AssetBundle bundle = LoadAB(assPath);
            Object obj = bundle.LoadAsset(prefabName);
            return obj; // 有可能为空
        }

        /// <summary>
        /// 递归加载加载依赖包 -- 去除重复引用
        /// </summary>
        /// <param name="list"></param>
        /// <param name="assPath"></param>
        public void LoadDependenciesABList(List<string> list, string assPath)
        {
            string[] bundles = manifest.GetAllDependencies(assPath);
            foreach (string b in bundles)
            {
                if(!list.Contains(b))
                {
                    list.Add(b);
                    LoadDependenciesABList(list, b);
                }
                else
                {
                    Debug.LogError("存在重复引用的包:"+b);
                }
            }
        }

        /// <summary>
        /// 加载并返回ab
        /// </summary>
        /// <param name="assPath">ab相对路径名含后缀</param>
        /// <returns></returns>
        public AssetBundle LoadAB(string assPath)
        {
            // 注意assPath必须全部小写
            assPath = assPath.ToLower();
            if (dicAB.ContainsKey(assPath))
            {
                return dicAB[assPath];
            }
            else
            {
                string trueAssPath = "";
                if (HotCheckManager.ins.IsHot(assPath))
                {
                    trueAssPath = AssetPath.CachePath + assPath;
                    Debug.Log("读取缓存abPath:<color=#00ff00>" + trueAssPath + "</color>");
                }
                else
                {
                    trueAssPath = AssetPath.StreamingAssetPath + assPath;
                }
                AssetBundle bundle = AssetBundle.LoadFromFile(trueAssPath);
                dicAB[assPath] = bundle;
                Debug.LogWarning("同步载入资源sync:<color=#FFD700>" + assPath + "</color>");
                return bundle;
            }
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="assPath"></param>
        /// <returns></returns>
        public bool HasAB(string assPath)
        {
            // 注意assPath必须全部小写
            assPath = assPath.ToLower();
            string trueAssPath = "";
            if (HotCheckManager.ins.IsHot(assPath))
            {
                trueAssPath = AssetPath.CachePath + assPath;
                Debug.Log("读取缓存abPath:<color=#00ff00>" + trueAssPath + "</color>");
            }
            else
            {
                trueAssPath = AssetPath.StreamingAssetPath + assPath;
            }
            return File.Exists(trueAssPath);
        }
#endregion
    }
}