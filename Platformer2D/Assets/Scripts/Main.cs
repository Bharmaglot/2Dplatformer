using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectView _playerView;
        [SerializeField] private CannonView _cannonView;
        [SerializeField] private List<LevelObjectView> _coinViews;
        [SerializeField] private GeneratorLevelView _generatorLevelView;


        private PlayerController _playerController;
        private CameraController _cameraController;
        private CannonController _cannonController;
        private EmitterController _emitterController;
        private CoinsController _coinsController;
        private GeneratorController _generatorController;

        private void Awake()
        {
            _playerController = new PlayerController(_playerView);
            _cameraController = new CameraController(_playerView, Camera.main.transform);
            _cannonController = new CannonController(_cannonView._muzzleTransform, _playerView._transform);
            _emitterController = new EmitterController(_cannonView._bullets, _cannonView._emitterTransform);
            _coinsController = new CoinsController(_playerView, _coinViews);
            _generatorController = new GeneratorController(_generatorLevelView);
            _generatorController.Start();
        }

        void Update()
        {
            _playerController.Update();
            _cameraController.Update();
            _cannonController.Update();
            _emitterController.Update();
            _coinsController.Update();
        }
    }
}