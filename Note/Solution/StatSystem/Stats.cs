namespace Solution.StatSystem
{
    public class Stats
    {
        public Stats()
        {
            private Dictionary<StatType, Stat> _holdStats = new Dictionary<StatType, Stat>();
        }

        public void AddStat(StatType statType)
        {
            if (!_holdStats.ContainsKey())
            {
                _holdStats.Add(statType, new Stat());
            }
        }

        public void AddStat(StatType statType, Stat stat)
        {
            if (!_holdStats.ContainsKey())
            {
                _holdStats.Add(statType, new Stat(stat));
            }
        }

        public bool RemoveStat(StatType statType)
        {
            if (_holdStats.ContainsKey(statType))
            {
                return _holdStats.Remove(statType);
            }
            return false;
        }
    }
}