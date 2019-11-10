using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSystem
{
    public static class Config
    {
        public const bool DebugMode = false;

        public const string ServerAddress = DebugMode ? "127.0.0.1" : "134.255.232.43";
        public const int ServerPort = 19489;
    }
}