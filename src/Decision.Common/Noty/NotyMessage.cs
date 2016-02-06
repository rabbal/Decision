using System;

namespace Decision.Common.Noty
{
    [Serializable]
    public class NotyMessage
    {
        public  AlertType Type{ get; set; }
        public string Message { get; set; }
        public AnimationTypes CloseAnimation { get; set; }
        public AnimationTypes OpenAnimation { get; set; }
        public int AnimateSpeed { get; set; }
        /// <summary>
        /// gets or sets value indicating whether this message hast timeout(false) 
        /// </summary>
        public bool IsSticky { get; set; }
        public MessageCloseType CloseWith { get; set; }
        public MessageLocation Location { get; set; }
        public bool IsSwing { get; set; }
        public bool IsKiller { get; set; }
        public bool IsForce { get; set; }
        public bool  IsModal { get; set; }
    }
}