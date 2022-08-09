using UnityEngine;

namespace PlatformerMVC
{
    public class PlayerController
    {
        private AnimationConfig _config;
        private SpriteAnimatorController _playerAnimator;
        private ContactPooler _contactPooler;
        private LevelObjectView _playerView;

        private Transform _playerT;
        private Rigidbody2D _rb;

        private float _xAxisInput;
        private bool _isJump;

        private float _walkSpeed = 200f;
        private float _animationSpeed = 10f;
        private float _movingTreshold = 0.1f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _RightScale = new Vector3(1, 1, 1);

        private bool _isMoving;

        private float _jumpForce = 7f;
        private float _jumpTreshhold = 1f;
        private float _yVelocity; 
        private float _xVelocity;

        public PlayerController(LevelObjectView player)
        {
            _config = Resources.Load<AnimationConfig>("SpriteAnimationCfg");
            _playerAnimator = new SpriteAnimatorController(_config);
            _playerAnimator.StartAnimation(player._renderer, AnimState.Run, true, _animationSpeed);
            _contactPooler = new ContactPooler(player._collider);

            _playerView = player;
            _playerT = player._transform;
            _rb = player._rb;
        }

        private void MoveTowards()
        {
            _xVelocity = Time.fixedDeltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1);
            _rb.velocity = new Vector2(_xVelocity, _rb.velocity.y);
            _playerT.localScale = _xAxisInput < 0 ? _leftScale : _RightScale;
        }

        public void Update()
        {
          _playerAnimator.Update();
            _contactPooler.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;
            _yVelocity = _rb.velocity.y;
            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshold;

            if(_isMoving)
            {
                MoveTowards();
            }
            else
            {
                _xVelocity = 0;
            }

            if(_contactPooler.IsGrounded)
            {
                _playerAnimator.StartAnimation(_playerView._renderer, _isMoving ? AnimState.Run : AnimState.Idle, true, _animationSpeed);

                if(_isJump&&_yVelocity<=_jumpTreshhold)
                {
                    _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                if(Mathf.Abs(_yVelocity) > _jumpTreshhold)
                {
                    _playerAnimator.StartAnimation(_playerView._renderer, AnimState.Jump, true, _animationSpeed);
                }
            }

        }
    }
}