using UnityEngine;
using UnityEngine.Serialization;

namespace SoundEffects
{
    [CreateAssetMenu(fileName = "SoundEffectDetail", menuName = "SoundEffects/Detail")]
    public class SoundEffectDetail : ScriptableObject
    {
        [SerializeField]
        private AudioClipDetail[] _audioClipDetails = new AudioClipDetail[]
        {
            new AudioClipDetail
            {
                AudioClip = null,
                Weight = 1,
                Volume = 1,
                MinPitch = 1,
                MaxPitch = 1,
            }
        };
        public AudioClipDetail[] AudioClipDetails => _audioClipDetails;

        [SerializeField]
        private float _spatialBlend = 1.0f;
        public float SpatialBlend => _spatialBlend;

        [SerializeField]
        private float _minDistance = 1.0f;
        public float MinDistance => _minDistance;

        [SerializeField]
        private float _maxDistance = 100.0f;
        public float MaxDistance => _maxDistance;
    }

    [System.Serializable]
    public class AudioClipDetail
    {
        [FormerlySerializedAs("_audioClip")]
        public AudioClip AudioClip;

        [FormerlySerializedAs("_weight")]
        public float Weight;

        [FormerlySerializedAs("_volume")]
        public float Volume;

        [FormerlySerializedAs("_minPitch")]
        public float MinPitch;

        [FormerlySerializedAs("_maxPitch")]
        public float MaxPitch;
    }
}
