using RigidbodyModels.Projectiles;
using UnityEngine;
using Utils;

namespace PlayerControls.Weapons.WeaponClassic
{
    public class WeaponClassicProjectile : ProjectileModelBase
    {
        protected override bool TryUpdateDynamicMove(Vector2 velocity, out MoveOptions options)
        {
            options = new MoveOptions
            {
                Velocity = this.Direction * maxSpeed
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
            base.OnHitNotStaticObject(sender, e);
            
            Destroy(gameObject);
        }

        protected override void OnHitStaticObject(object sender, CollisionEnterEventArgs e)
        {
            base.OnHitStaticObject(sender, e);
            
            Destroy(gameObject);
        }
    }
}