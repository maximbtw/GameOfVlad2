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

        public int MaxHeatPoint => maxHeatPoint;
        public int HeatPoint { get; private set; }

        public int Armor => armor;
        public int Damage => damage;

        //public MobModelBase Parent { get; }

        private void Awake()
        {
            LoadHealBarLineComponent();

            HeatPoint = maxHeatPoint;
        }

        protected sealed override bool TryGetLayer(out GameObjectLayer layer)
        {
            layer = GameObjectLayer.Mob;

            return true;
        }

        public override void OnProjectileHit(ProjectileModelBase sender, CollisionEnterEventArgs e)
        {
            HeatPoint -= sender.Damage - Armor;

            Debug.Log(HeatPoint);

            if (HeatPoint <= 0) OnHeatPointBecomeNegativeOrZero();
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

            var heatBarComponent = Instantiate(heatBarComponentAsset, transform, true);

            _heatBarComponent = heatBarComponent.GetComponent<HeatBarMobLineComponent>();
            _heatBarComponent.Initialize(this);
        }
    }
}