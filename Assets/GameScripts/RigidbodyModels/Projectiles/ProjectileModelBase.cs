using System;
using Components;
using UnityEngine;

namespace RigidbodyModels.Projectiles
{
    public class ProjectileModelBase : RigidbodyModelBase
    {
        [Space]
        [SerializeField] private float lifespanTime = 10;
        [SerializeField] private int damage = 1;
        [SerializeField] private float knockback;

        private Timer _lifespanTimer;

        protected Vector2? TargetPosition;
        protected Vector2? FixedDirection;
        
        private RigidbodyModelBase _parent;

        public int Damage
        {
            get => damage;
            private set => this.damage = value;
        }
        
        public float Knockback
        {
            get => this.knockback;
            private set => this.knockback = value;
        }

        public event EventHandler<CollisionEnterEventArgs> CollisionWithNotStaticRigidbodyModel;
        public event EventHandler<CollisionEnterEventArgs> CollisionWithStaticRigidbodyModel;

        public virtual void Initialize(
            GameObjectLayer layer,
            RigidbodyModelBase parent,
            Vector2 startPosition,
            Vector2? targetPosition = null,
            Vector2? fixedDirection = null,
            float? speedProjectile = null,
            int? damageProjectile = null,
            float? knockbackProjectile= null)
        {
            gameObject.layer = (int) layer;
            gameObject.transform.position = startPosition;
            _parent = parent;
            this.Damage = damageProjectile ?? this.Damage;
            this.Knockback = knockbackProjectile ?? this.Knockback;
            this.maxSpeed = speedProjectile ?? this.maxSpeed;
            this.TargetPosition = targetPosition;
            this.FixedDirection = fixedDirection;

            _lifespanTimer = new Timer(lifespanTime);
            _lifespanTimer.Ended += () => Destroy(gameObject);
            _lifespanTimer.Start();
        }

        protected override bool TryGetDirection(out Vector2 direction)
        {
            direction = Vector2.zero;
            
            if (this.FixedDirection != null)
            {
                direction = (Vector2)this.FixedDirection;
            }

            return direction != Vector2.zero;
        }

        protected override void Start()
        {
            base.Start();

            CollisionWithNotStaticRigidbodyModel += OnHitNotStaticObject;
            CollisionWithStaticRigidbodyModel += OnHitStaticObject;
        }
        
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);

            var collisionModel = other.gameObject.GetComponent<RigidbodyModelBase>();

            if (collisionModel == null || collisionModel == _parent)
            {
                return;
            }

            var eventArgs = new CollisionEnterEventArgs(collisionModel);

            collisionModel.OnProjectileHit(this, eventArgs);

            switch (collisionModel.Layer)
            {
                case GameObjectLayer.Static:
                    CollisionWithStaticRigidbodyModel?.Invoke(this, eventArgs);
                    break;
                case GameObjectLayer.Player:
                case GameObjectLayer.Mob:
                    CollisionWithNotStaticRigidbodyModel?.Invoke(this, eventArgs);
                    break;
                case GameObjectLayer.PlayerObject:
                    break;
                case GameObjectLayer.MobObject:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void UpdateAdditionalData()
        {
            _lifespanTimer.Update();
        }

        protected virtual void OnHitNotStaticObject(object sender, CollisionEnterEventArgs e)
        {
        }

        protected virtual void OnHitStaticObject(object sender, CollisionEnterEventArgs e)
        {
        }
    }
}