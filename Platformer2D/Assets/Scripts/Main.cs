using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        private SpriteAnimatorController _playerAnimator;

        private AnimationConfig _config;

        [SerializeField] private LevelObjectView _playerView; 

        private void Awake()
        {
            _config = Resources.Load<AnimationConfig>("SpriteAnimationCfg");
            _playerAnimator = new SpriteAnimatorController(_config);
            _playerAnimator.StartAnimation(_playerView._renderer, AnimState.Run, true, 10);
        }

        void Update()
        {
            _playerAnimator.Update();
        }
    }
}