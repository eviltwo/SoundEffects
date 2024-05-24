using UnityEngine;

namespace SoundEffects
{
    [CreateAssetMenu(fileName = "SoundEffectDetailList", menuName = "SoundEffects/SoundEffectDetailList")]
    public class SoundEffectDetailList : ScriptableObject
    {
        [Header("Filter")]
        [SerializeField]
        private string[] _filterPaths = null;
        public string[] FilterPaths => _filterPaths;

        [Header("Elements")]
        [SerializeField]
        private SoundEffectDetail[] _soundEffectDetails = null;
        public SoundEffectDetail[] SoundEffectDetails => _soundEffectDetails;

        public bool TryGetItem(string name, out SoundEffectDetail detail)
        {
            var count = _soundEffectDetails.Length;
            for (var i = 0; i < count; i++)
            {
                var item = _soundEffectDetails[i];
                if (item.name == name)
                {
                    detail = item;
                    return true;
                }
            }

            detail = null;
            return false;
        }
    }
}
