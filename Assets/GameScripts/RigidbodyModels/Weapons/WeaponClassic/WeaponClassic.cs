using UnityEngine;

namespace RigidbodyModels.Weapons.WeaponClassic
{
    public partial class WeaponClassic : WeaponModelBase
    {
        [SerializeField] private WeaponClassicProjectile bulletPrefab;
        [SerializeField] private float bulletSpeed;

        protected override void Update()
        {
            base.Update();

            UserInputUpdate();
        }

        protected override void Shoot()
        {
            if (CanShoot)
            {
                CreateBullet();

                base.Shoot();
            }
        }

        private void CreateBullet()
        {
            WeaponClassicProjectile bullet = Instantiate(bulletPrefab);

            bullet.Initialization(
                Player, 
                Player.Position, 
                _mousePosition,
                bulletSpeed,
                damage);
        }
    }
}