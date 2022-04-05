using System;
using Components;
using UnityEngine;

namespace RigidbodyModels.Weapons
{
    public abstract class WeaponModelBase : MonoBehaviour
    {
        [SerializeField] protected int damage;
        [SerializeField] protected float shootCooldown;
        protected Player.Player Player { get; private set; }
        protected Timer CooldownTimer { get; private set; }

        protected event Action Shooting;
        protected bool CanShoot => CooldownTimer.IsActive;

        public Weapon Weapon;

        private void Start()
        {
            Player = gameObject.GetComponent<Player.Player>();

            CooldownTimer = new Timer(shootCooldown);
        }

        protected virtual void Update()
        {
            CooldownTimer.Update();

            if (Player.GetCurrentWeapon() == Weapon)
            {
                UserInputUpdate();
            }
        }

        protected virtual void Shoot()
        {
            if (CanShoot)
            {
                CooldownTimer.Start();

                Shooting?.Invoke();
            }
        }

        protected abstract void UserInputUpdate();
    }
}