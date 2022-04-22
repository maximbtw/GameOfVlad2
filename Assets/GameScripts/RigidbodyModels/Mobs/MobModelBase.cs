namespace RigidbodyModels.Mobs
{
    public class MobModelBase : RigidbodyModelBase
    {
        public MobModelBase Parent { get; private set; }
        
        public int HeatPoint { get; private set; }
        
        public int Armor { get; private set; }
        
        public int Damage { get; private set; }

        protected sealed override bool TryGetLayer(out GameObjectLayer layer)
        {
            layer = GameObjectLayer.Mob;

            return true;
        }
    }
}