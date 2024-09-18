using UnityEngine;

namespace SoundEffects
{
    [CreateAssetMenu(fileName = "SoundEffectDetail", menuName = "SoundEffects/Detail")]
    public class SoundEffectDetail : ScriptableObject
    {
        [SerializeField]
        private AudioClipDetail[] _audioClipDetails = null;
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
        [SerializeField]
        private AudioClip _audioClip = null;
        public AudioClip AudioClip => _audioClip;

        [SerializeField]
        private float _weight = 1.0f;
        public float Weight => _weight;

        [SerializeField]
        private float _volume = 1.0f;
        public float Volume => _volume;

        [SerializeField]
        private float _minPitch = 1.0f;
        public float MinPitch => _minPitch;

        [SerializeField]
        private float _maxPitch = 1.0f;
        public float MaxPitch => _maxPitch;
    }
}
