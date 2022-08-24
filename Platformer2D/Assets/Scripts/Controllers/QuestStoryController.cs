using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class QuestStoryController : IQuestStory
    {
        private List<IQuest> _questCollection = new List<IQuest>();

        public bool IsDone => _questCollection.All(value => value.IsCompleted);

        public QuestStoryController(List<IQuest> questCollection)
        {
            _questCollection = questCollection;
            Subscribe();
            Reset(0);
        }
        
        private void Subscribe()
        {
            foreach(IQuest quest in _questCollection)
            {
                quest.QuestCompleted += OnQuestCompleted;
            }
        }

        private void Unsubscribe()
        {
            foreach (IQuest quest in _questCollection)
            {
                quest.QuestCompleted -= OnQuestCompleted;
            }
        }

        public void Reset(int index)
        {
            if(index < 0||index > _questCollection.Count)
            {
                return;
            }

            IQuest quest = _questCollection[index];

            if(quest.IsCompleted)
            {
                OnQuestCompleted(this, quest);
            }
            else
            {
                quest.Reset();
            }

        }

        private void OnQuestCompleted(object sender, IQuest quest)
        {
            int index = _questCollection.IndexOf(quest);
            if(IsDone)
            {
                Debug.Log("Story is done");
            }
            else
            {
                Reset(++index);
            }
        }

        public void Dispose()
        {
            Unsubscribe();
            foreach (var item in _questCollection)
            {
                item.Dispose();
            }
        }
    }
}
