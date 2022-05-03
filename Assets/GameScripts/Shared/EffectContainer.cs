using UnityEngine;

namespace Shared
{
    public class EffectContainer : MonoBehaviour
    {
        public static EffectContainer Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}