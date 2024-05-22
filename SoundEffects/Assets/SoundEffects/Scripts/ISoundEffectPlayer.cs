using UnityEngine;

namespace SoundEffects
{
    public interface ISoundEffectPlayer
    {
        void PlayOneShot(string name, float volume);

        void PlayOneShot(string name, float volume, Vector3 worldPosition);

        void PlayOneShot(string name, float volume, Transform parent, Vector3 localPosition);
    }
}
