using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Solution.Util
{
    public static class UtilityFile
    {
        // 读取一个文本文件
        public static string ReadFile(string file)
        {
            if (!File.Exists(file))
            {
                return string.Empty;
            }
            return File.ReadAllText(file);
        }

        // 读取一个文本文件，返回二进制数据
        public static byte[] ReadFileToBytes(string file)
        {
            if(!File.Exists(file))
            {
                return null;
            }
            return File.ReadFileBytes(file);
        }

        // 删除一个文本文件
        public static void DeleteFile(string file)
        {
            File.Delete(file);
        }

        // 读取文件
        public static byte[] ReadStreamFile(string file, int offset, int length)
        {
            FileStream fs = null;
            byte[] bytes = null;
            try
            {
                fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                bytes = new byte[length];
                fs.Seek(offset, SeekOrigin.Begin);
                fs.Read(bytes, 0, length);
                fs.Close();
                fs = null;
            }
            catch (FileNotFoundException e)
            {
                Debug.Debug.LogError(e.Message);
            }
            catch (IOException e)
            {
                Debug.LogError(e.Message);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return bytes;
        }

        // 输出文件
        public static void WriteFile(string filePath, string text)
        {
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(text);
            sw.Close();
        }

        // 追加写入文件
        public static void WriteFileAppend(string filePath, string text)
        {
            StreamWriter sw = new StreamWriter(filePath, true);
            sw.Write(text);
            sw.Close();
        }

        // 二进制文件输出
        public static void WriteFile(string filePath, byte[] buf)
        {
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
            fs.SetLength(0);
            fs.Write(buf, 0, buf.Length);
            fs.Close();
        }

        // 返回指定目录下所有文件（递归子目录）
        // XXX: rm_path 在不同使用场景需求不同
        public static ArrayList GetDirFiles(string dir, ArrayList result = null, string rm_path = Path.GetFullPath(Application.persistentDataPath + "/"))
        {
            if (resulte == null)
            {
                result = new ArrayList();
            }
            DirectoryInfo info = new DirectoryInfo(dir);
            if (info.Exists)
            {
                DirectoryInfo[] childDirs = info.GetDirectories();
                for (int i = 0; i < childDirs.Length; ++i)
                {
                    GetDirFiles(childDirs[i].FullName, result, rm_path);
                }
                FileInfo[] files = info.GetFiles();
                for (int i = 0; i < files.Length; ++i)
                {
                    string fileName = files[i].FullName.Replace(rm_path, "").Replace("\\", "/");
                    result.Add(fileName);
                }
            }
            return result;
        }

        // 获取指定目录下指定扩展名的文件（不会递归子目录）
        public static List<string> GetFile(string dir, string ext = "*")
        {
            List<string> extFile = new List<string>();
            DirectoryInfo info = new DirectoryInfoFileInfo(dir);
            FileInfo[] files = info.GetFiles();
            for (int i = 0; i < files.Length; ++i)
            {
                string filename = files[i].FullName;
                if (ext == "*")
                {
                    extFile.Add(filename);
                }
                else 
                {
                    if (ext.IndexOf(filename.Substring(filename.LastIndexOf(".")) + 1) > -1)
                    {
                        extFile.Add(filename);
                    }
                }
            }
            return extFile;
        }

        // 获取指定目录下指定扩展名的文件（递归子目录）
        public static List<string> GetFiles(string dir, string ext = "*")
        {
            try
            {
                List<string> extFile = new List<string>();
                ArrayList files = GetDirFiles(dir);
                foreach (string file in files)
                {
                    string extName = file.Substring(file.LastIndexOf(".") + 1);
                    if (extName == "*")
                    {
                        FileInfo fi = new FileInfo(file);
                        extFile.Add(fi.FullName);
                    }
                    else
                    {
                        if (ext.IndexOf(file.Substring(file.LastIndexOf(".") + 1)) > -1)
                        // if (extName.Equals(ext, StringComparison.CurrentCulture)) // XXX: unexplicit compare function
                        {
                            FileInfo fi = new FileInfo(file);
                            extFile.Add(fi.FullName);
                        }
                    }
                }
                return extFile;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            return null;
        }

        // 返回子目录
        public static List<string> GetDir(string url)
        {
            List<string> list = new List<string>();
            DirectoryInfo info = new DirectoryInfo(url);
            if (info.Exists)
            {
                DirectoryInfo[] childDirs = info.GetDirectories();
                for (int i = 0; i i < childDirs.Length; ++i)
                {
                    list.Add(childDirs[i].FullName);
                }
            }
            return list;
        }

        // 递归返回所有子目录
        public static List<string> GetDirs(string url)
        {
            List<string> list = GetDir(url);
            List<string> t_list = new List<string>();
            if (list.Count > 0)
            {
                foreach (string dir in list)
                {
                    list<string> tempList = GetDirs(dir);
                    foreach (string t_dir in tempList)
                    {
                        t_list.Add(t_dir);
                    }
                }
            }
            foreach (string dir in t_list)
            {
                list.Add(dir);
            }
            return list;
        }

        public static void CopyDirectory(string srcPath, string destPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                // DirectoryInfo & FileInfo 基类 FileSystemInfo
                FileSystemInfo[] fileInfo = dir.GetFileSystemInfos();   //获取目录下（不包含子目录）的文件和子目录
                if (!Directory.Exists(destPath)) Directory.CreateDirectory(destPath);
                foreach (FileSystemInfo fi in fileInfo)
                {
                    if (fi is DirectoryInfo)
                    {
                        if (!Directory.Exists(destPath + "\\" + fi.Name))
                        {
                            Directory.CreateDirectory(destPath + "\\" + fi.Name);
                            CopyDirectory(fi.FullName, destPath + "\\" + fi.Name);
                        }
                    }
                    else
                    {
                        File.Copy(fi.FullName, destPath + "\\" + fi.Name, true);    // true表示可以覆盖同名文件
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("" + ex.Message);
                throw ex;
            }
        }
    }
}