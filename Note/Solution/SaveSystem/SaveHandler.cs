
namespace Solution.SaveSystem
{
    public class SaveHandler
    {
        private string dataDirPath = "";
        private string dataFileName = "";

        private bool encryptData = false;
        private string codeWord = "26082024";

        public FileDataHandler(string _dataDirPath, string _dataFileName, bool _encryptData)
        {
            dataDirPath = _dataDirPath;
            dataFileName = _dataFileName;
            this.encryptData = _encryptData;
        }
        public void Save(GameData _data)
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
        
            tryp
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                string dataToStore = JsonUtility.ToJson(_data, true);
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        if(encryptData) 
                            dataToStore = EncryptDecrypt(dataToStore);
                        writer.Write(dataToStore);
                    }
                }
            }   
            catch(Exception e)
            {
                Debug.LogError("Errror on trying to save data to file: " + fullPath + "\n" + e.ToString());
            }
        }

        public GameData Load()
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            GameData loadData = null;
            if (File.Exists(fullPath))
            {
                try
                {
                    string dataToLoad = "";
                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                            if (encryptData)
                                dataToLoad = EncryptDecrypt(dataToLoad);
                        }
                    }
                    loadData = JsonUtility.FromJson<GameData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError("Errror on trying to load data from file: " + fullPath + "\n" + e.ToString());
                }
            }
            return loadData;
        }

        public void Delete()
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            if (File.Exists(fullPath)) 
                File.Delete(fullPath);
        }

        private string EncryptDecrypt(string _data)
        {
            string modifiedData = "";
            for (int i = 0; i < _data.Length; i++)
            {
                modifiedData += (char)(_data[i] ^ codeWord[i % codeWord.Length]);
            }
            return modifiedData;
        }
    }
}