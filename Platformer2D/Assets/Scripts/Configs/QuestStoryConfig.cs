using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public enum QuestStoryType
    {
        Common,
        Resettable
    }

    [CreateAssetMenu(fileName = "QuestStoryCfg", menuName = "Config / QuestSystem / QuestStoryCfg", order = 3)]
    
    public class QuestStoryConfig : ScriptableObject
    {
        public QuestConfig[] quests;
        public QuestStoryType type;
    }
}