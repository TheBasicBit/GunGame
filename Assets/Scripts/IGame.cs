using BlindDeer.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlindDeer.GameBase
{
    public interface IGame
    {
        GameHolder GameHolder { get; }

        GameType GameType { get; }

        void Main();

        void OnEngineUpdate();

        void OnPacket(Packet packet);

        void OnExit();
    }
}