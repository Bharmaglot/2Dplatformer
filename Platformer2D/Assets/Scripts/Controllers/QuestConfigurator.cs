using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class QuestConfigurator 
    {
        private QuestObjectView _singleQuest;
        private QuestController _singleQuestController;
        private QuestStoryConfig[] _storyConfig;
        private QuestObjectView[] _questObjects;
        private QuestCoinModel _model;

        private InteractiveObjectView _player;

        public QuestConfigurator(QuestView view, InteractiveObjectView player)
        {
            _singleQuest = view._singleQuest;
            _storyConfig = view._storyConfig;
            _questObjects = view._questObject;
            _player = player;
            _model = new QuestCoinModel();
        }

        public void Init()
        {
            _singleQuestController = new QuestController(_player, _singleQuest, _model);
            _singleQuestController.Reset();
        }
    }
}