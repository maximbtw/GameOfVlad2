using System;
using RigidbodyModels.Projectiles;
using UnityEngine;
using Utils;


namespace RigidbodyModels
{
    public class RigidbodyModelBase : MonoBehaviour
    {
        [SerializeField] [Range(0.001f, 10)] protected float acceleration = 0.01f;
        [SerializeField] [Range(0, 10)] protected float maxSpeed = 2.5f;
        [SerializeField] [Range(0.001f, 10)] protected float rotationSpeed = 5f;

        private Rigidbody2D _body;
        private Collider2D _collider;
        private Vector2 _direction;

        public Vector2 Position => _body.position;
        public GameObjectLayer Layer =>  (GameObjectLayer)_body.gameObject.layer;

        protected virtual void Start()
        {
            LoadRigidbody();
            LoadCollider();
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

        protected virtual void OnCollisionEnter2D(Collision2D col)
        {
            
        }

        public virtual void OnProjectileHit(ProjectileModelBase sender, CollisionEnterEventArgs e)
        {
            
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
            Vector2 positionToChange =
                new Vector2(_direction.x * acceleration, _direction.y * acceleration + _body.gravityScale);

            _body.MovePosition(_body.position + positionToChange);

            if (_body.velocity.magnitude >= maxSpeed)
            {
                _body.velocity = _body.velocity.normalized * maxSpeed;
            }
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
                _body = gameObject.AddComponent<Rigidbody2D>();
            }
        }
        
        private void LoadCollider()
        {
            _collider = gameObject.GetComponent<Collider2D>();

            if (_body == null)
            {
                _collider = gameObject.AddComponent<PolygonCollider2D>();
            }
        }
    }
}