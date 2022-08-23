using System;

namespace PlatformerMVC
{
    public class QuestController : IQuest
    {
        private InteractiveObjectView  _player;
        private QuestObjectView _quest;
        private bool _active;
        private IQuestModel _model;

        public event EventHandler<IQuest> QuestCompleted;

        public bool IsCompleted { get; private set; }

        public QuestController(InteractiveObjectView player, QuestObjectView quest, IQuestModel model)
        {
            _player = player;
            _quest = quest;
            _active = false;
            _model = model;
    }

        public void OnContact(QuestObjectView QuestItem)
        {
            if (QuestItem)
            {
                if (_model.TryComplete(QuestItem.gameObject))
                {
                    if (QuestItem == _quest)
                    {
                        Completed();
                    }
                }
            }
        }

        public void Completed()
        {
            if (!_active) return;
            
            _active = false;
            _player.OnCompletedQuest -= OnContact;
            _quest.ProcessComplete();
            QuestCompleted?.Invoke(this, this);

        }

        public void Reset()
        {
            if (_active) return;
            _active = true;
            _player.OnCompletedQuest += OnContact;
            _quest.ProcessActivate();
        }

        public void Dispose()
        {
            _player.OnCompletedQuest -= OnContact;
        }
    }
}