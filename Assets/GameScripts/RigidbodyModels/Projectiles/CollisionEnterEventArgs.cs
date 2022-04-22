using System;

namespace RigidbodyModels.Projectiles
{
    public class CollisionEnterEventArgs : EventArgs
    {
        public RigidbodyModelBase HitObject;

        public CollisionEnterEventArgs(RigidbodyModelBase hitObject)
        {
            HitObject = hitObject;
        }
    }
}