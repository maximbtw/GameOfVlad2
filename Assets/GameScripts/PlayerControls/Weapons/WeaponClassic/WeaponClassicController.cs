namespace PlayerControls.Weapons.WeaponClassic
{
    public class WeaponClassicController : WeaponControllerBase
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