using UnityEngine;
using Utils;

namespace RigidbodyModels.Mobs
{
    public class HeatBarMobLineComponent : MonoBehaviour
    {
        [SerializeField] private Transform healBarLine;
        [SerializeField] private Transform armorBarLine;
        private int _maxArmor;
        private MobModelBase _model;

        private RectTransform _rectTransform;

        private void Start()
        {
            SetSize();

            _rectTransform = GetComponent<RectTransform>();
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
            float lineX = Helpers.GetPercentFromMax(_model.MaxHeatPoint, _model.HeatPoint);

            healBarLine.localScale = new Vector3(lineX, healBarLine.localScale.y);
        }

        private void UpdateArmorBar()
        {
            int currentArmor = _model.Armor < 0 ? 0 : _model.Armor;

            if (currentArmor > _maxArmor) _maxArmor = currentArmor;

            float lineX = Helpers.GetPercentFromMax(_maxArmor, currentArmor);

            armorBarLine.localScale = new Vector3(lineX, armorBarLine.localScale.y);
        }
    }
}