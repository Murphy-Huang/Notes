namespace Solution.SaveSystem
{
    public interface ISavable
    {
        void LoadData(GameData _data);
        void SaveData(ref GameData _data);
    }
}