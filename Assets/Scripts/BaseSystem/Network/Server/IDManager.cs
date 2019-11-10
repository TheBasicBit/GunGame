using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSystem.Network.Server
{
    public class IDManager
    {
        private readonly List<ulong> ids = new List<ulong>();

        public ulong CreateNewID()
        {
            ulong id = 0;
            while (ids.Contains(id))
            {
                id++;
            }
            ids.Add(id);
            return id;
        }
    }
}