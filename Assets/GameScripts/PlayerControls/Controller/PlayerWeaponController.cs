using System.Collections.Generic;
using PlayerControls.Weapons;
using UnityEngine;

namespace PlayerControls.Controller
{
    public class PlayerWeaponController : MonoBehaviour
    {
        private Dictionary<WeaponType, WeaponBase> _availableWeapons = new Dictionary<WeaponType, WeaponBase>();
        private int MaxAvailableWeaponIndex => _availableWeapons.Count - 1;

        public WeaponType CurrentWeaponType { get; private set; }

        private void Start()
        {
            LoadAvailableWeapons();
        }

        private void Update()
        {
            UpdateWeaponSwitch();
        }

        public void AddWeapon(WeaponType weaponType)
        {
            if (_availableWeapons.ContainsKey(weaponType))
            {
                return;
            }

            WeaponContainer.WeaponComponent weaponComponent = WeaponContainer.Instance.GetWeaponByType(weaponType);

            var weapon = (WeaponBase) this.gameObject.AddComponent(weaponComponent.Weapon);

            weapon.UpdateProperties(
                weaponComponent.damage,
                weaponComponent.shootCooldown,
                weaponComponent.knockback);

            _availableWeapons.Add(weaponType, weapon);
            
            weapon.Unselect();
        }

        public void RemoveWeapon(WeaponType weaponType)
        {
            if (weaponType == WeaponType.None)
            {
                Debug.LogError("Нельзя удалить тип оружия по умолчанию");

                return;
            }

            if (_availableWeapons.ContainsKey(weaponType))
            {
                if (CurrentWeaponType == weaponType)
                {
                    UpdateCurrentWeapon((int) CurrentWeaponType - 1);
                }

                _availableWeapons.Remove(weaponType);
            }
        }

        private void LoadAvailableWeapons()
        {
            _availableWeapons = new Dictionary<WeaponType, WeaponBase>
            {
                {WeaponType.None, null}
            };

            AddWeapon(WeaponType.Classic);
            AddWeapon(WeaponType.Dart);


            // TODO: Загружем доступные оружия);
        }

        private void UpdateWeaponSwitch()
        {
            int userInput = HandleUserInput();

            if (userInput == 0)
            {
                return;
            }

            int newWeaponIndex = (int) CurrentWeaponType + userInput;
            
            if (newWeaponIndex > MaxAvailableWeaponIndex)
            {
                newWeaponIndex = 0;
            }

            if (newWeaponIndex < 0)
            {
                newWeaponIndex = MaxAvailableWeaponIndex;
            }

            UpdateCurrentWeapon(newWeaponIndex);
        }

        private void UpdateCurrentWeapon(int newWeaponIndex)
        {
            WeaponBase previousWeapon = _availableWeapons[CurrentWeaponType];

            // ReSharper disable once Unity.NoNullPropagation
            previousWeapon?.Unselect();

            CurrentWeaponType = (WeaponType) newWeaponIndex;

            WeaponBase currentWeapon = _availableWeapons[CurrentWeaponType];
            
            if (currentWeapon != null)
            {
                WeaponContainer.WeaponComponent weaponComponent =
                    WeaponContainer.Instance.GetWeaponByType(CurrentWeaponType);
                
                currentWeapon.UpdateProperties(
                    weaponComponent.damage,
                    weaponComponent.shootCooldown,
                    weaponComponent.knockback);
                
                currentWeapon.Select();
            }

            Debug.Log("Weapon switch to " + CurrentWeaponType);
        }

        private static int HandleUserInput()
        {
            // Так как scrollAxis может быть только -0.1 или 0.1 поэтому умножаем на 10
            var scrollAxis = (int) (Input.GetAxis("Mouse ScrollWheel") * 10);

            if (Input.GetKey(KeyCode.Q))
            {
                scrollAxis = 1;
            }

            return scrollAxis;
        }
    }
}