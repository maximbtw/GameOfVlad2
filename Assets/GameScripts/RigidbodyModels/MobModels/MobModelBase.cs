using Components.MobComponent;
using RigidbodyModels.PlayerModel;
using RigidbodyModels.Projectiles;
using UnityEditor;
using UnityEngine;

namespace RigidbodyModels.MobModels
{
    public class MobModelBase : RigidbodyModelBase
    {
        [Space]
        [SerializeField] [Range(1, 100000)] private int maxHeatPoint = 10;
        [SerializeField] [Range(0, 100000)] private int damage = 1;
        [SerializeField] private int armor = 1;

        private HeatBarMobLineComponent _heatBarComponent;
        private Player _player;

        protected Vector2 TargetPosition => _player.Position;

        public int MaxHeatPoint => maxHeatPoint;
        public int HeatPoint { get; private set; }

        public int Armor => armor;
        public int Damage => damage;

        //public MobModelBase Parent { get; }

        private void Awake()
        {
            LoadHealBarLineComponent();

            HeatPoint = maxHeatPoint;

            // TODO:
            _player = FindObjectOfType<Player>();
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