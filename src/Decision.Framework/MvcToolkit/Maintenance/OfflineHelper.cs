using System;
using System.IO;

namespace Decision.Framework.MvcToolkit.Maintenance
{
    public class OfflineHelper
    {
        public OfflineHelper(string currentIpAddress, Func<string, string> mapPath)
        {
            var offlineFilePath = mapPath?.Invoke(OfflineFileData.OfflineFilePath);

            if (File.Exists(offlineFilePath))
            {
                if (OfflineData == null)
                    OfflineData = new OfflineFileData(offlineFilePath);

                ThisUserShouldBeOffline =
                    DateTime.UtcNow.Subtract(OfflineData.TimeWhenSiteWillGoOfflineUtc)
                        .TotalSeconds > 0
                    && currentIpAddress != OfflineData.IpAddressToLetThrough;
            }
            else
            {
                OfflineData = null;
            }
        }

        public static OfflineFileData OfflineData { get; private set; }

        /// <summary>
        /// This is true if we should redirect the user to the Offline View
        /// </summary>
        public bool ThisUserShouldBeOffline { get; private set; }
    }
}