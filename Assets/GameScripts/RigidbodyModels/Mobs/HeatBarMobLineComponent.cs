using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Utils;

namespace RigidbodyModels.Mobs
{
    public class HeatBarMobLineComponent : MonoBehaviour
    {
        [SerializeField] private Image healBarLine;
        [SerializeField] private Image armorBarLine;
        
        private MobModelBase _model;

        public void Initialize(MobModelBase model)
        {
            _model = model;
        }

        private void Start()
        {
            SetSize();
        }
        
        private void SetSize()
        {
            transform.localScale = new Vector3(30, 20, 0);
        }

        private void Update()
        {
            UpdateDrawLocation();

            UpdateHealBar();
        }

        private void UpdateDrawLocation()
        {
            float y = _model.Position.y - _model.transform.localScale.y / 2.5f;
            
            gameObject.transform.position = new Vector3(_model.Position.x, y, 0);
        }

        private void UpdateHealBar()
        {
            healBarLine.fillAmount = Helpers.GetPercentFromMax(_model.MaxHeatPoint, _model.HeatPoint);
        }
    }
}