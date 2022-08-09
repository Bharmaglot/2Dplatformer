using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class CoinsController : IDisposable
    {
        private float _animationSpeed = 5;
        private AnimationConfig _config;

        private SpriteAnimatorController _controller;
        private InteractiveObjectView _playerView;
        private List<LevelObjectView> _coinViews;

        public CoinsController(InteractiveObjectView playerV, List<LevelObjectView> coinsViews)
        {
            _playerView = playerV;
            _coinViews = coinsViews;

            _playerView.OnActivate += OnLevelObjectConact;
            _config = Resources.Load<AnimationConfig>("CoinAnimation");
            _controller = new SpriteAnimatorController(_config);
            foreach(LevelObjectView coin in coinsViews)
            {
                _controller.StartAnimation(coin._renderer, AnimState.Run, true, _animationSpeed);
            }           
        }

        private void OnLevelObjectConact(LevelObjectView contactView)
        {
            if(_coinViews.Contains(contactView))
            {
                _controller.StopAnimation(contactView._renderer);
                GameObject.Destroy(contactView.gameObject);
                _coinViews.Remove(contactView);
            }
        }

        public void Update()
        {
            _controller.Update();
        }

        public void Dispose()
        {
            _playerView.OnActivate -= OnLevelObjectConact;
        }
    }
}