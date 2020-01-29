using System;
using System.Threading;

namespace BlindDeer
{
    public static class UpdateManager
    {
        private static bool _update = false;

        private static readonly Thread _updateThread = new Thread(new ThreadStart(() =>
        {
            while (true)
            {
                if (_update)
                {
                    Update?.Invoke(null, new EventArgs());
                }
            }
        }));

        public static event EventHandler<EventArgs> Update;

        static UpdateManager()
        {
            _updateThread.Start();
        }

        public static void Start()
        {
            _update = true;
            Logger.LogInfo("UpdateManager has been started.");
        }

        public static void Stop()
        {
            _update = false;
            Logger.LogInfo("UpdateManager has been stopped.");
        }
    }
}