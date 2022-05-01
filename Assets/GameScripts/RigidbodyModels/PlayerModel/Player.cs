using PlayerControls.Controller;
using PlayerControls.Weapons;
using RigidbodyModels.Projectiles;
using UnityEngine;
using Utils;

namespace RigidbodyModels.PlayerModel
{
    public sealed class Player : RigidbodyModelBase
    {
        private PlayerMoveController _moveController;
        private PlayerWeaponController _weaponController;

        public int HeatPoint { get; private set; }

        public int Armor { get; private set; }

        public Weapon GetCurrentWeapon()
        {
            return _weaponController.CurrentWeapon;
        }
        
        public override void OnProjectileHit(ProjectileModelBase sender, CollisionEnterEventArgs e)
        {
            SetDamage(sender.Damage);
            SetKnockback(sender.Direction, sender.Knockback);
        }
        
        protected override void Start()
        {
            base.Start();

            LoadMoveController();
            LoadWeaponController();
        }

        protected override bool TryGetDirection(out Vector2 direction)
        {
            return _moveController.TryGetDirection(out direction);
        }

        protected override bool TryGetAngleRotation(Vector2 direction, float rotation, out float angle)
        {
            angle = Helpers.GetAngleFromDirection(direction * -1);

            angle = Mathf.LerpAngle(rotation, angle, rotationSpeed * Time.deltaTime);

            return true;
        }

        protected override bool TryGetLayer(out GameObjectLayer layer)
        {
            layer = GameObjectLayer.Player;

            return true;
        }
        
        private void SetKnockback(Vector2 direction, float knockback)
        {
            Vector2 knockbackForce = direction * knockback;
            
            AddForce(knockbackForce);
        }

        private void SetDamage(int takenDamage)
        {
            // TODO:
            
            return;
            HeatPoint -= takenDamage - Armor;

            if (HeatPoint <= 0)
            {
                OnHeatPointBecomeNegativeOrZero();
            }
        }

        private void OnHeatPointBecomeNegativeOrZero()
        {
            Destroy(gameObject);
        }

        private void LoadWeaponController()
        {
            _weaponController = gameObject.GetComponent<PlayerWeaponController>();

            if (_weaponController == null)
            {
                _weaponController = gameObject.AddComponent<PlayerWeaponController>();
            }
        }

        private void LoadMoveController()
        {
            _moveController = gameObject.GetComponent<PlayerMoveController>();

            if (_moveController == null)
            {
                _moveController = gameObject.AddComponent<PlayerMoveController>();
            }
        }
    }
}