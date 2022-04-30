using UnityEngine;

namespace Player.Weapons.WeaponClassic
{
    public partial class WeaponClassic
    {
        private bool _leftMousePressedHeld;
        private Vector2 _mousePosition;
        private UnityEngine.Camera _camera;

        protected override void Start()
        {
            base.Start();
            
            _camera = UnityEngine.Camera.main;
        }

        protected override void UserInputUpdate()
        {
            _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                _leftMousePressedHeld = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _leftMousePressedHeld = false;
            }

            if (_leftMousePressedHeld)
            {
                Shoot();
            }
        }
    }
}