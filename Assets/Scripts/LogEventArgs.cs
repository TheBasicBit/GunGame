using System;
using System.Collections.Generic;
using System.Text;

namespace BlindDeer
{
    public class LogEventArgs : EventArgs
    {
        public string Message { get; }

        public LogType Type { get; }

        public LogEventArgs(string msg, LogType type)
        {
            Message = msg;
            Type = type;
        }
    }
}