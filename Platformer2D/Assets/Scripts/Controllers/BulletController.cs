using UnityEngine;

namespace PlatformerMVC
{
    public class BulletController
    {
        private LevelObjectView _view;
        private Vector3 _velocity;

        public BulletController(LevelObjectView view)
        {
            _view = view;
            Active(false);
        }

        public void Active(bool val)
        {
            _view.gameObject.SetActive(val);
        }

        private void SetVelociy(Vector3 velocity)
        {
            _velocity = velocity;

            float angle = Vector3.Angle(Vector3.left, _velocity);
            Vector3 axis = Vector3.Cross(Vector3.left, _velocity);
            _view.transform.rotation = Quaternion.AngleAxis(angle, axis);
        }

        public void Trow(Vector3 position, Vector3 velocity)
        {
            _view._transform.position = position;
            SetVelociy(velocity);
            _view._rb.velocity = Vector2.zero;
            _view._rb.angularVelocity = 0;
            Active(true);

            _view._rb.AddForce(velocity, ForceMode2D.Impulse);

        }

    }
}