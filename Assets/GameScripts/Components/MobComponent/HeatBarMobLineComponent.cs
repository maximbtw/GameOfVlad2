using RigidbodyModels.Mob;
using UnityEngine;
using Utils;

namespace Components.MobComponent
{
    public class HeatBarMobLineComponent : MonoBehaviour
    {
        [SerializeField] private Transform healBarLine;
        [SerializeField] private Transform armorBarLine;
        [SerializeField] private SpriteRenderer healBarLineSpriteRenderer;
        [SerializeField] private SpriteRenderer armorBarLineSpriteRenderer;
        
        private int _maxArmor;
        private MobModelBase _model;
        private RectTransform _rectTransform;
        private Timer _switchingColorTimer;

        private static readonly Color BaseHealBarColor = new Color(8588235f, 0.1254902f, 0.1764706f);
        private static readonly Color LowPercentHealBarColor = new Color(1, 1, 1);
        private static readonly Color BaseArmorBarColor = new Color(0.4588235f, 0.4705882f, 0.5686275f);
        
        private const float PercentHealPointToShowAnimation = 0.3f;

        private void Start()
        {
            SetSize();

            _rectTransform = GetComponent<RectTransform>();
            
            _switchingColorTimer = new Timer(countdownTime: 0.3f);

            healBarLineSpriteRenderer.color = BaseHealBarColor;
            armorBarLineSpriteRenderer.color = BaseArmorBarColor;
        }

        private void Update()
        {
            UpdateDrawLocation();
            UpdateHealBar();
            UpdateArmorBar();
        }

        public void Initialize(MobModelBase model)
        {
            _model = model;
            _maxArmor = model.Armor;
        }

        private void SetSize()
        {
            const float fixedSize = 3;

            transform.localScale = new Vector3(_model.Size.x * fixedSize, _model.Size.x * fixedSize, 0);
        }

        private void UpdateDrawLocation()
        {
            float y = -(_model.Size.y * 100 / 2);

            _rectTransform.anchoredPosition3D = new Vector3(0, y, 0);
        }

        private void UpdateHealBar()
        {
            float percentHealPoint = Helpers.GetPercentFromMax(_model.MaxHeatPoint, _model.HeatPoint);

            healBarLine.localScale = new Vector3(percentHealPoint, healBarLine.localScale.y);

            if (percentHealPoint <= PercentHealPointToShowAnimation)
            {
                if (!_switchingColorTimer.IsActive)
                {
                    healBarLineSpriteRenderer.color = healBarLineSpriteRenderer.color == BaseHealBarColor
                        ? LowPercentHealBarColor
                        : BaseHealBarColor;
                    
                    _switchingColorTimer.Start();
                }
                
                _switchingColorTimer.Update();
            }
        }

        private void UpdateArmorBar()
        {
            int currentArmor = _model.Armor < 0 ? 0 : _model.Armor;

            if (currentArmor > _maxArmor)
            {
                _maxArmor = currentArmor;
            }

            float percentArmorBar = Helpers.GetPercentFromMax(_maxArmor, currentArmor);

            armorBarLine.localScale = new Vector3(percentArmorBar, armorBarLine.localScale.y);
        }
    }
}