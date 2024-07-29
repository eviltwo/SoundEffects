using System;

namespace SoundEffects
{
    [Serializable]
    public struct SoundEffectPlayRequest
    {
        /// <summary>
        /// Sound name.
        /// </summary>
        public string Name;

        /// <summary>
        /// Sound volume.
        /// </summary>
        public float Volume;

        /// <summary>
        /// Delay time until the sound is played (seconds).
        /// </summary>
        public float Delay;

        public SoundEffectPlayRequest(string name, float volume, float delay)
        {
            Name = name;
            Volume = volume;
            Delay = delay;
        }

        public static SoundEffectPlayRequest Default => new SoundEffectPlayRequest(string.Empty, 1.0f, 0.0f);
    }
}
