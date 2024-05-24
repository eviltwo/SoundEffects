using UnityEngine;

namespace SoundEffects
{
    public class SoundEffectTrigger : MonoBehaviour
    {
        [SerializeField]
        public string SoundEffectName = string.Empty;

        [SerializeField]
        public float Volume = 1.0f;

        public void Play()
        {
            SoundEffectManager.Player?.PlayOneShot(SoundEffectName, Volume);
        }

        public void Play3D()
        {
            SoundEffectManager.Player?.PlayOneShot(SoundEffectName, Volume, transform.position);
        }
    }
}
