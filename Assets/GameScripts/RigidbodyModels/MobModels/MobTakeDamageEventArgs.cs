using System;

namespace RigidbodyModels.MobModels
{
    public class MobTakeDamageEventArgs : EventArgs
    {
        public RigidbodyModelBase BodyCausedDamage { get; }
        
        public MobTakeDamageEventArgs(RigidbodyModelBase bodyCausedDamage)
        {
            this.BodyCausedDamage = bodyCausedDamage;
        }
    }
}