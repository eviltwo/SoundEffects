using System.Collections.Generic;
using UnityEngine;
namespace SoundEffects
{
    public class SoundPlayerComponent : MonoBehaviour, ISoundEffectPlayer
    {
        [SerializeField]
        private SoundEffectDetailFinder _soundEffectDetailFinder = null;

        [SerializeField]
        private AudioSource _defaultAudioSource = null;

        [SerializeField]
        private int _defaultPoolSize = 16;

        private readonly System.Random _random = new System.Random();
        private AudioSourcePool _audioSourcePool;
        private readonly List<SoundEffectHolder> _playingAudioSources = new List<SoundEffectHolder>();
        private Transform _audioListener;

        private void OnEnable()
        {
            SoundEffectManager.Bind(this);
        }

        private void OnDisable()
        {
            SoundEffectManager.Unbind(this);
        }

        private void Awake()
        {
            _audioSourcePool = new AudioSourcePool(_defaultAudioSource, transform, _defaultPoolSize);
        }

        public void PlayOneShot(in SoundEffectPlayRequest request)
        {
            PlayOneShotImpl(request, false, null, Vector3.zero);
        }

        public void PlayOneShot(in SoundEffectPlayRequest request, Vector3 worldPosition)
        {
            PlayOneShotImpl(request, true, null, worldPosition);
        }

        public void PlayOneShot(in SoundEffectPlayRequest request, Transform parent, Vector3 localPosition)
        {
            PlayOneShotImpl(request, true, parent, localPosition);
        }

        private void PlayOneShotImpl(in SoundEffectPlayRequest request, bool is3d, Transform parent, Vector3 localPosition)
        {
            if (_soundEffectDetailFinder.FindDetail(request.Name, out var detail))
            {
                var index = SoundEffectUtility.ChooseAudioClip(detail.AudioClipDetails, _random);
                var audioClipDetail = detail.AudioClipDetails[index];
                var spatialBlend = is3d ? detail.SpatialBlend : 0;
                is3d |= detail.SpatialBlend > 0;

                // Skip far sound
                if (is3d && _audioListener != null)
                {
                    var position = parent == null ? localPosition : parent.TransformPoint(localPosition);
                    var sqrDist = Vector3.SqrMagnitude(position - _audioListener.position);
                    var distanceThreshold = Mathf.Lerp(detail.MinDistance, detail.MaxDistance, request.Volume);
                    if (sqrDist > distanceThreshold * distanceThreshold)
                    {
                        return;
                    }
                }

                var audioSource = _audioSourcePool.GetOrCreate();
                audioSource.name = $"AudioSource_{request.Name}";
                audioSource.clip = audioClipDetail.AudioClip;
                audioSource.volume = audioClipDetail.Volume * request.Volume;
                audioSource.pitch = Mathf.Lerp(audioClipDetail.MinPitch, audioClipDetail.MaxPitch, (float)_random.NextDouble());
                audioSource.spatialBlend = spatialBlend;
                audioSource.minDistance = detail.MinDistance;
                audioSource.maxDistance = detail.MaxDistance;
                if (request.Delay == 0)
                {
                    audioSource.Play();
                }
                else
                {
                    audioSource.PlayDelayed(request.Delay);
                }
                var holder = new SoundEffectHolder(audioSource, parent, localPosition);
                _playingAudioSources.Add(holder);
            }
        }

        private void Update()
        {
            if (_audioListener == null)
            {
                _audioListener = Camera.main?.transform;
            }

            var count = _playingAudioSources.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                var holder = _playingAudioSources[i];
                holder.UpdatePosition();
                if (!holder.AudioSource.isPlaying)
                {
                    _audioSourcePool.Return(holder.AudioSource);
                    _playingAudioSources.RemoveAt(i);
                }
            }
        }
    }

    internal class SoundEffectHolder
    {
        public readonly AudioSource AudioSource;
        public readonly Transform Parent;
        public readonly Vector3 LocalPosition;
        public SoundEffectHolder(AudioSource audioSource, Transform parent, Vector3 localPosition)
        {
            AudioSource = audioSource;
            Parent = parent;
            LocalPosition = localPosition;
        }

        public void UpdatePosition()
        {
            if (Parent == null)
            {
                AudioSource.transform.position = LocalPosition;
            }
            else
            {
                AudioSource.transform.position = Parent.TransformPoint(LocalPosition);
            }
        }
    }

    internal class AudioSourcePool
    {
        private readonly AudioSource _defaultAudioSource;
        private readonly Transform _parent;
        private readonly Queue<AudioSource> _pooledAudioSources = new Queue<AudioSource>();

        public int Size => _pooledAudioSources.Count;

        public AudioSourcePool(AudioSource defaultAudioSource, Transform parent, int defaultPoolSize)
        {
            _defaultAudioSource = defaultAudioSource;
            _parent = parent;

            _defaultAudioSource.gameObject.SetActive(false);
            for (int i = 0; i < defaultPoolSize; i++)
            {
                var instance = CreateInstance();
                instance.gameObject.SetActive(false);
                _pooledAudioSources.Enqueue(instance);
            }
        }

        public AudioSource GetOrCreate()
        {
            AudioSource instance;
            if (_pooledAudioSources.Count == 0)
            {
                instance = CreateInstance();
            }
            else
            {
                instance = _pooledAudioSources.Dequeue();
            }
            instance.gameObject.SetActive(true);
            return instance;
        }

        public void Return(AudioSource audioSource)
        {
            audioSource.clip = null;
            audioSource.gameObject.SetActive(false);
            audioSource.transform.SetParent(_parent);
            _pooledAudioSources.Enqueue(audioSource);
        }

        private AudioSource CreateInstance()
        {
            var instance = Object.Instantiate(_defaultAudioSource, _parent);
            instance.gameObject.SetActive(false);
            return instance;
        }
    }
}
