using SoundEffects;
using UnityEngine;

public class LoopSample : MonoBehaviour
{
    [SerializeField]
    private float _interval = 1.0f;

    [SerializeField]
    private string[] _soundKeys = default;

    private float _elapsedTime;
    private int _nextIndex;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _interval)
        {
            _elapsedTime = 0;
            PlaySound(_soundKeys[_nextIndex]);
            _nextIndex = (_nextIndex + 1) % _soundKeys.Length;
        }
    }

    private void PlaySound(string key)
    {
        var request = SoundEffectPlayRequest.Default;
        request.Name = key;
        SoundEffectManager.Player.PlayOneShot(request);
    }
}
