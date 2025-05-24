using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.StatSystem
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private int _baseValue;
        [SerializeField] private int _levelModifier;
        [SerializeField] public int BaseValue
        {
            get { return _baseValue * _levelModifier; }
        }
        [SerializeField] private List<int> _modifiers;

        public Stat(int basevalue)
        {
            _baseValue = basevalue;
        }

        public Stat(int basevalue, int levelModifier)
        {
            _baseValue = basevalue;
            _levelModifier = levelModifier;
        }
        
        public int GetValue()
        {
            int finalValue = BaseValue;
            foreach (int modifier in _modifiers)
            {
                finalValue += modifier;
            }
            return finalValue;
        }
        public void SetBaseValue(int value) => _baseValue = value;
        public void SetLevelModifier(float levelModifier) => _levelModifier = levelModifier;
        public void AddNumericModifier(float modifier) => _modifiers.Add(modifier);
        public void RemoveNumericModifier(float modifier) => _modifiers.Remove(modifier);
        public void AddMultiplyModifier(float modifier) => _modifiers.Add(BaseValue * modifier);
        public void RemoveMultiplyModifier(float modifier) => _modifiers.Remove(BaseValue * modifier);
    }
}