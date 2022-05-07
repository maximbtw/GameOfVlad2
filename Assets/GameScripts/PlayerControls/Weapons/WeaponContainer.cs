﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerControls.Weapons
{
    public class WeaponContainer : MonoBehaviour
    {
        public static WeaponContainer Instance;

        [SerializeField] private List<WeaponComponent> weapons;

        private static readonly Dictionary<WeaponType, Type> WeaponsCash = new Dictionary<WeaponType, Type>
        {
            {WeaponType.Classic, typeof(WeaponClassic.WeaponClassic)}
        };

        private void Awake()
        {
            Instance = this;
        }

        public WeaponComponent GetWeaponByType(WeaponType weaponType) => 
            weapons.First(x => x.type == weaponType);

        [Serializable]
        public class WeaponComponent
        {
            public Type Weapon => WeaponsCash[type];
            
            public WeaponType type;
            
            [Space]
            public int damage;
            public float shootCooldown;
            public float knockback;
        }
    }
}