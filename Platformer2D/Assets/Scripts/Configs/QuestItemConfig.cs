using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    [CreateAssetMenu(fileName = "QuestItemCfg", menuName = "Config / QuestSystem / QuestItemCfg", order = 2)]
    
    public class QuestItemConfig : ScriptableObject
    {
        public int questID;
        public List<int> questItemIDCollection;
    }
}