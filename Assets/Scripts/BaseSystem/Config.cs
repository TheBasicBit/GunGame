using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSystem
{
    public static class Config
    {
        public static bool DebugMode { get; set; } = true;

        public static string ServerAddress { get => DebugMode ? "127.0.0.1" : "134.255.232.43"; }
        public static int ServerPort { get; } = 19489;

        public static int ControlServerPort { get; } = 19877;
    }
}