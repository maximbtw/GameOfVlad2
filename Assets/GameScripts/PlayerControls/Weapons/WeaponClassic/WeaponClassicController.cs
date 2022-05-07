using System;

namespace PlayerControls.Weapons.WeaponClassic
{
    public class WeaponClassicController : WeaponControllerBase
    {
        public event Action Shoot;

        protected override void UserInputUpdate()
        {
            if (LeftMousePressedHeld)
            {
                Shoot?.Invoke();
            }
        }
    }
}