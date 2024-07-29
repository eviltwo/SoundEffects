using UnityEngine;

namespace SoundEffects
{
    public interface ISoundEffectPlayer
    {
        void PlayOneShot(in SoundEffectPlayRequest request);

        void PlayOneShot(in SoundEffectPlayRequest request, Vector3 worldPosition);

        void PlayOneShot(in SoundEffectPlayRequest request, Transform parent, Vector3 localPosition);
    }
}
