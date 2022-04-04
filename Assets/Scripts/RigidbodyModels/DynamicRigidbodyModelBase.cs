using UnityEngine;
using Utils;


namespace RigidbodyModels
{
    public class DynamicRigidbodyModelBase : MonoBehaviour
    {
        [SerializeField] [Range(0.001f, 10)] protected float acceleration = 0.01f;
        [SerializeField] [Range(0, 10)] protected float maxSpeed = 3f;
        [SerializeField] [Range(0.001f, 10)] protected float rotationSpeed = 3f;

        private Rigidbody2D _body;
        private Vector2 _direction;
        
        protected virtual void Start()
        {
            LoadRigidbody();
        }

        private void LoadRigidbody()
        {
            _body = gameObject.GetComponent<Rigidbody2D>();

            if (_body == null)
            {
                gameObject.AddComponent(typeof(Rigidbody2D));

                LoadRigidbody();
            }
        }

        private void Update()
        {
            if (TryGetDirection(out Vector2 direction))
            {
                _direction = direction;

                UpdateMove();
            }

            UpdateRotation();
        }

        protected virtual bool TryGetDirection(out Vector2 direction)
        {
            direction = Vector2.zero;

            return false;
        }

        protected virtual bool TryGetRotation(out float angle)
        {
            angle = 0;

            return false;
        }

        private void UpdateMove()
        {
            Vector2 force = _direction * (acceleration * Time.deltaTime);

            force.Normalize();

            _body.AddForce(force);

            if (_body.velocity.magnitude >= maxSpeed)
            {
                _body.velocity = _body.velocity.normalized * maxSpeed;
            }
        }

        private void UpdateRotation()
        {
            if (!TryGetRotation(out float angle))
            {
                angle = Helpers.GetAngleFromDirection(_direction * -1);
            }

            angle = Mathf.LerpAngle(_body.rotation, angle, rotationSpeed * Time.deltaTime);

            _body.MoveRotation(angle);
        }
    }
}