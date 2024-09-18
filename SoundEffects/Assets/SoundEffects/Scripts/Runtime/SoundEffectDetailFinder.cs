using UnityEngine;

namespace SoundEffects
{
    public abstract class SoundEffectDetailFinder : ScriptableObject
    {
        public abstract bool FindDetail(string name, out SoundEffectDetail detail);
    }
}
