using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Utils;


namespace RigidbodyModels
{
    public class RigidbodyModelBase : MonoBehaviour
    {
        [SerializeField] [Range(0.001f, 10)] protected float acceleration = 0.01f;
        [SerializeField] [Range(0, 10)] protected float maxSpeed = 2.5f;
        [SerializeField] [Range(0.001f, 10)] protected float rotationSpeed = 5f;

        private Rigidbody2D _body;
        private Vector2 _direction;

        public Vector2 Position => _body.position;
        public GameObjectLayer Layer =>  (GameObjectLayer)_body.gameObject.layer;

        protected virtual void Start()
        {
            LoadRigidbody();
            SetLayer();
        }

        private void SetLayer()
        {
            if (TryGetLayer(out GameObjectLayer layer))
            {
                _body.gameObject.layer = (int)layer;
            }
        }

        protected virtual bool TryGetLayer(out GameObjectLayer layer)
        {
            layer = 0;

            return false;
        }

        protected void SwitchToKinematic()
        {
            _body.bodyType = RigidbodyType2D.Kinematic;
        }
        
        protected void SwitchToDynamic()
        {
            _body.bodyType = RigidbodyType2D.Dynamic;
        }
        
        protected void SetGravity(float gravityScale)
        {
            _body.gravityScale = gravityScale;
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
            switch (_body.bodyType)
            {
                case RigidbodyType2D.Kinematic:
                    UpdateKinematicMove();
                    break;
                case RigidbodyType2D.Dynamic:
                    UpdateDynamicMove();
                    break;
                default: throw new Exception("This model body type can't be static");
            }
        }

        protected virtual void UpdateDynamicMove()
        {
            Vector2 force = _direction * (acceleration * Time.deltaTime);

            force.Normalize();

            _body.AddForce(force);

            if (_body.velocity.magnitude >= maxSpeed)
            {
                _body.velocity = _body.velocity.normalized * maxSpeed;
            }
        }
        
        protected virtual void UpdateKinematicMove()
        {
            var x = _direction.x * acceleration;
            var y = _direction.y * acceleration;
            
            Vector2 positionToChange =
                new Vector2(x, y + _body.gravityScale);
            
            // Debug.Log(x);
            // Debug.Log(y);
            // Debug.Log(positionToChange);
            // Debug.Log(_body.position);
            // Debug.Log(_body.position + positionToChange);

            _body.MovePosition(_body.position + positionToChange);

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
                
                angle = Mathf.LerpAngle(_body.rotation, angle, rotationSpeed * Time.deltaTime);
            }

            _body.MoveRotation(angle);
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
    }
}