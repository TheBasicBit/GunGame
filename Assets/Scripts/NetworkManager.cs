using BlindDeer.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BlindDeer;

namespace BlindDeer.GameBase
{
    public static class NetworkManager
    {
        public static Connection Connection { get; private set; }

        public static bool Connect()
        {
            try
            {
                if (!ConnectToLocalServer())
                {
                    Connection = new Connection(new TcpClient(BlindDeerGames.SERVER_ADDRESS, BlindDeerGames.SERVER_PORT));
                    Connection.Closed += Connection_Closed;
                    Connection.Output += Connection_Output;

                    Logger.LogInfo("Connected to remote server.");
                }

                if (BaseGameSystem.GameType == GameType.None || !SendPacket(new Packet()
                {
                    [PacketField.GameType] = (int)BaseGameSystem.GameType,
                    [PacketField.PacketType] = (int)PacketType.Game
                }))
                {
                    Logger.LogError("The game_type packet could not be sent.");
                    return false;
                }

                return true;
            }
            catch
            {
                Logger.LogError("Can´t connect to server.");

                return false;
            }
        }

        public static bool ConnectToLocalServer()
        {
            try
            {
                Connection = new Connection(new TcpClient(BlindDeerGames.LOCALHOST_ADDRESS, BlindDeerGames.SERVER_PORT));
                Connection.Closed += Connection_Closed;
                Connection.Output += Connection_Output;

                Logger.LogInfo("Connected to local server.");

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void Connection_Output(object sender, ConnectionPacketOutputEventArgs e)
        {
            Packet packet = e.Packet;
            PacketType type = (PacketType)packet[PacketField.PacketType];

            if (type == PacketType.Log)
            {
                Logger.LogAny((LogType)packet[PacketField.ServerLogType], "ServerLog: " + packet[PacketField.ServerLogMessage]);
            }

            BaseGameSystem.Game.OnPacket(packet);
        }

        private static void Connection_Closed(object sender, ConnectionEventArgs e)
        {
            Logger.LogError("The connection to the server was closed.");
        }

        public static bool SendPacket(Packet packet)
        {
            if (Connection == null)
            {
                return false;
            }

            return Connection.SendPacket(packet);
        }
    }
}