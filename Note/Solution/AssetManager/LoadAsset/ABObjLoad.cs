using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Solution.AssetManage
{
    /// <summary>
    /// 对ABManager的封装使用，僅作爲參考，需要根据项目的结构来修改
    /// </summary>
    public class ABObjLoad
    {
        /// <summary>
        /// 资源路径信息
        /// </summary>
        private struct ResPathInfo
        {
            public string moduleName; // 模块名
            public string modulePath; // 模块路径
            public string fileName;   // 文件名

            public ResPathInfo(string moduleName, string modulePath, string fileName)
            {
                this.moduleName = moduleName;
                this.modulePath = modulePath;
                this.fileName   = fileName;
            }
        }

        /// <summary>
        /// 解析资源路径信息
        /// </summary>
        /// <param name="res_path">Assets目录的资源相对路径</param>
        /// <param name="old_path">模块的非相对路径，用于获取资源在模块的相对路径</param> 
        /// <returns></returns>
        private static ResPathInfo ParseResPath(string res_path, string old_path)
        {
            /*
                "Games/slotwatermargin/Res/effect/effect_ani/bird"
                respath = slotwatermargin/Res/effect/effect_ani/bird
                moduleName = effect_ani;
                modulePath = slotwatermargin/Res/effect/effect_ani;
                fileName = bird;
                改造成移除game_tag，期望结果
                respath = Res/effect/effect_ani/bird
                moduleName = effect_ani;
                modulePath = Res/effect/effect_ani;
                fileName = bird;
            */
            string respath;
            if (res_path.EndsWith(".prefab"))
            {
                respath = res_path.Replace(old_path, "").Replace(".prefab", "");
            }
            else
            {
                respath = res_path.Replace(old_path, "");
            }
            string moduleName = ""; // 模块名称
            string modulePath = ""; // 模块相对路径
            string fileName   = ""; // 要获取的文件名称
            if (respath.Contains("/module/"))
            {
                moduleName = respath.Substring(respath.IndexOf("module/")).Split('/')[1];
                modulePath = respath.Substring(0, respath.IndexOf("/" + moduleName)) + "/" + moduleName;
                fileName = Path.GetFileNameWithoutExtension(respath);
            }
            else if (respath.Contains("/global/"))
            {
                modulePath = respath;
                fileName = Path.GetFileNameWithoutExtension(respath);
            }
            else if (respath.Contains("/effect/"))
            {
                moduleName = respath.Substring(respath.IndexOf("effect/")).Split('/')[1];
                modulePath = respath.Substring(0, respath.IndexOf("/" + moduleName)) + "/" + moduleName;
                fileName = Path.GetFileNameWithoutExtension(respath);
            }
            else if (respath.Contains("/share/"))
            {
                moduleName = respath.Substring(respath.IndexOf("share/")).Split('/')[1];
                modulePath = respath.Substring(0, respath.IndexOf("/" + moduleName)) + "/" + moduleName;
                fileName = Path.GetFileNameWithoutExtension(respath);
            }
            else if (respath.Contains("/language/"))
            {
                moduleName = respath.Substring(respath.IndexOf("language/")).Split('/')[1];
                modulePath = respath.Substring(0, respath.IndexOf("/" + moduleName)) + "/" + moduleName;
                fileName = Path.GetFileNameWithoutExtension(respath);
            }
            else
            {
                Debug.LogError("未知的资源路径=" + res_path);
            }

            return new ResPathInfo(moduleName, modulePath.ToLower(), fileName);
        }

        /// <summary>
        /// 解析资源路径信息
        /// </summary>
        /// <param name="respath">模块内资源相对路径</param>
        /// <returns></returns>
        private static ResPathInfo ParseResPath(string resPath)
        {
            string moduleName = ""; // 模块名称
            string modulePath = ""; // 模块相对路径
            string fileName   = ""; // 要获取的文件名称
            // moduleName = resPath.Substring(respath.IndexOf("module/")).Split('/')[1];
            // modulePath = resPath.Substring(0, respath.IndexOf("/" + moduleName)) + "/" + moduleName;
            fileName = Path.GetFileNameWithoutExtension(resPath);
            modulePath = resPath.Substring(0, resPath.IndexOf("/" + fileName));

            return new ResPathInfo(moduleName, modulePath.ToLower(), fileName);
        }

        /// <summary>
        /// 直接加载预设资源
        /// </summary>
        /// <param name="prefabPath">模块内路径</param>
        /// <param name="modPath">模块路径</param>
        /// <returns></returns>
        public static GameObject LoadPrefab(string prefabPath, string modPath, ABManager abManager = null)
        {
            GameObject prefab = null;
    #if UNITY_EDITOR
            prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/" + modPath + prefabPath);
    #else
            ResPathInfo info = ParseResPath(prefabPath);
            Object prefabobj = abManager.LoadAbPrefab(info.modulePath, info.fileName);
            if(prefabobj == null)
            {
                Debug.LogError("fail to LoadPrefab modulePath=" + info.modulePath);
            }
            prefab = prefabobj as GameObject;
    #endif
            return prefab;
        }

        /// <summary>
        /// 加载一个精灵图片
        /// </summary>
        /// <param name="tPath">路径:不包含"Assets" 需带文件后缀</param>
        /// <param name="modPath">资源目录对应所在的模块路径</param>
        /// <param name="perUnit">缩放比列</param>
        /// <returns></returns>
        public static Sprite LoadSprite(string tPath, string modPath, int perUnit, ABManager abManager = null)
        {
            Sprite objpng = null;
    #if UNITY_EDITOR
            string path = "Assets/" + modPath + tPath;
            Sprite sp = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(path);
            if (sp == null)
            {
                Debug.LogError("获取不到图片path=" + path);
                return null;
            }
            objpng = sp;
    #else
            ResPathInfo info = ParseResPath(tPath);
            AssetBundle budle = abManager.LoadAB(info.modulePath + ".pngass");
            Texture2D t2d = budle.LoadAsset<Texture2D>(info.fileName);
            if (t2d == null)
            {
                objpng = budle.LoadAsset<Sprite>(info.fileName);
                if (objpng == null)
                {
                    Debug.LogError("获取不到图片path=" + tPath);
                    return null;
                }
            }
            else
            {
                objpng = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f), perUnit);
                objpng.name = t2d.name;
            }
    #endif
            return objpng;
        }

#region Lua加载
        /// <summary>
        /// Lua加载序列帧特效资源
        /// <param name="modPath">资源目录对应所在的模块路径</param>
        /// </summary>
        public static Sprite[] LoadLuaSprites(string effectDir, string modPath, ABManager aBManager)
        {
            List<Sprite> list = LoadSprites(effectDir, modPath, abManager);
            Sprite[] slist = list.ToArray();
            list = null;
            return slist;
        }

        /// <summary>
        /// 按升序返回指定目錄的sprite列表
        /// </summary>
        /// <param name="effectDir">模块内的资源路径</param>
        /// <param name="modPath">资源目录对应所在的模块路径</param>
        /// <returns></returns> <summary>
        public static List<Sprite> LoadSprites(string effectDir, string modPath, ABManager abManager = null)
        {
            List<Sprite> list = new List<Sprite>();
    #if UNITY_EDITOR
            string effectPath = Application.dataPath + "/" + modPath + "/" + effectDir;
            List<string> pnglist = UtilityFile.GetFiles(effectPath, ".png");
            pnglist.Sort((x, y) =>
            {
                string nx = Path.GetFileName(x).Replace(".png", "");
                string ny = Path.GetFileName(y).Replace(".png", "");
                return nx.CompareTo(ny);
            });
            foreach (string pngpath in pnglist)
            {
                string spritepath = pngpath.Replace("\\", "/").Replace(Application.dataPath, "Assets");
                Sprite sp = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(spritepath);
                if (sp == null)
                {
                    Debug.LogError("获取不到序列帧图片path=" + spritepath);
                    return null;
                }
                list.Add(sp);
            }
    #else
            if (abManager != null)
            {
                string spritepath = effectDir + ".pngass"; 
                spritepath = spritepath.Replace(modPath, "");

                AssetBundle bundle = abManager.LoadAB(spritepath.ToLower());
                Object[] assets = bundle.LoadAllAssets();
                foreach (var obj in assets)
                {
                    if (obj is Sprite)
                    {
                        Sprite sp = obj as Sprite;
                        list.Add(sp);
                    }
                }
                list.Sort((x, y) =>
                {
                    int xv = 0;
                    if (int.TryParse(x.name, out xv) && int.TryParse(y.name, out xv))
                    {
                        return int.Parse(x.name).CompareTo(int.Parse(y.name));  // 按照整型排序
                    }
                    else
                    {
                        if (x.name.Length == y.name.Length)
                        {
                            return x.name.CompareTo(y.name);  // 排序
                        }
                        else if (x.name.Length < y.name.Length)
                        {
                            return -1;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                });
            }
            
    #endif
            return list;
        }
#endregion

        /// <summary>
        /// 加载一个音频文件
        /// </summary>
        /// <param name="path">带后缀</param>
        /// <param name="modPath">模块的Asset相对路径</param>
        /// <returns></returns>
        public static AudioClip LoadAudio(string path, string modPath)
        {
            AudioClip audio = null;
    #if UNITY_EDITOR
            audio = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/" + modPath + path);
    #else
            string respath    = path.Replace("Assets/" + modPath, "");
            string moduleName = respath.Substring(respath.IndexOf("audio/")).Split('/')[1];
            string filename   = Path.GetFileNameWithoutExtension(respath);
            string assPath    = respath.Substring(0, respath.IndexOf("/" + moduleName)) + "/audio.audioass";
            AssetBundle bundle = ABManager.ins.LoadAB(assPath);
            audio = bundle.LoadAsset<AudioClip>(filename);
    #endif
            if (audio == null)
            {
                Debug.LogError("找不到音频文件:path=" + path);
                return null;
            }
            else
            {
                return audio;
            }
        }

        /// <summary>
        /// 加载一个ComputeShader文件
        /// </summary>
        /// <param name="path">带后缀</param>
        /// <param name="modPath">模块的Asset相对路径</param>
        /// <returns></returns>
        public static ComputeShader LoadCShader(string path, string modPath)
        {
            ComputeShader cs = null;
    #if UNITY_EDITOR
                cs = UnityEditor.AssetDatabase.LoadAssetAtPath<ComputeShader>(path);
    #else
                string respath = path.Replace("Assets/" + modPath, "");
                string moduleName = respath.Substring(respath.IndexOf("cshader/")).Split('/')[1];
                string filename = Path.GetFileNameWithoutExtension(respath);
                string assPath = respath.Substring(0, respath.IndexOf("/" + moduleName)) + "/cshader.shass";
                Debug.LogError(assPath);
                AssetBundle bundle = ABManager.ins.LoadAB(assPath);
                cs = bundle.LoadAsset<ComputeShader>(filename);   // 不能用的
    #endif
            if (cs == null)
            {
                Debug.LogError("找不到ComputeShader文件:path=" + path);
                return null;
            }
            else
            {
                return cs;
            }
        }
    }
}