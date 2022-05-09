using Components;
using UnityEngine;

namespace Effects
{
    public class FadeEffect : MonoBehaviour
    {
        [SerializeField] private float fadeOutTime;
        [SerializeField] private float minimumFadePercent;
        [SerializeField] private bool active;

        private SpriteRenderer _spriteRenderer;
        private Timer _timer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _timer = new Timer(this.fadeOutTime);
            _timer.Start();
        }

        public void RunEffectAction(float? fadeOutTimeEffect = null, float? minimumFadePercentEffect = null)
        {
            this.fadeOutTime = fadeOutTimeEffect ?? this.fadeOutTime;
            this.minimumFadePercent = minimumFadePercentEffect ?? this.minimumFadePercent;
            
            _timer = new Timer(this.fadeOutTime);
            _timer.Start();

            active = true;
        }

        private void Update()
        {
            if (!active)
            {
                return;
            }
            
            _timer.Update();
            
            Color currentColor = _spriteRenderer.color;

            float transparency = _timer.TimeLeft / _timer.CountdownTime + minimumFadePercent;

            _spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, transparency);

            if (!_timer.IsActive)
            {
                _timer = new Timer(fadeOutTime);
            }
        }
    }
}