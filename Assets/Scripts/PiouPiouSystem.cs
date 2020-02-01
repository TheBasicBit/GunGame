using BlindDeer.GameBase;
using BlindDeer.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlindDeer.Game.PiouPiou
{
    public class PiouPiouSystem : IGame
    {
        public GameHolder GameHolder { get; set; }

        public GameType GameType => GameType.PiouPiou;

        public Player Player { get; set; }

        public PlayerCamera PlayerCamera { get; set; }

        public GameObject OtherPlayerPrefab { get; set; }

        public GameObject BulletPrefab { get; set; }

        public Stopwatch MovementPacketTimer { get; } = new Stopwatch();

        public List<Action> SyncActions { get; } = new List<Action>();

        public List<ulong> UsedClientIds { get; } = new List<ulong>();

        public ulong Id { get; set; }

        public void Main()
        {
            MovementPacketTimer.Start();
        }

        public void OnEngineUpdate()
        {
            while (SyncActions.Count != 0)
            {
                SyncActions[SyncActions.Count - 1].Invoke();
                SyncActions.RemoveAt(SyncActions.Count - 1);
            }

            if (MovementPacketTimer.Elapsed.TotalSeconds > 0.016)
            {
                MovementPacketTimer.Restart();

                Vector3 pos = Player.transform.position;
                NetworkManager.SendPacket(new Packet()
                {
                    [PacketField.PacketType] = (int)PacketType.PlayerPosition,
                    [PacketField.PlayerPosX] = pos.x,
                    [PacketField.PlayerPosY] = pos.y,
                    [PacketField.PlayerPosZ] = pos.z
                });
            }
        }

        public void RunSync(Action action)
        {
            SyncActions.Add(action);
        }

        public void OnPacket(Packet packet)
        {
            RunSync(new Action(() =>
            {
                PacketType type = (PacketType)packet[PacketField.PacketType];

                if (type == PacketType.Id)
                {
                    Id = (ulong)packet[PacketField.YourId];
                    Logger.LogInfo("ID: " + Id);
                }

                if (type == PacketType.PlayerPosition)
                {
                    ulong id = (ulong)packet[PacketField.PlayerId];

                    if (OtherPlayer.OtherPlayers.ContainsKey((ulong)packet[PacketField.PlayerId]))
                    {
                        OtherPlayer player = OtherPlayer.OtherPlayers[id];
                        Vector3 pos = player.gameObject.transform.position;

                        if (packet.Contains(PacketField.PlayerPosX))
                        {
                            pos = new Vector3((float)packet[PacketField.PlayerPosX], pos.y, pos.z);
                        }

                        if (packet.Contains(PacketField.PlayerPosY))
                        {
                            pos = new Vector3(pos.x, (float)packet[PacketField.PlayerPosY], pos.z);
                        }

                        if (packet.Contains(PacketField.PlayerPosZ))
                        {
                            pos = new Vector3(pos.x, pos.y, (float)packet[PacketField.PlayerPosZ]);
                        }

                        player.MoveTo(pos, player.gameObject.transform.eulerAngles, 0.05f);
                    }
                    else if (!UsedClientIds.Contains(id))
                    {
                        UsedClientIds.Add(id);
                        Vector3 pos = new Vector3(0, 0, 0);

                        if (packet.Contains(PacketField.PlayerPosX))
                        {
                            pos = new Vector3((float)packet[PacketField.PlayerPosX], pos.y, pos.z);
                        }

                        if (packet.Contains(PacketField.PlayerPosY))
                        {
                            pos = new Vector3(pos.x, (float)packet[PacketField.PlayerPosY], pos.z);
                        }

                        if (packet.Contains(PacketField.PlayerPosZ))
                        {
                            pos = new Vector3(pos.x, pos.y, (float)packet[PacketField.PlayerPosZ]);
                        }

                        SpawnPlayer(id, pos, new Vector3(0, 0, 0));
                    }
                }

                if (type == PacketType.Shoot)
                {
                    SpawnBullet((ulong)packet[PacketField.PlayerId], (float)packet[PacketField.ShootPosX], (float)packet[PacketField.ShootPosY], (float)packet[PacketField.ShootPosZ], (float)packet[PacketField.ShootRotX], (float)packet[PacketField.ShootRotY], (float)packet[PacketField.ShootRotZ]);
                }
            }));
        }

        public void SpawnPlayer(ulong id, Vector3 pos, Vector3 rot)
        {
            GameHolder.CreateObject(OtherPlayerPrefab, pos, rot).GetComponent<OtherPlayer>().id = id;
        }

        public void SpawnBullet(ulong clientId, float posX, float posY, float posZ, float rotX, float rotY, float rotZ)
        {
            Vector3 startPos = new Vector3(posX, posY, posZ);
            GameObject obj = GameHolder.CreateObject(BulletPrefab, startPos, new Vector3(rotX, rotY, rotZ));
            Bullet bullet = obj.GetComponent<Bullet>();
            bullet.startPosition = startPos;
            bullet.shooterId = clientId;
        }

        public void OnExit()
        {
            NetworkManager.Connection.Dispose();
        }
    }
}