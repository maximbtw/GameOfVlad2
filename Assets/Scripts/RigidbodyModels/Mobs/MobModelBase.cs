namespace RigidbodyModels.Mobs
{
    public class MobModelBase : DynamicRigidbodyModelBase
    {
        public MobModelBase Parent { get; private set; }
        
        public int HeatPoint { get; private set; }
        
        public int Armor { get; private set; }
        
        public int Damage { get; private set; }
    }
}