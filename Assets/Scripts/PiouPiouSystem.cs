using BlindDeer.GameBase;
using BlindDeer.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlindDeer.Game.PiouPiou
{
    public class PiouPiouSystem : IGame
    {
        public GameHolder GameHolder { get; set; }

        public GameType GameType => GameType.PiouPiou;

        public void Main()
        {
        }

        public void OnEngineUpdate()
        {
        }

        public void OnPacket(Packet packet)
        {
        }
    }
}