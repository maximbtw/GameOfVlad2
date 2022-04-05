﻿using System;
using System.Collections.Generic;
using RigidbodyModels.Weapons;
using RigidbodyModels.Weapons.WeaponClassic;
using UnityEngine;

namespace RigidbodyModels.Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        // TODO: Временный хак, пока не знаю как реализовать
        private readonly Dictionary<Weapon, Type> _weaponTypeByWeaponIndex = new Dictionary<Weapon, Type>
        {
            { Weapon.Classic, typeof(WeaponClassic) }
        };

        private Dictionary<Weapon, WeaponModelBase> _availableWeapons;
        private int MaxAvailableWeaponIndex => _availableWeapons.Count - 1;
        
        public Weapon CurrentWeapon { get; private set; }

        public void AddWeapon(Weapon weapon)
        {
            if (!_availableWeapons.ContainsKey(weapon))
            {
                Type weaponType = _weaponTypeByWeaponIndex[weapon];

                _availableWeapons.Add(weapon, (WeaponModelBase)gameObject.AddComponent(weaponType));
            }
        }
        
        public void RemoveWeapon(Weapon weapon)
        {
            if (weapon == Weapon.None)
            {
                Debug.LogError("Нельзя удалить тип оружия по умолчанию");
                
                return;
            }
            
            if (_availableWeapons.ContainsKey(weapon))
            {
                if (CurrentWeapon == weapon)
                {
                    UpdateCurrentWeapon((int)CurrentWeapon - 1);
                }
                
                _availableWeapons.Remove(weapon);
            }
        }

        private void Start()
        {
            LoadAvailableWeapon();
        }

        private void LoadAvailableWeapon()
        {
            _availableWeapons = new Dictionary<Weapon, WeaponModelBase>()
            {
                { Weapon.None, null }
            };
            
            AddWeapon(Weapon.Classic);
            

            // TODO: Загружем доступные оружия);
        }

        private void Update()
        {
            UpdateWeaponSwitch();
        }

        private void UpdateWeaponSwitch()
        {
            int userInput = HandleUserInput();

            if (userInput == 0)
            {
                return;
            }

            int nextWeaponIndex = (int)CurrentWeapon + userInput;

            UpdateCurrentWeapon(nextWeaponIndex);
        }

        private void UpdateCurrentWeapon(int newWeaponIndex)
        {
            if (newWeaponIndex > MaxAvailableWeaponIndex)
            {
                newWeaponIndex = 0;
            }
            
            if (newWeaponIndex < 0)
            {
                newWeaponIndex = MaxAvailableWeaponIndex;
            }

            CurrentWeapon = (Weapon)newWeaponIndex;
        }

        private static int HandleUserInput()
        {
            // Так как scrollAxis может быть только -0.1 или 0.1 поэтому умножаем на 10
            int scrollAxis = (int)(Input.GetAxis("Mouse ScrollWheel") * 10);
            
            if (Input.GetKey(KeyCode.Q))
            {
                scrollAxis = 1;
            }

            return scrollAxis;
        }
        
    }
}