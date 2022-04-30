using System;
using Components;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Player.Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected int damage;
        [SerializeField] [Range(0,100000)] protected float shootCooldown;
        [SerializeField] [Range(0,10000)] protected float knockback;
        protected RigidbodyModels.Player.Player Player { get; private set; }
        protected Timer CooldownTimer { get; private set; }
        protected bool CanShoot => !CooldownTimer.IsActive;
        
        protected event Action Shooting;

        
        public abstract Weapon GetWeaponType();

        protected virtual void Start()
        {
            Player = gameObject.GetComponent<RigidbodyModels.Player.Player>();

            CooldownTimer = new Timer(shootCooldown);
        }

        protected virtual void Update()
        {
            CooldownTimer.Update();

            if (Player.GetCurrentWeapon() == GetWeaponType())
            {
                UserInputUpdate();
            }
        }
        
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