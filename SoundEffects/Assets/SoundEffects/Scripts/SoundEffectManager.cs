using System.Collections.Generic;
using System.Linq;

namespace SoundEffects
{
    public static class SoundEffectManager
    {
        private static List<ISoundEffectPlayer> _players = new List<ISoundEffectPlayer>();

        public static void Bind(ISoundEffectPlayer player)
        {
            _players.Add(player);
        }

        public static void Unbind(ISoundEffectPlayer player)
        {
            _players.Remove(player);
        }

        public static ISoundEffectPlayer Player
        {
            get
            {
                return _players.FirstOrDefault();
            }
        }
    }
}

