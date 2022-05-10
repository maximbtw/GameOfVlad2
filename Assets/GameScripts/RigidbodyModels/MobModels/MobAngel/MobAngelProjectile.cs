using RigidbodyModels.Projectiles;
using UnityEngine;
using Utils;

namespace RigidbodyModels.MobModels.MobAngel
{
    public class MobAngelProjectile : ProjectileModelBase
    {
        private Vector2 _fixedDirection;
        
        public override void Initialize(
            GameObjectLayer layer,
            RigidbodyModelBase parent, 
            Vector2 startPosition, 
            Vector2? targetPosition = null, 
            Vector2? fixedDirection = null,
            float? speedProjectile = null, 
            int? damageProjectile = null,
            float? knockbackProjectile= null)
        {
            base.Initialize(
                layer,
                parent, 
                startPosition, 
                targetPosition, 
                fixedDirection,
                speedProjectile, 
                damageProjectile,
                knockbackProjectile);
            
            gameObject.layer = (int) GameObjectLayer.MobObject;
            
            SetDirection();
        }
        
        protected override bool TryGetDirection(out Vector2 direction)
        {
            direction = _fixedDirection;

            return true;
        }
        
        protected override bool TryUpdateDynamicMove(Vector2 direction, Vector2 velocity, out MoveOptions options)
        {
            options = new MoveOptions
            {
                Velocity = direction * maxSpeed
            };

            return true;
        }
        
        protected override bool TryGetAngleRotation(float rotation, out float angle)
        {
            angle = Helpers.GetAngleFromDirection(this.Direction);

            angle -= 90;

            return true;
        }

        protected override void OnHitNotStaticObject(object sender, CollisionEnterEventArgs e)
        {
            Destroy(gameObject);
        }

        protected override void OnHitStaticObject(object sender, CollisionEnterEventArgs e)
        {
            Destroy(gameObject);
        }
        
        private void SetDirection()
        {
            Vector2 currentPosition = transform.position;

            // ReSharper disable once PossibleInvalidOperationException
            _fixedDirection = (Vector2)TargetPosition - currentPosition;

            _fixedDirection.Normalize();
        }
    }
}