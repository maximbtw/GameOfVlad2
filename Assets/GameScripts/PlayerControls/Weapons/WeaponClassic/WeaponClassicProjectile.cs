﻿using RigidbodyModels;
using RigidbodyModels.Projectiles;
using UnityEngine;
using Utils;

namespace PlayerControls.Weapons.WeaponClassic
{
    public class WeaponClassicProjectile : ProjectileModelBase
    {
        private Vector2 _fixedDirection;

        public override void Initialize(
            GameObjectLayer layer,
            RigidbodyModelBase parent,
            Vector2 startPosition,
            Vector2 targetPosition,
            float? speedProjectile = null,
            int? damageProjectile = null,
            float? knockbackProjectile = null)
        {
            base.Initialize(
                layer,
                parent, 
                startPosition, 
                targetPosition,
                speedProjectile, 
                damageProjectile,
                knockbackProjectile);

            SetDirection();
        }

        protected override bool TryUpdateDynamicMove(Vector2 direction, Vector2 velocity, out MoveOptions options)
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

            _fixedDirection = TargetPosition - currentPosition;

            _fixedDirection.Normalize();
        }
    }
}