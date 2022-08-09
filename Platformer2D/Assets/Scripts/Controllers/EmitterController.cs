using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class EmitterController
    {
        private List<BulletController> _bullets = new List<BulletController>();
        private Transform _transform;

        private int _index;
        private float _timeTillNextBull;
        private float _delay = 1;
        private float _startSpeed = 15;


        public EmitterController(List<LevelObjectView> bulletViews, Transform emitterT)
        {
            _transform = emitterT;
            foreach(LevelObjectView bulletView in bulletViews)
            {
                _bullets.Add(new BulletController(bulletView));
            }
        }
        public void Update()
        {
            if(_timeTillNextBull > 0)
            {
                _bullets[_index].Active(false);
                _timeTillNextBull -= Time.deltaTime;
            }
            else
            {
                _timeTillNextBull = _delay;
                _bullets[_index].Trow(_transform.position, -_transform.up * _startSpeed);
                _index++;
                if(_index >= _bullets.Count)
                {
                    _index = 0;
                }
            }
        }
    }
}