using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.AssetManage
{
    public class AssetPath
    {
        // 存储相对路径保持小写

        public static string ModDirectory = "Game";
        public static string ABDirectory = "../GameAB"
        public static string StreamingAssetPath = Application.streamingAssetPath;
        public static string CachePath 
        {
            get
            {
            #if UNITY_EDITOR
                return Application.dataPath;
            #else
                return Application.persistentDataPath;
            #endif
            }
        }
    }
}