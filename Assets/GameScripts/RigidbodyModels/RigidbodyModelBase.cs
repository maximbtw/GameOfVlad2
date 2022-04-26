using RigidbodyModels.Projectiles;
using UnityEngine;

namespace RigidbodyModels
{
    public partial class RigidbodyModelBase : MonoBehaviour
    {
        [SerializeField] [Range(0.001f, 10)] protected float acceleration = 0.01f;
        [SerializeField] [Range(0, 10)] protected float maxSpeed = 2.5f;
        [SerializeField] [Range(0.001f, 10)] protected float rotationSpeed = 5f;

        private Rigidbody2D _body;
        private Collider2D _collider;
        private Vector2 _direction;
        private SpriteRenderer _spriteRenderer;

        public Vector2 Position => _body.position;
        public GameObjectLayer Layer => (GameObjectLayer) _body.gameObject.layer;
        public Vector2 Size => _spriteRenderer.size;
        public Vector2 Direction => _direction;

        protected virtual void Start()
        {
            LoadRigidbody();
            LoadCollider();
            LoadSpriteRenderer();
            SetLayer();
        }

        private void Update()
        {
            if (TryGetDirection(out Vector2 direction))
            {
                _direction = direction;

                UpdateMove();
            }

            UpdateRotation();
            UpdateAdditionalData();
        }

        protected virtual void OnCollisionEnter2D(Collision2D col)
        {
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

        public virtual void OnProjectileHit(ProjectileModelBase sender, CollisionEnterEventArgs e)
        {
        }

        protected virtual void UpdateAdditionalData()
        {
        }

        protected virtual bool TryGetDirection(out Vector2 direction)
        {
            direction = Vector2.zero;

            return false;
        }

        protected virtual bool TryGetRotation(Vector2 direction, float rotation, out float angle)
        {
            angle = 0;

            return false;
        }

        private void UpdateRotation()
        {
            if (TryGetRotation(_direction, _body.rotation, out float angle))
            {
                _body.MoveRotation(angle);
            }
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

        private void LoadSpriteRenderer()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            if (_body == null)
            {
                _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            }
        }

        private void SetLayer()
        {
            if (TryGetLayer(out GameObjectLayer layer))
            {
                _body.gameObject.layer = (int)layer;
            }
        }
    }
}