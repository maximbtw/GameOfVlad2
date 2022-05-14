namespace PlayerControls.Weapons.WeaponImpaler
{
    public class WeaponImpalerController : WeaponControllerBase
    {
        protected override void UserInputUpdate()
        {
            if (this.LeftMousePressedHeld)
            {
                OnShoot();
            }
        }
    }
}