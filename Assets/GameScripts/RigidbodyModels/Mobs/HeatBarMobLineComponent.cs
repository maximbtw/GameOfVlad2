using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace RigidbodyModels.Mobs
{
    public class HeatBarMobLineComponent : MonoBehaviour
    {
        [SerializeField] private Image healBarLine;
        [SerializeField] private Image armorBarLine;

        private RectTransform _rectTransform;
        private MobModelBase _model;
        private int _maxArmor;

        public void Initialize(MobModelBase model)
        {
            _model = model;
            _maxArmor = model.Armor;
        }

        private void Start()
        {
            SetSize();

            _rectTransform = GetComponent<RectTransform>();
        }

        private void SetSize()
        {
            float x = _model.Size.x * 100;
            float y = _model.Size.y * 100 / 2 - 20;

            transform.localScale = new Vector3(x, y, 0);
        }

        private void Update()
        {
            UpdateDrawLocation();

            UpdateHealBar();
            UpdateArmorBar();
        }

        private void UpdateDrawLocation()
        {
            float x = _model.Size.x * 100 / 2;
            float y = -(_model.Size.y * 100 / 2);

            _rectTransform.anchoredPosition3D =new Vector3(x, y, 0);
        }

        private void UpdateHealBar()
        {
            healBarLine.fillAmount = Helpers.GetPercentFromMax(_model.MaxHeatPoint, _model.HeatPoint);
        }

        private void UpdateArmorBar()
        {
            int currentArmor = _model.Armor < 0 ? 0 : _model.Armor;

            if (currentArmor > _maxArmor)
            {
                _maxArmor = currentArmor;
            }

            armorBarLine.fillAmount = Helpers.GetPercentFromMax(_maxArmor, currentArmor);
        }
    }
}