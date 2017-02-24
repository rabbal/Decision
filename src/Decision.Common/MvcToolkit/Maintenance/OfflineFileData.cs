using System;
using System.IO;

namespace NTierMvcFramework.Common.MvcToolkit.Maintenance
{
    public class OfflineFileData
    {
        internal const string OfflineFilePath = "~/App_Data/offline_file.txt";

        //The offline file contains three fields separated by the 'TextSeparator' char
        //a) datetimeUtc to go offline
        //b) the ip address to allow through
        //c) Message to show the user

        private const char TextSeparator = '|';

        private const string DefaultOfflineMessage =
            "کاربر گرامی سایت برای تعمیر و نگهداری فعلا از دسترس خارج میباشد.  بعدا مراجعه فرمایید";

        public OfflineFileData(string offlineFilePath)
        {
            var offlineContent = File.ReadAllText(offlineFilePath).Split(TextSeparator);

            DateTime parsedDateTime;
            TimeWhenSiteWillGoOfflineUtc = DateTime.TryParse(offlineContent[0],
                out parsedDateTime)
                ? parsedDateTime
                : DateTime.UtcNow;
            IpAddressToLetThrough = offlineContent[1];
            Message = offlineContent[2];
        }

        /// <summary>
        ///     This contains the datatime when the site should go offline should be offline
        /// </summary>
        public DateTime TimeWhenSiteWillGoOfflineUtc { get; private set; }

        /// <summary>
        ///     This contains the IP address of the authprised person to let through
        /// </summary>
        public string IpAddressToLetThrough { get; private set; }

        /// <summary>
        ///     A message to display in the Offline View
        /// </summary>
        public string Message { get; private set; }

        public static void SetOffline(int delayByMinutes,
            string currentIpAddress, string optionalMessage,
            Func<string, string> mapPath)
        {
            var offlineFilePath = mapPath?.Invoke(OfflineFilePath);

            var fields = string.Format("{0:O}{1}{2}{1}{3}",
                DateTime.UtcNow.AddMinutes(delayByMinutes), TextSeparator,
                currentIpAddress, optionalMessage ?? DefaultOfflineMessage);
            
            File.WriteAllText(offlineFilePath, fields);
        }

        public static void RemoveOffline(Func<string, string> mapPath)
        {
            var offlineFilePath = mapPath?.Invoke(OfflineFilePath);
            File.Delete(offlineFilePath);
        }
    }
}