using System;
using System.Collections.Generic;
using PlayerControls.Weapons;
using PlayerControls.Weapons.WeaponClassic;
using UnityEngine;

namespace PlayerControls.Controller
{
    public class PlayerWeaponController : MonoBehaviour
    {
        // TODO: Временный хак, пока не знаю как реализовать
        private readonly Dictionary<Weapon, Type> _weaponTypeByWeaponIndex = new Dictionary<Weapon, Type>
        {
            {Weapon.Classic, typeof(WeaponClassic)}
        };

        private Dictionary<Weapon, WeaponBase> _availableWeapons;
        private int MaxAvailableWeaponIndex => _availableWeapons.Count - 1;

        public Weapon CurrentWeapon { get; private set; }

        private void Start()
        {
            LoadAvailableWeapon();
        }

        private void Update()
        {
            UpdateWeaponSwitch();
        }

        public void AddWeapon(Weapon weapon)
        {
            if (!_availableWeapons.ContainsKey(weapon))
            {
                var weaponType = _weaponTypeByWeaponIndex[weapon];

                _availableWeapons.Add(weapon, (WeaponBase) gameObject.AddComponent(weaponType));
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
                if (CurrentWeapon == weapon) UpdateCurrentWeapon((int) CurrentWeapon - 1);

                _availableWeapons.Remove(weapon);
            }
        }

        private void LoadAvailableWeapon()
        {
            _availableWeapons = new Dictionary<Weapon, WeaponBase>
            {
                {Weapon.None, null}
            };

            AddWeapon(Weapon.Classic);


            // TODO: Загружем доступные оружия);
        }

        private void UpdateWeaponSwitch()
        {
            int userInput = HandleUserInput();

            if (userInput == 0) return;

            int nextWeaponIndex = (int) CurrentWeapon + userInput;

            UpdateCurrentWeapon(nextWeaponIndex);
        }

        private void UpdateCurrentWeapon(int newWeaponIndex)
        {
            if (newWeaponIndex > MaxAvailableWeaponIndex) newWeaponIndex = 0;

            if (newWeaponIndex < 0) newWeaponIndex = MaxAvailableWeaponIndex;

            CurrentWeapon = (Weapon) newWeaponIndex;

            Debug.Log("Weapon switch to " + CurrentWeapon);
        }

        private static int HandleUserInput()
        {
            // Так как scrollAxis может быть только -0.1 или 0.1 поэтому умножаем на 10
            var scrollAxis = (int) (Input.GetAxis("Mouse ScrollWheel") * 10);

            if (Input.GetKey(KeyCode.Q)) scrollAxis = 1;

            return scrollAxis;
        }
    }
}