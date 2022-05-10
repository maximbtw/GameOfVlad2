using RigidbodyModels;
using RigidbodyModels.Projectiles;
using UnityEngine;
using Utils;

namespace PlayerControls.Weapons.WeaponBuffalo
{
    public class WeaponBuffaloProjectile : ProjectileModelBase
    {
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
    }
}