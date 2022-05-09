using System;
using Components;
using UnityEngine;

namespace Effects
{
    public class PartialLineEffectItem : MonoBehaviour
    {
        private float _rotationSpeed;
        private Timer _lifeSpanTimer;
        private FadeEffect  _fadeEffect;
        private Vector2 _direction;

        public event Action LifeSpanTimeEnded;

        public void Initialize(
            float rotationSpeed, 
            Vector2 direction,
            float lifeSpanTime)
        {
            _rotationSpeed = rotationSpeed;
            _direction = direction;
            _lifeSpanTimer = new Timer(lifeSpanTime);
            _fadeEffect = gameObject.AddComponent<FadeEffect>();

            _fadeEffect.RunEffectAction(lifeSpanTime, minimumFadePercentEffect: 0);

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

            UpdateTransform();
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