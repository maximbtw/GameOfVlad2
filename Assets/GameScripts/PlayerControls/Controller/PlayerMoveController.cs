using UnityEngine;

namespace PlayerControls.Controller
{
    public class PlayerMoveController : MonoBehaviour
    {
        private const float DirectionSpeed = 1;

        private Vector2 _direction;
        private bool _needToUpdate;

        private void Update()
        {
            _needToUpdate = false;

            if (Input.GetKey(KeyCode.W))
            {
                _direction += new Vector2(0, DirectionSpeed);

                _needToUpdate = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                _direction += new Vector2(0, -DirectionSpeed);

                _needToUpdate = true;
            }

            if (Input.GetKey(KeyCode.A))
            {
                _direction += new Vector2(-DirectionSpeed, 0);

                _needToUpdate = true;
            }

            if (Input.GetKey(KeyCode.D))
            {
                _direction += new Vector2(DirectionSpeed, 0);

                _needToUpdate = true;
            }

            _direction.Normalize();
        }

        public bool TryGetDirection(out Vector2 direction)
        {
            direction = _direction;

            return _needToUpdate;
        }
    }
}