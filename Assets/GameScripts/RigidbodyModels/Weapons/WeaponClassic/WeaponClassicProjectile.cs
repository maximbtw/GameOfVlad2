using RigidbodyModels.Projectiles;
using Utils;
using Vector2 = UnityEngine.Vector2;

namespace RigidbodyModels.Weapons.WeaponClassic
{
    public class WeaponClassicProjectile : ProjectileModelBase
    {
        private Vector2 _direction;
        
        protected override void Start()
        {
            base.Start();
            
            SwitchToKinematic();
        }

        public override void Initialize(RigidbodyModelBase parent, Vector2 startPosition, Vector2 targetPosition, float speed,
            int damage)
        {
            base.Initialize(parent, startPosition, targetPosition, speed, damage);

            SetDirection();
        }

        protected override bool TryGetDirection(out Vector2 direction)
        {
            direction = _direction;
            
            return true;
        }

        protected override bool TryGetRotation(out float angle)
        {
            angle = Helpers.GetAngleFromDirection(_direction);

            angle -= 90;

            return true;
        }

        protected override void OnHitNotStaticObject(object sender, CollisionEnterEventArgs e)
        {
            base.OnHitNotStaticObject(sender, e);
            
            Destroy(this.gameObject);
        }

        private void SetDirection()
        {
            Vector2 currentPosition = transform.position;
            
            _direction = TargetPosition - currentPosition;
            
            _direction.Normalize();
        }
    }
}