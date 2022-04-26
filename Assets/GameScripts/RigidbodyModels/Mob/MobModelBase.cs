using Components.MobComponent;
using RigidbodyModels.Projectiles;
using UnityEditor;
using UnityEngine;

namespace RigidbodyModels.Mob
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
            SetDamage(sender.Damage);
            SetKnockback(sender.Direction, sender.Knockback);
        }

        protected virtual void SetKnockback(Vector2 direction, float knockback)
        {
            Vector2 knockbackForce = direction * knockback;

            Debug.Log("Add force: " + knockbackForce);
            AddForce(knockbackForce);
        }

        protected virtual void SetDamage(int takenDamage)
        {
            HeatPoint -= takenDamage - Armor;

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

            var heatBarComponent = Instantiate(heatBarComponentAsset, transform, true);

            _heatBarComponent = heatBarComponent.GetComponent<HeatBarMobLineComponent>();
            _heatBarComponent.Initialize(this);
        }
    }
}