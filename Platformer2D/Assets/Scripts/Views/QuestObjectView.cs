using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class QuestObjectView : LevelObjectView
    {
        public int _id;
        public Color _completedColor;
        public Color _defaultColor;

        private void Awake()
        {
            _defaultColor = _renderer.color;
        }

        public void ProcessComplete()
        {
            _renderer.color = _completedColor;
        }

        public void ProcessActivate()
        {
            _renderer.color = _defaultColor;
        }
    }
}
