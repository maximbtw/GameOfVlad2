using System;
using System.Collections.Generic;
using UnityEngine;

namespace RigidbodyModels.Projectiles
{
    public class LaserProjectileModelBase : ProjectileModelBase
    {
        private readonly List<Guid> _touchedModelGuids = new List<Guid>();

        public void Initialize(
            GameObjectLayer layer,
            RigidbodyModelBase parent,
            Vector2 startPosition,
            Vector2? targetPosition = null,
            Vector2? fixedDirection = null,
            int? damageProjectile = null)
        {
            base.Initialize(
                layer,
                parent,
                startPosition,
                targetPosition,
                fixedDirection,
                speedProjectile: 0,
                damageProjectile,
                knockbackProjectile: 0);
        }

        protected override bool TryUpdateKinematicMove(out MoveOptions options)
        {
            options = new MoveOptions();

            return true;
        }

        protected override void Start()
        {
            base.Start();

            SwitchToKinematic();
        }

        protected override void OnHitNotStaticObject(object sender, CollisionEnterEventArgs e)
        {
            if (!_touchedModelGuids.Contains(e.HitObject.Guid))
            {
                base.OnHitNotStaticObject(sender, e);
                
                _touchedModelGuids.Add(e.HitObject.Guid);
            }
        }

        protected override void OnHitStaticObject(object sender, CollisionEnterEventArgs e)
        {
            if (!_touchedModelGuids.Contains(e.HitObject.Guid))
            {
                base.OnHitStaticObject(sender, e);
                
                _touchedModelGuids.Add(e.HitObject.Guid);
            }
        }
    }
}