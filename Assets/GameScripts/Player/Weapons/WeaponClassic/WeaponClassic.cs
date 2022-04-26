using UnityEditor;
using UnityEngine;

namespace Player.Weapons.WeaponClassic
{
    public partial class WeaponClassic : WeaponBase
    {
        [SerializeField] private WeaponClassicProjectile bulletPrefab;
        [SerializeField] private float bulletSpeed;

        private void Awake()
        {
            damage = 10;
            shootCooldown = 2;
            bulletSpeed = 4f;
            knockback = 1;

            bulletPrefab =
                AssetDatabase.LoadAssetAtPath("Assets/Prefabs/ProjectilePrefab/WeaponClassicBullet.prefab",
                    typeof(WeaponClassicProjectile)) as WeaponClassicProjectile;
        }

        public override Weapon GetWeaponType()
        {
            return Weapon.Classic;
        }

        protected override void CreateBullet()
        {
            var bullet = Instantiate(bulletPrefab);

            bullet.Initialize(
                Player,
                Player.Position + new Vector2(0.1f, 0.1f),
                _mousePosition,
                bulletSpeed,
                damage,
                knockback);
        }
    }
}