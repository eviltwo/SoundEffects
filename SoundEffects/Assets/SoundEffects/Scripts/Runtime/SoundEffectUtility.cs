namespace SoundEffects
{
    public static class SoundEffectUtility
    {
        public static int ChooseAudioClip(AudioClipDetail[] audioClipDetails, System.Random random)
        {
            var totalWeight = 0f;
            for (var i = 0; i < audioClipDetails.Length; i++)
            {
                totalWeight += audioClipDetails[i].Weight;
            }

            var randomValue = random.NextDouble() * totalWeight;
            for (var i = 0; i < audioClipDetails.Length; i++)
            {
                if (randomValue < audioClipDetails[i].Weight)
                {
                    return i;
                }

                randomValue -= audioClipDetails[i].Weight;
            }

            return audioClipDetails.Length - 1;
        }
    }
}
