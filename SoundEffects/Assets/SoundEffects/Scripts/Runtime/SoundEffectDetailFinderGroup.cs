using UnityEngine;

namespace SoundEffects
{
    [CreateAssetMenu(fileName = "SoundEffectDetailFinderGroup", menuName = "SoundEffects/DetailFinderGroup")]
    public class SoundEffectDetailFinderGroup : SoundEffectDetailFinder
    {
        [SerializeField]
        private SoundEffectDetailFinder[] _finders = default;

        public override bool FindDetail(string name, out SoundEffectDetail detail)
        {
            var count = _finders.Length;
            for (var i = 0; i < count; i++)
            {
                if (_finders[i].FindDetail(name, out detail))
                {
                    return true;
                }
            }
            detail = null;
            return false;
        }
    }
}
