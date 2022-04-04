using UnityEngine;

namespace Utils
{
    public static class Helpers
    {
        public static float GetAngleFromDirection(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;
            
            if (angle < 0)
            {
                angle += 360;
            }
            return angle;
        }
    }
}