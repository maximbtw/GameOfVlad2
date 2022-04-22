using System;
using Components;
using UnityEngine;

namespace RigidbodyModels.Projectiles
{
    public class ProjectileModelBase : RigidbodyModelBase
    {
        [SerializeField] private float lifespanTime = 10;

        private Timer _lifespanTimer;
        public RigidbodyModelBase Parent { get; private set; }
        
        public int Damage { get; private set; }

        protected Vector2 TargetPosition;
        
        public event EventHandler<CollisionEnterEventArgs> CollisionWithDynamicRigidbodyModel;
        public event EventHandler<CollisionEnterEventArgs> CollisionWithStaticRigidbodyModel;

        protected override void Start()
        {
            base.Start();

            CollisionWithDynamicRigidbodyModel += OnHitDynamicObject;
        }

        public virtual void Initialization(
            RigidbodyModelBase parent, 
            Vector2 startPosition,
            Vector2 targetPosition, 
            float speed,
            int damage)
        {
            gameObject.layer = (int)GameObjectLayer.PlayerObject;
            // ReSharper disable once Unity.InefficientPropertyAccess
            gameObject.transform.position = startPosition;
            Parent = parent;
            Damage = damage;
            maxSpeed = speed;
            TargetPosition = targetPosition;

            _lifespanTimer = new Timer(lifespanTime);
            _lifespanTimer.Ended += () => Destroy(this.gameObject);
            _lifespanTimer.Start();
        }

        protected override void UpdateKinematicMove()
        {
            base.UpdateKinematicMove();
            
            _lifespanTimer.Update();
        }

        protected virtual void OnHitDynamicObject(object sender, CollisionEnterEventArgs e)
        {
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            var collisionDynamicModel = other.gameObject.GetComponent<RigidbodyModelBase>();
            
            if (collisionDynamicModel != null)
            {
                if (collisionDynamicModel == Parent)
                {
                    return;
                }

                CollisionWithDynamicRigidbodyModel?.Invoke(this, new CollisionEnterEventArgs(collisionDynamicModel));
            }
            
            //TODO: Для статических моделей также
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            //throw new NotImplementedException();
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            //throw new NotImplementedException();
        }
    }
}