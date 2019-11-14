using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSystem.Network.Server
{
    public class BulletManager
    {
        public Server Server { get; }

        private readonly IDManager bulletIdManager = new IDManager();

        public BulletManager(Server server)
        {
            Server = server;
        }

        public void Update()
        {
        }

        public void Create(float posX, float posY, float posZ, float yaw, float pitch)
        {
        }
    }
}