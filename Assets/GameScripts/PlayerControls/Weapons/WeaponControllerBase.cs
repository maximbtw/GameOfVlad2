using System;
using UnityEngine;

namespace PlayerControls.Weapons
{
    public abstract class WeaponControllerBase : MonoBehaviour
    {
        protected bool LeftMousePressedHeld;
        
        private Camera _camera;

        protected event Action LeftMouseClick;
        protected event Action RightMouseClick;
        
        public Vector2 GetMousePosition() => _camera.ScreenToWorldPoint(Input.mousePosition);

        protected abstract void UserInputUpdate();
        
        private void Start()
        {
            _camera = Camera.main;
        }
        
        private void Update()
        {
            UpdateMouse();
            UserInputUpdate();
        }

        private void UpdateMouse()
        {
            if (Input.GetMouseButtonDown(0))
            {
                LeftMousePressedHeld = true;
                LeftMouseClick?.Invoke();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                LeftMousePressedHeld = false;
            }

            if (Input.GetMouseButtonDown(1))
            {
                RightMouseClick?.Invoke();
            }
        }
    }
}