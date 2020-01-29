using System;

namespace BlindDeer
{
    public static class Logger
    {
        public static event EventHandler<LogEventArgs> Log;

        public static void LogInfo(object obj)
        {
            LogAny(LogType.Info, obj);
        }

        public static void LogWarning(object obj)
        {
            LogAny(LogType.Warning, obj);
        }

        public static void LogError(object obj)
        {
            LogAny(LogType.Error, obj);
        }

        public static void LogAny(LogType type, object obj)
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            if (type == LogType.Error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (type == LogType.Warning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            Console.WriteLine("[" + DateTime.Now.ToString("dd.MM.yy HH:mm:ss") + "]: " + obj);
            Log?.Invoke(null, new LogEventArgs(obj.ToString(), type));
        }
    }
}