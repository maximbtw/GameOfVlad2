﻿using RigidbodyModels.Weapons;
using UnityEngine;

namespace RigidbodyModels.Player
{
    public sealed class Player : RigidbodyModelBase
    {
        private PlayerMoveController _moveController;
        private PlayerWeaponController _weaponController;
        
        public int HeatPoint { get; private set; }
        
        public int Armor { get; private set; }
        
        public Weapon GetCurrentWeapon()
        {
            return _weaponController.CurrentWeapon;
        }
        
        protected override bool TryGetDirection(out Vector2 direction)
        {
            return _moveController.TryGetDirection(out direction);
        }

        protected override void Start()
        {
            base.Start();

            LoadMoveController();
            LoadWeaponController();
        }

        protected override bool TryGetLayer(out GameObjectLayer layer)
        {
            layer = GameObjectLayer.Player;

            return true;
        }

        private void LoadWeaponController()
        {
            _weaponController = gameObject.GetComponent<PlayerWeaponController>();

            if (_weaponController == null)
            {
                _weaponController = gameObject.AddComponent<PlayerWeaponController>();
            }
        }

        private void LoadMoveController()
        {
            _moveController = gameObject.GetComponent<PlayerMoveController>();

            if (_moveController == null)
            {
                _moveController = gameObject.AddComponent<PlayerMoveController>();
            }
        }
    }
}