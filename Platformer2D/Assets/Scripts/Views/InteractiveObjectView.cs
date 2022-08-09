using System;
using UnityEngine;

namespace PlatformerMVC
{
    public class InteractiveObjectView : LevelObjectView
    {
        public Action<LevelObjectView> OnActivate { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.TryGetComponent<LevelObjectView>(out LevelObjectView contactView);
            OnActivate?.Invoke(contactView);
        }
    }
}