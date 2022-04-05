using System;
using UnityEngine;

namespace RigidbodyModels.Projectiles
{
    public class ProjectileModelBase : DynamicRigidbodyModelBase
    {
        public DynamicRigidbodyModelBase Parent { get; private set; }
        
        public int Damage { get; private set; }

        protected Vector2 TargetPosition;
        
        public event Action CollisionWithDynamicRigidbodyModel;
        public event Action CollisionWithStaticRigidbodyModel;

        public virtual void Initialization(
            DynamicRigidbodyModelBase parent, 
            Vector2 startPosition,
            Vector2 targetPosition, 
            float speed,
            int damage)
        {
            gameObject.transform.position = startPosition;
            Parent = parent;
            Damage = damage;
            maxSpeed = speed;
            TargetPosition = targetPosition;
        }

        protected override bool TryGetDirection(out Vector2 direction)
        {
            Vector2 currentPosition = transform.position;

            direction = TargetPosition - currentPosition;
            
            direction.Normalize();

            return true;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var collisionDynamicModel = other.gameObject.GetComponent<DynamicRigidbodyModelBase>();

            if (collisionDynamicModel != null)
            {
                if (collisionDynamicModel == Parent)
                {
                    return;
                }
                
                CollisionWithDynamicRigidbodyModel?.Invoke();
            }
            
            //TODO: Для статических моделей также
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            throw new NotImplementedException();
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            throw new NotImplementedException();
        }
    }
}