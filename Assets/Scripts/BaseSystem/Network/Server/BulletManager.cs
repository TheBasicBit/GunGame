using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSystem.Network.Server
{
    public class BulletManager
    {
        private readonly IDManager bulletIdManager = new IDManager();
        private readonly List<ServerSideBullet> bullets = new List<ServerSideBullet>();

        public Server Server { get; }

        public BulletManager(Server server)
        {
            Server = server;
        }

        public void Update()
        {
        }

        public ServerSideBullet Create(float posX, float posY, float posZ, float yaw, float pitch)
        {
            ServerSideBullet serverSideBullet = new ServerSideBullet(bulletIdManager.CreateNewID());
            bullets.Add(serverSideBullet);
            return serverSideBullet;
        }
    }
}