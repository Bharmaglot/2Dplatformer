using System;
using UnityEngine;

namespace PlatformerMVC
{
    public class InteractiveObjectView : LevelObjectView
    {
        public Action<LevelObjectView> OnActivate { get; set; }

        public Action<QuestObjectView> OnCompletedQuest { get; set; }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent<LevelObjectView>(out LevelObjectView contactView))
            {
                OnActivate?.Invoke(contactView);
            }

            if(collision.TryGetComponent<QuestObjectView>(out QuestObjectView questItemView))
            {
                OnCompletedQuest?.Invoke(questItemView);
            }
         }
    }
}