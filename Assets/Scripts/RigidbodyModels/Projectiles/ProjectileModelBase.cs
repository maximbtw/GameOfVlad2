using UnityEngine;

namespace RigidbodyModels.Projectiles
{
    public class ProjectileModelBase : DynamicRigidbodyModelBase
    {
        public DynamicRigidbodyModelBase Parent { get; private set; }

        protected Vector2 TargetPosition;

        protected override bool TryGetDirection(out Vector2 direction)
        {
            Vector2 currentPosition = transform.position;

            direction = TargetPosition - currentPosition;
            
            direction.Normalize();

            return true;
        }
    }
}