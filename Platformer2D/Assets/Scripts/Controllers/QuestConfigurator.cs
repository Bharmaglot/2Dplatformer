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

        private List<IQuestStory> _questStories;

        private InteractiveObjectView _player;

        private Dictionary<QuestType, Func<IQuestModel>> _questFactory = new Dictionary<QuestType, Func<IQuestModel>>(1);
        private Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactory = new Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>>(2);


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

            _questFactory.Add(QuestType.Coins, () => new QuestCoinModel());
            _questStoryFactory.Add(QuestStoryType.Common, questCollection => new QuestStoryController(questCollection));

            _questStories = new List<IQuestStory>();

            foreach (QuestStoryConfig cfg in _storyConfig)
            {
                _questStories.Add(CreateQuestStory(cfg));
            }
        }
        private IQuest CreateQuest(QuestConfig cfg)
        {
            int questID = cfg.id;

            QuestObjectView qView = _questObjects.FirstOrDefault(value => value._id == cfg.id);

            if (qView == null)
            {
                Debug.Log("NoView");
                return null;
            }

            if (_questFactory.TryGetValue(cfg.type, out var factory))
            {
                IQuestModel qModel = factory.Invoke();
                return new QuestController(_player, qView, qModel);
            }

            Debug.Log("NoModel");
            return null;
        }

        private IQuestStory CreateQuestStory(QuestStoryConfig cfg)
        {
            List<IQuest> quests = new List<IQuest>();

            foreach (QuestConfig Qcfg in cfg.quests)
            {
                IQuest quest = CreateQuest(Qcfg);

                if (quest == null) continue;

                quests.Add(quest);
                Debug.Log("Add Quest");
            }

            return _questStoryFactory[cfg.type].Invoke(quests);
        }
    }
}
