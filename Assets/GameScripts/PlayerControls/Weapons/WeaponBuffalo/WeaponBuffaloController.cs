namespace PlayerControls.Weapons.WeaponBuffalo
{
    public class WeaponBuffaloController : WeaponControllerBase
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