using UnityEngine;

namespace SoundEffects
{
    public class SoundEffectTrigger : MonoBehaviour
    {
        [SerializeField]
        public SoundEffectPlayRequest Request = SoundEffectPlayRequest.Default;

        public void Play()
        {
            SoundEffectManager.Player?.PlayOneShot(Request);
        }

        public void Play3D()
        {
            SoundEffectManager.Player?.PlayOneShot(Request, transform.position);
        }
    }
}
