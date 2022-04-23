using RigidbodyModels.Projectiles;
using UnityEditor;
using UnityEngine;

namespace RigidbodyModels.Mobs
{
    public class MobModelBase : RigidbodyModelBase
    {
        [SerializeField] [Range(1, 100000)] private int maxHeatPoint;
        [SerializeField] [Range(0, 100000)] private int damage;
        [SerializeField] private int armor;

        private HeatBarMobLineComponent _heatBarComponent;
        private int _heatPoint;
        
        public int MaxHeatPoint => maxHeatPoint;
        public int HeatPoint => _heatPoint;
        public int Armor => armor;
        public int Damage => damage;
        
        //public MobModelBase Parent { get; }

        private void Awake()
        {
            LoadHealBarLineComponent();
            
            _heatPoint = maxHeatPoint;
        }

        protected sealed override bool TryGetLayer(out GameObjectLayer layer)
        {
            layer = GameObjectLayer.Mob;

            return true;
        }

        public override void OnProjectileHit(ProjectileModelBase sender, CollisionEnterEventArgs e)
        {
            _heatPoint -= sender.Damage - Armor;

            if (HeatPoint <= 0)
            {
                OnHeatPointBecomeNegativeOrZero();
            }
        }

        protected virtual void OnHeatPointBecomeNegativeOrZero()
        {
            Destroy(gameObject);
        }

        private void LoadHealBarLineComponent()
        {
            var heatBarComponentAsset =
                AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Components/HealAndArmorBarComponent.prefab",
                    typeof(GameObject)) as GameObject;
            
            GameObject heatBarComponent = Instantiate(heatBarComponentAsset, transform, worldPositionStays: true);

            _heatBarComponent = heatBarComponent.GetComponent<HeatBarMobLineComponent>();
            _heatBarComponent.Initialize(this);
        }
    }
}