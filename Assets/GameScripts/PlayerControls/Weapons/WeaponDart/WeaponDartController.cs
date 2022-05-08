namespace PlayerControls.Weapons.WeaponDart
{
    public class WeaponDartController : WeaponControllerBase
    {
        protected override void UserInputUpdate()
        {
            if (LeftMousePressedHeld)
            {
                OnShoot();
            }
        }
    }
}