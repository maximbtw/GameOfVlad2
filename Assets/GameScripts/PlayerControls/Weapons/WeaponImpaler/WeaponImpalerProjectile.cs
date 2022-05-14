using RigidbodyModels.Projectiles;
using Utils;

namespace PlayerControls.Weapons.WeaponImpaler
{
    public class WeaponImpalerProjectile : LaserProjectileModelBase
    {
        protected override bool TryGetAngleRotation(float rotation, out float angle)
        {
            angle = Helpers.GetAngleFromDirection(this.Direction);
            
            return true;
        }
    }
}