using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Effects
{
    public class PartialLineEffect : MonoBehaviour
    {
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private List<Gradient> gradients;
        
        [SerializeField] [Range(1, 100)] private int frequency;
        [SerializeField] [Range(0, 5)] private float dispersion;

        [Header("Speed rotation")]
        [SerializeField] [Range(0, 100)] private float minimumSpeedRotation;
        [SerializeField] [Range(0, 100)] private float maximumSpeedRotation;

        [Header("Life span")]
        [SerializeField] [Range(0.01f, 10)] private float minimumLifeSpanInSecond = 1;
        [SerializeField] [Range(0.01f, 10)] private float maximumLifeSpanInSecond = 1;

        [Header("Scale")]
        [SerializeField] [Range(0.01f, 2)] private float minimumScale = 1;
        [SerializeField] [Range(0.01f, 2)] private float maximumScale = 1;

        private Transform _transform;
        private int _countItems;

        private float NormalizeDispersion => dispersion / 100;

        private void Start()
        {
            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            for (var i = 0; i < frequency; i++)
            {
                CreateItem();
            }
        }

        private void CreateItem()
        {
            if (!sprites.Any() || !gradients.Any())
            {
                return;
            }

            _countItems++;
            
            float scale = Random.Range(minimumScale, maximumScale);
            float lifeSpan = Random.Range(minimumLifeSpanInSecond, maximumLifeSpanInSecond);
            float speedRotation = Random.Range(minimumSpeedRotation, maximumSpeedRotation);
            
            var forceDirection = new Vector2(
                x: Random.Range(-NormalizeDispersion, NormalizeDispersion),
                y: Random.Range(-NormalizeDispersion, NormalizeDispersion));
            
            Sprite sprite = sprites[Random.Range(0, sprites.Count)];
            Color color = gradients[Random.Range(0, gradients.Count)].GetColor();

            var item = new GameObject(nameof(PartialLineEffectItem) + _countItems);

            var itemTransform = item.GetComponent<Transform>();
            var itemsSpriteRenderer = item.AddComponent<SpriteRenderer>();
            var itemBody = item.AddComponent<PartialLineEffectItem>();

            itemsSpriteRenderer.sprite = sprite;
            itemsSpriteRenderer.color = color;

            itemTransform.position = _transform.position;
            itemTransform.localScale = new Vector3(scale, scale);

            itemBody.LifeSpanTimeEnded += () => _countItems--;

            itemBody.Initialize(itemsSpriteRenderer, speedRotation, forceDirection, lifeSpan);
        }

        [Serializable]
        public class Gradient
        {
            [SerializeField] private Color firstColor;

            [SerializeField] private Color secondColor;

            public Color GetColor()
            {
                if (firstColor.Equals(secondColor))
                {
                    return firstColor;
                }
                
                float r = GetRandom(this.firstColor.r, this.secondColor.r);
                float g = GetRandom(this.firstColor.g, this.secondColor.g);
                float b = GetRandom(this.firstColor.b, this.secondColor.b);

                return new Color(r, g, b);
            }

            private float GetRandom(float color1, float color2)
            {
                float item1 = color1 > color2 ? color1 : color2;
                float item2 = color1 < color2 ? color1 : color2;

                return Random.Range(item1, item2);
            }
        }
    }
}