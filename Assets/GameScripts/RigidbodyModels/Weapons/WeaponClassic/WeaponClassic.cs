﻿using System;
using RigidbodyModels.Projectiles;
using UnityEditor;
using UnityEngine;

namespace RigidbodyModels.Weapons.WeaponClassic
{
    public partial class WeaponClassic : WeaponBase
    {
        [SerializeField] private WeaponClassicProjectile bulletPrefab;
        [SerializeField] private float bulletSpeed;

        private void Awake()
        {
            damage = 10;
            shootCooldown = 2;
            bulletSpeed = 7f;
            
            bulletPrefab =
                AssetDatabase.LoadAssetAtPath("Assets/Prefabs/ProjectilePrefab/WeaponClassicBullet.prefab",
                    typeof(WeaponClassicProjectile)) as WeaponClassicProjectile;
        }

        public override Weapon GetWeaponType() => Weapon.Classic;

        protected override void CreateBullet()
        {
            WeaponClassicProjectile bullet = Instantiate(bulletPrefab);

            bullet.Initialization(
                Player, 
                Player.Position +new Vector2(0.1f,0.1f), 
                _mousePosition,
                bulletSpeed,
                damage);
        }
    }
}