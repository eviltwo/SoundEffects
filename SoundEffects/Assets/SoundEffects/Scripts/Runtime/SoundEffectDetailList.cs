using UnityEngine;

namespace SoundEffects
{
    [CreateAssetMenu(fileName = "SoundEffectDetailList", menuName = "SoundEffects/DetailList")]
    public class SoundEffectDetailList : SoundEffectDetailFinder
    {
        [Header("Details")]
        [SerializeField]
        private SoundEffectDetail[] _soundEffectDetails = null;
        public SoundEffectDetail[] SoundEffectDetails => _soundEffectDetails;

        [Header("Auto Collect")]
        [SerializeField]
        private string[] _autoCollectDirectories = null;
        public string[] AutoCollectDirectories => _autoCollectDirectories;

        public override bool FindDetail(string name, out SoundEffectDetail detail)
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
