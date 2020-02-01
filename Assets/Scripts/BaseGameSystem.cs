using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace BlindDeer.GameBase
{
    public static class BaseGameSystem
    {
        public static IGame Game { get; set; }

        public static GameType GameType
        {
            get
            {
                if (Game == null)
                {
                    return GameType.None;
                }
                else
                {
                    return Game.GameType;
                }
            }
        }

        public static void Main()
        {
            Logger.Log += OnLog;

            if (Game == null)
            {
                Logger.LogWarning("BlindDeer.GameBase.BaseGameSystem.Game is not defined.");
            }

            NetworkManager.Connect();

            UpdateManager.Start();

            Game?.Main();
        }

        private static void OnLog(object sender, LogEventArgs e)
        {
            LogType type = e.Type;

            if (type == LogType.Info)
            {
                Debug.Log(e.Message);
            }
            else if (type == LogType.Warning)
            {
                Debug.LogWarning(e.Message);
            }
            else if (type == LogType.Error)
            {
                Debug.LogError(e.Message);
            }
        }

        public static void OnEngineUpdate()
        {
            Game?.OnEngineUpdate();
        }

        public static void OnExit()
        {
            Game?.OnExit();
        }
    }
}