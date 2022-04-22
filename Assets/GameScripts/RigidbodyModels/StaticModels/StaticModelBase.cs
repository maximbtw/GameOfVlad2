namespace RigidbodyModels.StaticModels
{
    public class StaticModelBase : RigidbodyModelBase
    {
        protected sealed override bool TryGetLayer(out GameObjectLayer layer)
        {
            layer = GameObjectLayer.Static;

            return true;
        }
    }
}