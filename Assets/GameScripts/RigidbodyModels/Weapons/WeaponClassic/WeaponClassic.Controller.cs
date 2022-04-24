using UnityEngine;

namespace RigidbodyModels.Weapons.WeaponClassic
{
    public partial class WeaponClassic
    {
        private bool _leftMousePressedHeld;
        private Vector2 _mousePosition;

        protected override void UserInputUpdate()
        {
            if (Camera.main == null) return;

            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
                _leftMousePressedHeld = true;
            else if (Input.GetMouseButtonUp(0)) _leftMousePressedHeld = false;

            if (_leftMousePressedHeld) Shoot();
        }
    }
}