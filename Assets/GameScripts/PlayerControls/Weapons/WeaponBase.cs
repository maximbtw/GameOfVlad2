using System;
using Components;
using PlayerControls.Weapons.WeaponClassic;
using RigidbodyModels.PlayerModel;
using UnityEngine;

namespace PlayerControls.Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected int damage;
        [SerializeField] [Range(0,100000)] protected float shootCooldown;
        [SerializeField] [Range(0,10000)] protected float knockback;
        
        protected WeaponClassicController Controller;
        
        protected Player Player { get; private set; }
        protected Timer CooldownTimer { get; private set; }
        protected bool CanShoot => !CooldownTimer.IsActive;
        
        protected event Action Shooting;
        
        public abstract WeaponType GetWeaponType();

        public virtual void Select()
        {
            Controller.enabled = true;
        }

        public virtual void Unselect()
        {
            Controller.enabled = false;
        }

        public void UpdateProperties(int weaponDamage, float weaponShootCooldown, float weaponKnockback)
        {
            this.damage = weaponDamage;
            this.shootCooldown = weaponShootCooldown;
            this.knockback = weaponKnockback;

            bool updateTimer = CooldownTimer != null &&
                               Math.Abs(this.shootCooldown - CooldownTimer.CountdownTime) > 0.001f;

            if (updateTimer)
            {
                CooldownTimer = new Timer(this.shootCooldown);
            }
        }

        protected virtual void Start()
        {
            Player = gameObject.GetComponent<Player>();

            CooldownTimer = new Timer(shootCooldown);
        }

        protected virtual void Update()
        {
            CooldownTimer.Update();
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

        protected abstract void CreateBullet();
    }
}