using RigidbodyModels;
using RigidbodyModels.Projectiles;
using UnityEngine;
using Utils;

namespace Player.Weapons.WeaponClassic
{
    public class WeaponClassicProjectile : ProjectileModelBase
    {
        private Vector2 _fixedDirection;

        public override void Initialize(
            RigidbodyModelBase parent,
            Vector2 startPosition,
            Vector2 targetPosition,
            float speed,
            int damage,
            float knockback)
        {
            base.Initialize(parent, startPosition, targetPosition, speed, damage, knockback);

            SetDirection();
        }

        protected override bool TryUpdateDynamicMove(Vector2 direction, out MoveOptions options)
        {
            options = new MoveOptions
            {
                Velocity = direction * maxSpeed
            };

            return true;
        }

        protected override bool TryGetDirection(out Vector2 direction)
        {
            direction = _fixedDirection;

            return true;
        }

        protected override bool TryGetRotation(Vector2 direction, float rotation, out float angle)
        {
            angle = Helpers.GetAngleFromDirection(direction);

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

            _fixedDirection = TargetPosition - currentPosition;

            _fixedDirection.Normalize();
        }
    }
}