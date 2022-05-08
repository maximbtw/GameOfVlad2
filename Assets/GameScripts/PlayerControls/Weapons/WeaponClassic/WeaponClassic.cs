using RigidbodyModels;
using UnityEditor;
using UnityEngine;

namespace PlayerControls.Weapons.WeaponClassic
{
    public class WeaponClassic : WeaponBase
    {
        [SerializeField] private WeaponClassicProjectile bulletPrefab;

        public override WeaponType GetWeaponType() => WeaponType.Classic;

        private void Awake()
        {
            bulletPrefab =
                AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Player/Weapon/Classic/WeaponClassicBullet.prefab",
                    typeof(WeaponClassicProjectile)) as WeaponClassicProjectile;
            
            Controller = gameObject.AddComponent<WeaponClassicController>();

            Controller.Shoot += Shoot;
        }

        protected override void CreateBullet()
        {
            WeaponClassicProjectile bullet = Instantiate(bulletPrefab);

            bullet.Initialize(
                GameObjectLayer.PlayerObject,
                Player,
                startPosition: Player.Position,
                targetPosition: Controller.GetMousePosition(),
                speedProjectile: null,
                damage,
                knockback);
        }
    }
}