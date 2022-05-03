using UnityEngine;

namespace Shared
{
    public class AudioControllerBase : MonoBehaviour
    {
        protected AudioSource AudioSource;

        protected virtual void Start()
        {
            LoadAudionSource();
        }

        protected void PlayOneShotIfNotNull(AudioClip audioClip)
        {
            if (audioClip != null)
            {
                AudioSource.PlayOneShot(audioClip);
            }
        }

        private void LoadAudionSource()
        {
            AudioSource = GetComponent<AudioSource>();

            if (AudioSource == null)
            {
                AudioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }
}