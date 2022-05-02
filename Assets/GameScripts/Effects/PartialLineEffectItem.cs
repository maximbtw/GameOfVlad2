using System;
using Components;
using UnityEngine;

namespace Effects
{
    public class PartialLineEffectItem : MonoBehaviour
    {
        private float _rotationSpeed;
        private Timer _lifeSpanTimer;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _direction;

        public event Action LifeSpanTimeEnded;

        public void Initialize(
            SpriteRenderer spriteRenderer, 
            float rotationSpeed, 
            Vector2 direction,
            float lifeSpanTime)
        {
            _spriteRenderer = spriteRenderer;
            _rotationSpeed = rotationSpeed;
            _direction = direction;
            _lifeSpanTimer = new Timer(lifeSpanTime);

            _lifeSpanTimer.Ended += LifeHasEnded;
            _lifeSpanTimer.Start();
        }

        private void Update()
        {
            if (_lifeSpanTimer == null)
            {
                return;
            }
            
            _lifeSpanTimer.Update();
            
            UpdateTransparency();
            UpdateTransform();
        }

        private void UpdateTransparency()
        {
            Color currentColor = _spriteRenderer.color;

            float transparency = _lifeSpanTimer.TimeLeft / _lifeSpanTimer.CountdownTime;

            _spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, transparency);
        }

        private void UpdateTransform()
        {
            Transform currentTransform = transform;
            
            currentTransform.position += new Vector3(_direction.x, _direction.y, z: 0);
            transform.Rotate(xAngle: 0, yAngle: 0, zAngle: currentTransform.rotation.z + _rotationSpeed);
        }

        private void LifeHasEnded()
        {
            LifeSpanTimeEnded?.Invoke();
            
            Destroy(this.gameObject);
        }
    }
}