using System;
using System.Collections.Generic;

namespace Decision.Common.Noty
{
    [Serializable]
    public class Noty
    {
        public const string TempDataKey = "TempDataNotyAlerts";
        private IList<NotyMessage> _notyMessages;
        public bool DismissQueue { get; set; }
        public int MaxVisibleForQueue { get; set; }

        public IList<NotyMessage> NotyMessages
        {
            get { return _notyMessages; }
            set { _notyMessages = value; }
        }

        public Noty()
        {
            NotyMessages = new List<NotyMessage>();
            MaxVisibleForQueue = 20;
        }
        public NotyMessage AddNotyMessage(NotyMessage message)
        {
            NotyMessages.Add(message);
            return message;
        }


    }
}
