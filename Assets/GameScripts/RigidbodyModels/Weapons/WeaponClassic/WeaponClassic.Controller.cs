using UnityEngine;

namespace RigidbodyModels.Weapons.WeaponClassic
{
    public partial class WeaponClassic
    {
        private Vector2 _mousePosition;

        protected override void UserInputUpdate()
        {
            _mousePosition = Input.mousePosition;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
            }
        }
    }
}