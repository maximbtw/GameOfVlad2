using System;
using Components;
using UnityEngine;

namespace RigidbodyModels.Projectiles
{
    public class ProjectileModelBase : RigidbodyModelBase
    {
        [SerializeField] private float lifespanTime = 10;

        private Timer _lifespanTimer;

        protected Vector2 TargetPosition;
        public RigidbodyModelBase Parent { get; private set; }

        public int Damage { get; private set; }

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

            Debug.Log("Enter");

            if (collisionModel == null || collisionModel == Parent) return;

            var eventArgs = new CollisionEnterEventArgs(collisionModel);

            collisionModel.OnProjectileHit(this, eventArgs);

            Debug.Log(collisionModel.Layer);

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

        public event EventHandler<CollisionEnterEventArgs> CollisionWithNotStaticRigidbodyModel;
        public event EventHandler<CollisionEnterEventArgs> CollisionWithStaticRigidbodyModel;

        public virtual void Initialize(
            RigidbodyModelBase parent,
            Vector2 startPosition,
            Vector2 targetPosition,
            float speed,
            int damage)
        {
            gameObject.layer = (int) GameObjectLayer.PlayerObject;
            // ReSharper disable once Unity.InefficientPropertyAccess
            gameObject.transform.position = startPosition;
            Parent = parent;
            Damage = damage;
            maxSpeed = speed;
            TargetPosition = targetPosition;

            _lifespanTimer = new Timer(lifespanTime);
            _lifespanTimer.Ended += () => Destroy(gameObject);
            _lifespanTimer.Start();
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