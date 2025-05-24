using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Solution.AssetPack;
using Solution.Util.UtilityFile;

namespace Solution.AssetManage
{
    public struct PackItem
    {
        public string file_name;   // 文件名
        public string file_ext;    // 后缀
        public string path;        // 文件绝对路径
        public string mod_name;    // 所属游戏模块名称
        public string sub_mod_name;// 所属子模块名称
        public string img_tag;     // 图集包标记
        public string bundle_name; // 生成的资源包名称
        public string bundle_ext;  // 生成的资源包后缀
    }

    public class PackAssetBundleData
    {
        public PackAssetBundleData()
        {
            private string bundle_name; // 生成的资源包名称
            private string bundle_ext;  // 生成的资源包后缀
            public List<string> asset_names = new List<string>();

            public PackAssetBundleData(string bundle_name, string bundle_ext)
            {
                this.bundle_name = bundle_name;
                this.bundle_ext = bundle_ext;
            }

            /// <summary>
            /// 获取包名后缀
            /// </summary>
            public string Ext
            {
                get
                {
                    return bundle_ext;
                }
            }

            /// <summary>
            /// 包路径-小写
            /// </summary>
            public string bundle_path
            {
                get
                {
                    return (bundle_name + "." + bundle_ext).ToLower();
                }
            }

            /// <summary>
            /// 添加一个资源
            /// </summary>
            /// <param name="asset_path"></param>
            public void AddAsset(string asset_path)
            {
                asset_names.Add(asset_path);
            }

            /// <summary>
            /// 生成对应的AssetBundleBuild结构
            /// </summary>
            /// <returns></returns>
            public AssetBundleBuild GetAssetBundleBuild()
            {
                AssetBundleBuild b = new AssetBundleBuild();
                b.assetBundleName = bundle_path;
                b.assetBundleVariant = ""; // 变体参数
                b.assetNames = asset_names.ToArray();
                return b;
            }
        }
    }

    public class AssetPackCtrl
    {
        /// <summary>
        /// 获取目标目录的所有模块名称
        /// </summary>
        /// <param name="directory">Assets目录下的相对路径</param>
        /// <returns></returns>
        public static List<string> GetMods(string directory)
        {
            List<string> mods = new List<string>();
            List<string> files = UtilityFile.GetDirs(Application.dataPath + "/" + directory);
            foreach (string f in files)
            {
                string modName = Path.GetFileNameWithoutExtension(f);
                mods.Add(modName);
            }
            return mods;
        }

#region 图集标记
        /// <summary>
        /// 获取Texture2D类型
        /// </summary>
        /// <param name="path">Asset开头的项目相对路径</param>
        /// <returns></returns>
        private static TextureImporter GetTextureSettings(string path)
        {
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            if (textureImporter && textureImporter.textureType == TextureImporterType.Sprite)
            {
                return textureImporter;
            }

            return null;
        }

        /// <summary>
        /// 清理图集的标记
        /// </summary>
        /// <param name="alist"></param>
        /// <returns></returns>
        public static void ClearImgTags(List<PackItem> alist)
        {
            string oldstr = Application.dataPath.Replace("/", "\\").Replace("Assets", "");
            string texurl = Application.dataPath + "/" + directory;
            string ext = ".png|.jpg";   // 图片类型
            foreach (PackItem item in alist)
            {
                if (ext.IndexOf(item.path.Substring(item.path.LastIndexOf(".") + 1)) == -1) continue;
                TextureImporter t = GetTextureSettings(item.path.Replace(oldstr, ""));
                if (t != null)
                {
                    t.spritePackingTag = "";
                    t.SaveAndReimport();
                }
            }

            // List<string> files = UtilityFile.GetFiles(texurl, ".png|.jpg"); // 获取目录下所有图片
            // foreach (string f in files)
            // {
            //     TextureImporter t = GetTextureSettings(f.Replace(oldstr, ""));
            //     if (t != null)
            //     {
            //         t.spritePackingTag = "";
            //         t.SaveAndReimport();
            //     }
            // }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        // 打包时是否标记平台图片标示
        public static bool isBuildPlatformTextureSettings = false; 

        /// <summary>
        /// 设定图集打包标记
        /// </summary>
        /// <param name="alist"></param>
        public static void BuildImgTag(List<PackItem> alist)
        {
            // 定位到和Assets同级别的目录
            string oldstr = Application.dataPath.Replace("/", "\\").Replace("Assets", "");
            foreach (PackItem item in alist)
            {
                if (string.IsNullOrEmpty(item.img_tag)) continue;
                TextureImporter t = GetTextureSettings(item.path.Replace(oldstr, ""));
                if (t && t.spritePackingTag != item.img_tag)
                {
                    t.spritePackingTag = item.img_tag;
                    t.SaveAndReimport();
                }
                // HACK: 自己设定图片的导入格式
                if (t != null && isBuildPlatformTextureSettings)
                {
                    // 安卓设置
                    TextureImporterPlatformSettings androidSetting = new TextureImporterPlatformSettings();
                    androidSetting.name = "Android";
                    androidSetting.maxTextureSize = t.maxTextureSize;
                    androidSetting.format = TextureImporterFormat.ETC2_RGBA8;
                    androidSetting.compressionQuality = 100;
                    androidSetting.allowsAlphaSplitting = true;
                    androidSetting.androidETC2FallbackOverride = AndroidETC2FallbackOverride.Quality16Bit;
                    androidSetting.overridden = true;
                    // ios设置
                    TextureImporterPlatformSettings iosSetting = new TextureImporterPlatformSettings();
                    iosSetting.name = "iPhone";
                    iosSetting.maxTextureSize = t.maxTextureSize;
                    iosSetting.format = TextureImporterFormat.ASTC_6x6;
                    iosSetting.compressionQuality = 100;
                    iosSetting.allowsAlphaSplitting = true;
                    iosSetting.overridden = true;
                    t.SetPlatformTextureSettings(androidSetting);
                    t.SetPlatformTextureSettings(iosSetting);
                    t.SaveAndReimport();
                }
            }
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }
#endregion

        /// <summary>
        /// 解析生成目标目录下所有文件的配置信息（PackItem格式）
        /// </summary>
        /// <param name="directory">Assets目录下的相对路径</param>
        /// <returns></returns>
        public static List<PackItem> ParseAllAssets(string directory)
        {
            List<PackItem> alist = new List<PackItem>();
            string oldstr = Application.dataPath.Replace("/", "\\").Replace("Assets", "");
            string texurl = Application.dataPath + "/" + directory;
            List<string> files = UtilityFile.GetFiles(texurl, ".png|.jpg|.OGG|.ogg|.mp3|.wav|.prefab|.mat|.json|.fontsettings|.compute|.unity"); // 获取目录下所有文件和图片
            foreach (string f in files)
            {
                // HACK: 根据目录结构设计模块结构
                string mod_path = f.Replace(oldstr, "").Replace("Assets\\" + directory.Substring(0, directory.LastIndexOf("/") + 1).Replace("/", "\\"), "");
                string[] paths = mod_path.Split('\\');
                string mod_name = paths[0]; // 主模块名称
                string res_name = paths[1];
                string subdir_name = paths[2]; // 子模块目录名
                string file_name = Path.GetFileNameWithoutExtension(f);
                string file_ext = Path.GetExtension(f).Replace(".","");

                // HACK: 根据项目结构设计PackItem的数据
                // 场景打包
                if (res_name == "Scene")
                {
                    PackItem item = new PackItem();
                    item.file_name = file_name;
                    item.file_ext  = file_ext;
                    item.path = f;
                    item.mod_name = mod_name;
                    item.img_tag = "";
                    item.sub_mod_name = subdir_name;
                    item.bundle_name = string.Format("{0}", res_name.ToLower());
                    item.bundle_ext = "sceneass";
                    alist.Add(item);
                }

                // 音频模块
                if (subdir_name == "audio")
                {
                    PackItem item = new PackItem();
                    item.file_name = file_name;
                    item.file_ext = file_ext;
                    item.path = f;
                    item.mod_name = mod_name;
                    item.img_tag = "";
                    item.sub_mod_name = subdir_name;
                    item.bundle_name = string.Format("{0}/{1}/{2}", res_name, subdir_name, subdir_name);
                    item.bundle_ext = "audioass";
                    alist.Add(item);
                }
                if (subdir_name == "effect")
                {
                    if (paths[3] == "effect_ani") // 序列帧
                    {
                        PackItem item = new PackItem();
                        item.file_name = file_name;
                        item.file_ext = file_ext;
                        item.path = f;
                        item.mod_name = mod_name;
                        if (file_ext == "png" || file_ext == "jpg")
                        {
                            item.img_tag = string.Format("{0}_{1}_{2}_{3}", mod_name, subdir_name, paths[3], paths[4]);
                        }
                        else
                        {
                            item.img_tag = "";
                        }
                        item.sub_mod_name = subdir_name;
                        item.bundle_name = string.Format("{0}/{1}/{2}/{3}", res_name, subdir_name, paths[3], paths[4]);
                        item.bundle_ext = "pngass";
                        alist.Add(item);
                    }
                    if (paths[3] == "effect_par") // 粒子
                    {
                        PackItem item = new PackItem();
                        item.file_name = file_name;
                        item.file_ext = file_ext;
                        item.path = f;
                        item.mod_name = mod_name;
                        item.img_tag = "";
                        item.sub_mod_name = subdir_name;
                        if (file_ext == "prefab") // 预设
                        {
                            item.bundle_name = string.Format("{0}/{1}/{2}", res_name, subdir_name, paths[3]);
                            item.bundle_ext = "prefabass";
                            alist.Add(item);
                        }
                        else if (file_ext == "mat") // 材质
                        {
                            item.bundle_name = string.Format("{0}/{1}/{2}/materials", res_name, subdir_name, paths[3]);
                            item.bundle_ext = "matass";
                            alist.Add(item);
                        }
                        else
                        {
                            Debug.LogError("未知的文件类型!");
                        }
                    }
                }
                // 字体模块
                if (subdir_name == "fonts")
                {
                    if (paths[3] == "font_img" && file_name.StartsWith(paths[4] + "_"))
                    {
                        if (file_ext == "fontsettings" || file_ext == "mat" || file_ext == "png")
                        {
                            PackItem item = new PackItem();
                            item.file_name = file_name;
                            item.file_ext = file_ext;
                            item.path = f;
                            item.mod_name = mod_name;
                            item.img_tag = "";
                            item.sub_mod_name = subdir_name;
                            item.bundle_name = string.Format("{0}/{1}/{2}/{3}", res_name, subdir_name, paths[3], paths[4]);
                            item.bundle_ext = "fontass";
                            alist.Add(item);
                        }
                    }
                }
                // 全局公用模块
                if (subdir_name == "global")
                {
                    if (file_ext == "prefab")
                    {
                        PackItem item = new PackItem();
                        item.file_name = file_name;
                        item.file_ext = file_ext;
                        item.path = f;
                        item.mod_name = mod_name;
                        item.img_tag = "";
                        item.sub_mod_name = subdir_name;
                        item.bundle_name = mod_path.Replace("." + file_ext, "").Replace("\\", "/").Replace(mod_name+"/", "");
                        item.bundle_ext = "prefabass";
                        alist.Add(item);
                    }
                }
                // 语言包
                if (subdir_name == "language")
                {
                    if (file_ext == "png" || file_ext == "jpg")
                    {
                        PackItem item = new PackItem();
                        item.file_name = file_name;
                        item.file_ext = file_ext;
                        item.path = f;
                        item.mod_name = mod_name;
                        item.img_tag = mod_path.Replace("\\" + file_name + "." + file_ext, "").Replace("\\", "_");
                        item.sub_mod_name = subdir_name;
                        item.bundle_name = mod_path.Replace("\\" + file_name + "." + file_ext, "").Replace("\\", "/").Replace(mod_name + "/", "");
                        item.bundle_ext = "pngass";
                        alist.Add(item);
                    }
                }
                // 游戏组件模块
                if (subdir_name == "module")
                {
                    if (file_ext == "prefab")
                    {
                        PackItem item = new PackItem();
                        item.file_name = file_name;
                        item.file_ext = file_ext;
                        item.path = f;
                        item.mod_name = mod_name;
                        item.img_tag = "";
                        item.sub_mod_name = subdir_name;
                        item.bundle_name = string.Format("{0}/{1}/{2}", res_name, subdir_name, paths[3]);
                        item.bundle_ext = "prefabass";
                        alist.Add(item);
                    }
                    else if (file_ext == "png" || file_ext == "jpg")
                    {
                        PackItem item = new PackItem();
                        item.file_name = file_name;
                        item.file_ext = file_ext;
                        item.path = f;
                        item.mod_name = mod_name;
                        item.img_tag = mod_path.Replace("\\" + file_name + "." + file_ext, "").Replace("\\", "_");
                        item.sub_mod_name = subdir_name;
                        item.bundle_name = string.Format("{0}/{1}/{2}", res_name, subdir_name, paths[3]);
                        item.bundle_ext = "pngass";
                        alist.Add(item);
                    }
                    else
                    {
                        // 未知文件
                    }
                }
                // 共享模块
                if (subdir_name == "share")
                {
                    if (file_ext == "png" || file_ext == "jpg")
                    {
                        PackItem item = new PackItem();
                        item.file_name = file_name;
                        item.file_ext = file_ext;
                        item.path = f;
                        item.mod_name = mod_name;
                        item.img_tag = mod_path.Replace("\\" + file_name + "." + file_ext, "").Replace("\\", "_");
                        item.sub_mod_name = subdir_name;
                        item.bundle_name = mod_path.Replace("\\" + file_name + "." + file_ext, "").Replace("\\", "/").Replace(mod_name + "/", "");
                        item.bundle_ext = "pngass";
                        alist.Add(item);
                    }
                }
            }
            return alist;
        }

        /// <summary>
        /// 解析生成所有的资源包结构，将PackItem 转化为 PackAssetBundleData
        /// </summary>
        /// <param name="alist"></param>
        /// <returns></returns>
        public static Dictionary<string, PackAssetBundleData> ParseAllPreBundle(List<PackItem> alist)
        {
            Dictionary<string, PackAssetBundleData> dic = new Dictionary<string, PackAssetBundleData>();
            foreach (PackItem item in alist)
            {
                string bundle_path = (item.bundle_name + "." + item.bundle_ext).ToLower();
                if (!dic.ContainsKey(bundle_path))
                {
                    dic[bundle_path] = new PackAssetBundleData(item.bundle_name, item.bundle_ext);
                }
                dic[bundle_path].AddAsset(item.path.Replace("\\","/").Replace(Application.dataPath, "Assets"));
            }
            return dic;
        }

        /// <summary>
        /// 生成资源包
        /// </summary>
        /// <param name="directory">ab包导出目录</param>
        /// <param name="alist">PackItem需要打包ab包的文件信息列表</param>
        /// <param name="platform">目标平台 1. android 2. iOS</param>
        /// <returns></returns>
        public static bool BuildAssetBundle(string directory, List<PackItem> alist, int platform)
        {
            // 生成AssetBundleBuild[]
            List<AssetBundleBuild> builds = new List<AssetBundleBuild>();
            Dictionary<string, PackAssetBundleData> bdic = ParseAllPreBundle(alist);
            foreach (KeyValuePair<string, PackAssetBundleData> kv in bdic)
            {
                if (kv.Value.Ext != "sceneass")
                {
                    builds.Add(kv.Value.GetAssetBundleBuild());
                }
            }
            // 导出AssetBundle包
            BuildTarget target = BuildTarget.Android;
            if (platform == 1) target = BuildTarget.Android;
            else if (platform == 2) target = BuildTarget.iOS;
            else
            {
                Debug.LogError("未知的打包平台");
                return false;
            }
            string workingPath = Application.dataPath + "/" + directory;
            if (!Directory.Exists(workingPath)) Directory.CreateDirectory(workingPath);
            AssetBundleManifest assetBundleManifest = BuildPipeline.BuildAssetBundles(workingPath, builds.ToArray(), BuildAssetBundleOptions.None, target);
            if (assetBundleManifest == null)
            {
                Debug.LogError(string.Format("Build AssetBundles for '{0}' failure.", target.ToString())); // 打包失败
                return false;
            }
            return true;
        }

        /// <summary>
        /// 生成资源包
        /// </summary>
        /// <param name="directory">ab包导出目录</param>
        /// <param name="alist">PackItem需要打包ab包的文件信息列表</param>
        /// <param name="platform">目标平台 1. android 2. iOS</param>
        /// <returns></returns>
        public static bool BuildSceneAssetBundle(string directory, List<PackItem> alist, int platform)
        {
            // 生成AssetBundleBuild[]
            List<AssetBundleBuild> builds = new List<AssetBundleBuild>();
            Dictionary<string, PackAssetBundleData> bdic = ParseAllPreBundle(alist);
            foreach (KeyValuePair<string, PackAssetBundleData> kv in bdic)
            {
                if (kv.Value.Ext == "sceneass")
                {
                    builds.Add(kv.Value.GetAssetBundleBuild());
                }
            }
            // 导出AssetBundle包
            BuildTarget target = BuildTarget.Android;
            if (platform == 1) target = BuildTarget.Android;
            else if (platform == 2) target = BuildTarget.iOS;
            else
            {
                Debug.LogError("不支持的打包平台");
                return false;
            }
            string workingPath = Application.dataPath + "/" + directory;
            if (!Directory.Exists(workingPath)) Directory.CreateDirectory(workingPath);
            workingPath += "/scene";
            if (!Directory.Exists(workingPath)) Directory.CreateDirectory(workingPath);
            AssetBundleManifest assetBundleManifest = BuildPipeline.BuildAssetBundles(workingPath, builds.ToArray(), BuildAssetBundleOptions.None, target);
            if (assetBundleManifest == null)
            {
                Debug.LogError(string.Format("Build AssetBundles for '{0}' failure.", target.ToString())); // 打包失败
                return false;
            }
            return true;
        }
    }
}