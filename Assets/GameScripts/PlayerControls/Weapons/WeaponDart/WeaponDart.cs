using RigidbodyModels;
using UnityEditor;
using UnityEngine;

namespace PlayerControls.Weapons.WeaponDart
{
    public class WeaponDart : WeaponBase
    {
        [SerializeField] private WeaponDartProjectile bulletPrefab;
        public override WeaponType GetWeaponType() => WeaponType.Dart;
        
        private void Awake()
        {
            bulletPrefab =
                AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Player/Weapon/Dart/WeaponDartProjectile.prefab",
                    typeof(WeaponDartProjectile)) as WeaponDartProjectile;
            
            Controller = gameObject.AddComponent<WeaponDartController>();

            Controller.Shoot += Shoot;
        }

        protected override void CreateBullet()
        {
            WeaponDartProjectile bullet = Instantiate(bulletPrefab);

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