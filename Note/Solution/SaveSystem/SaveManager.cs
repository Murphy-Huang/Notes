using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Solution.SaveSystem
{
    /// <summary>
    /// 跟SaveHandler.cs & ISavable.cs 配合使用
    /// </summary>
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager instance;

        [SerializeField] private string fileName;
        [SerializeField] private string filePath = "idbfs/20231012hhhh"; //used in the webGL save
        [SerializeField] private bool encryptData;
        private GameData gameData;
        private List<ISaveManager> saveManagers;
        private FileDataHandler dataHandler;

        [ContextMenu("Delete save file")]
        public void DeleteSavedData()
        {
            dataHandler = new FileDataHandler(filePath, fileName, encryptData);
            dataHandler.Delete();
        }

        private void Awake()
        {
            if (instance != null)
                Destroy(instance.gameObject);
            else
                instance = this;
        }

        private void Start()
        {
            dataHandler = new FileDataHandler(filePath, fileName, encryptData);
            saveManagers = FindAllSaveManager();
            LoadGame();
        }
        private void OnApplicationQuit()
        {
            SaveGame();
        }

        public void NewGame()
        {
            gameData = new GameData();
        }

        public void LoadGame()
        {
            gameData = dataHandler.Load();
            if (this.gameData == null)
            {
                Debug.Log("No save");
                NewGame();
            }
            foreach (ISaveManager saveManager in saveManagers)
            {
                saveManager.LoadData(gameData);
            }
            // Debug.Log("Loaded currency " + gameData.currency);
        }

        public void SaveGame()
        {
            foreach ( ISaveManager saveManager in saveManagers )
            {
                saveManager.SaveData(ref gameData);
            }
            dataHandler.Save(gameData);
            // Debug.Log("Save currency " + gameData.currency);
        }

        private List<ISaveManager> FindAllSaveManager()
        {
            IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
            return new List<ISaveManager>(saveManagers);
        }

        public bool HasSavedData()
        {
            if (dataHandler.Load() != null)
                return true;
            return false;
        }
    }

}