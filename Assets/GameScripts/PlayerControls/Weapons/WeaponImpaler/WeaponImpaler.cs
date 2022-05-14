using System;
using RigidbodyModels;
using UnityEditor;
using UnityEngine;

namespace PlayerControls.Weapons.WeaponImpaler
{
    public class WeaponImpaler : WeaponBase
    {
        [SerializeField] private WeaponImpalerProjectile laserItemPrefab;
        [SerializeField] [Range(0, 100)] private float laserDistance = 15;
        [SerializeField] [Range(0, 100)] private float distanceBetweenProjectilePrefab = 0.45f;

        public override WeaponType GetWeaponType() => WeaponType.Impaler;
        
        private void Awake()
        {
            laserItemPrefab =
                AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Player/Weapon/Impaler/WeaponImpalerProjectile.prefab",
                    typeof( WeaponImpalerProjectile)) as  WeaponImpalerProjectile;
            
            Controller = gameObject.AddComponent<WeaponImpalerController>();
        }

        protected override void CreateBullet()
        {
            ConstructLaserLine();
        }

        private void ConstructLaserLine()
        {
            Vector2 startPosition = Player.Position;
            Vector2 direction = (this.Controller.GetMousePosition() - startPosition).normalized;
            Vector2 lastGeneratedLaserItemPosition = startPosition;
            
            float currentDistance = 0;

            while (currentDistance <= laserDistance)
            {
                CreateLaserItem(lastGeneratedLaserItemPosition, direction);

                lastGeneratedLaserItemPosition += direction * distanceBetweenProjectilePrefab;

                currentDistance = (lastGeneratedLaserItemPosition - startPosition).magnitude;
            }
        }

        private void CreateLaserItem(Vector2 position, Vector2 direction)
        {
            WeaponImpalerProjectile laserItem = Instantiate(laserItemPrefab);

            laserItem.Initialize(
                GameObjectLayer.Player,
                Player,
                position,
                targetPosition: null,
                direction,
                this.damage);
        }
    }
}