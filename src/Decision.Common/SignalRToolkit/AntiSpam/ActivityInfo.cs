using System;

namespace NTierMvcFramework.Common.SignalRToolkit.AntiSpam
{
    public class ActivityInfo
    {
        public ActivityInfo(string connectionId)
        {
            ConnectionId = connectionId;
            Time = DateTime.Now;
        }
        public string ConnectionId { get; set; }

        public DateTime Time { get; set; }
    }
}