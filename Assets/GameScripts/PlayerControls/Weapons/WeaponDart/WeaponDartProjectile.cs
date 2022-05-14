using RigidbodyModels.Projectiles;
using UnityEngine;
using Utils;
using Vector2 = UnityEngine.Vector2;

namespace PlayerControls.Weapons.WeaponDart
{
    public class WeaponDartProjectile : ProjectileModelBase
    {
        [SerializeField] [Range(0,1)] private float startForce;
        
        protected override void LateStart()
        {
            SetGravity(1);

            Vector2 force = CalculateStartForce();
            
            AddForce(force);
        }
        
        protected override bool TryGetAngleRotation(float rotation, out float angle)
        {
            angle = Helpers.GetAngleFromDirection(this.CalculateDirection);

            angle += 180;

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
        
        private Vector2 CalculateStartForce()
        {
            Vector2 currentPosition = transform.position;

            Vector2 direction = (Vector2)TargetPosition - currentPosition;

            direction.Normalize();

            return direction * startForce;
        }
    }
}