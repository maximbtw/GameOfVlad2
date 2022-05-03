using Components;
using UnityEngine;

namespace Shared
{
    public class TransparencySpriteController
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly Timer _timer;
        
        public TransparencySpriteController(SpriteRenderer spriteRenderer, Timer timer)
        {
            _spriteRenderer = spriteRenderer;
            _timer = timer;
        }

        public void Update()
        {
            Color currentColor = _spriteRenderer.color;

            float transparency = _timer.TimeLeft / _timer.CountdownTime;

            _spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, transparency);
        }
    }
}