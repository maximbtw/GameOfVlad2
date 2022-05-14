using RigidbodyModels.Projectiles;

namespace PlayerControls.Weapons
{
    public interface IProjectileHit
    {
        void OnHitNotStaticObject(object sender, CollisionEnterEventArgs e);

        void OnHitStaticObject(object sender, CollisionEnterEventArgs e);
    }
}