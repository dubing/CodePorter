using System;
using System.Diagnostics;

namespace CodePorter.Utility
{


    [Serializable]
    internal class LogObject
    {
        public int EventId;
        public EventLogEntryType LogType;
        public string Message;

        public LogObject(string message, EventLogEntryType logType, int eventId)
        {
            this.Message = message;
            this.LogType = logType;
            this.EventId = eventId;
        }
    }
}

