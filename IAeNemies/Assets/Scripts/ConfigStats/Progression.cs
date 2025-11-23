using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ConfigStats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Scriptable Objects/Progression")]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        private Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;
        public float GetStat(Stat stat,CharacterClass characterClass, int level)
        {
            BuildLookup();
            
            float[] levels = lookupTable[characterClass][stat];
            
            if (levels.Length < level)
            {
                return 0;
            }
            return levels[level - 1];
        }

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookup();
            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }

        private void BuildLookup()
        {
            if (lookupTable != null) return;
            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                var statlookupTable = new Dictionary<Stat, float[]>();

                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    statlookupTable[progressionStat.stat] = progressionStat.levels;
                }
                lookupTable[progressionClass.characterClass] = statlookupTable;
            }
        }
        

        [System.Serializable]
        class ProgressionCharacterClass
        { 
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
           
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    
    }
}
