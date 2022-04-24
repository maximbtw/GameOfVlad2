﻿using System;
using Components;
using UnityEngine;

namespace RigidbodyModels.Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected int damage;
        [SerializeField] protected float shootCooldown;
        protected Player.Player Player { get; private set; }
        protected Timer CooldownTimer { get; private set; }
        protected bool CanShoot => !CooldownTimer.IsActive;

        protected virtual void Start()
        {
            Player = gameObject.GetComponent<Player.Player>();

            CooldownTimer = new Timer(shootCooldown);
        }

        protected virtual void Update()
        {
            CooldownTimer.Update();

            if (Player.GetCurrentWeapon() == GetWeaponType()) UserInputUpdate();
        }

        protected event Action Shooting;

        public abstract Weapon GetWeaponType();

        protected virtual void Shoot()
        {
            if (CanShoot)
            {
                CreateBullet();

                CooldownTimer.Start();

                Shooting?.Invoke();
            }
        }

        protected abstract void UserInputUpdate();

        protected abstract void CreateBullet();
    }
}