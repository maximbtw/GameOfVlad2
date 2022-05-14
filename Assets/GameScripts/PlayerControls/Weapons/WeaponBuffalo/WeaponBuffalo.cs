using System.Collections.Generic;
using RigidbodyModels;
using UnityEditor;
using UnityEngine;

namespace PlayerControls.Weapons.WeaponBuffalo
{
    public class WeaponBuffalo : WeaponBase
    {
        [SerializeField] private WeaponBuffaloProjectile projectilePrefab;
        [SerializeField] [Range(0, 1)] private float dispersionCoefficient = 0.1f;

        private const int CountProjectile = 5;

        public override WeaponType GetWeaponType() => WeaponType.Buffalo;

        private void Awake()
        {
            projectilePrefab =
                AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Player/Weapon/Buffalo/WeaponBuffaloProjectile.prefab",
                    typeof(WeaponBuffaloProjectile)) as WeaponBuffaloProjectile;

            Controller = gameObject.AddComponent<WeaponBuffaloController>();
        }

        protected override void CreateBullet()
        {
            List<Vector2> directions = GetProjectileDirections();

            for (var i = 0; i < CountProjectile; i++)
            {
                WeaponBuffaloProjectile projectile = Instantiate(projectilePrefab);

                projectile.Initialize(
                    GameObjectLayer.PlayerObject,
                    Player,
                    startPosition: Player.Position,
                    targetPosition: Controller.GetMousePosition(),
                    fixedDirection: directions[i],
                    speedProjectile: null,
                    damage,
                    knockback);
            }
        }

        private List<Vector2> GetProjectileDirections()
        {
            var directions = new List<Vector2>();

            Vector2 targetPosition = Controller.GetMousePosition();

            Vector2 direction = targetPosition - Player.Position;

            direction.Normalize();

            directions.Add(direction);

            for (var i = 1; directions.Count < CountProjectile; i++)
            {
                directions.Add(item: GetOffsetDirection(direction, i));
                directions.Add(item: GetOffsetDirection(direction, -i));
            }

            return directions;
        }

        private Vector2 GetOffsetDirection(Vector2 mainDirection, int projectileNumber)
        {
            Vector2 offset = Vector2.zero;

            float pointOffset = projectileNumber * dispersionCoefficient;

            if (mainDirection.y < 0.5f && mainDirection.y > 0)
            {
                offset = new Vector2(x: 0, y: -pointOffset);
            }
            else if (mainDirection.y > -0.5f && mainDirection.y < 0)
            {
                offset = new Vector2(x: 0, y: pointOffset);
            }
            else if (mainDirection.x > 0)
            {
                offset = new Vector2(x: -pointOffset, y: 0);
            }
            else if (mainDirection.x < 0)
            {
                offset = new Vector2(x: pointOffset, y: 0);
            }

            mainDirection += offset;

            return mainDirection.normalized;
        }
    }
}