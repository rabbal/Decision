using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Web;
using Decision.Common.Extentions;
using Decision.Utility;
using Timer = System.Timers.Timer;

namespace Decision.Common.HttpModules
{
    /// <summary>
    /// ماژولی برای جلوگیری از حملات 
    /// Dos
    /// </summary>
    public class DosAttackModule : IHttpModule
    {

        #region  Fields
        private const int BannedRequests = 10;
        private const int ReductionInterval = 1000; // 1 second
        private const int ReleaseInterval = 12 * 60 * 60 * 1000; // 12 hour

        private static readonly Lazy<Dictionary<string, short>> IpAdresses =
            new Lazy<Dictionary<string, short>>(() => new Dictionary<string, short>(),
                LazyThreadSafetyMode.ExecutionAndPublication);

        private static readonly Lazy<Stack<string>> Banned = new Lazy<Stack<string>>(() => new Stack<string>(),
            LazyThreadSafetyMode.ExecutionAndPublication);
        private static readonly Lazy<Timer> Timer = new Lazy<Timer>(CreateTimer, LazyThreadSafetyMode.ExecutionAndPublication);
        private static readonly Lazy<Timer> BannedTimer = new Lazy<Timer>(CreateBanningTimer, LazyThreadSafetyMode.ExecutionAndPublication);

        #endregion

        #region IHttpModule Members
        void IHttpModule.Dispose()
        {
            // Nothing to dispose; 
        }

        void IHttpModule.Init(HttpApplication context)
        {
            context.BeginRequest += Context_BeginRequest;
        }

        private static void Context_BeginRequest(object sender, EventArgs e)
        {
            //اگر عمل 
            //Forwarding 
            //صورت نگیرد در این صورت آدرس اصلی در متغییر زیر است
            var ip = HttpContext.Current.Request.GetIp();

            if (Banned.Value.Contains(ip))
            {
                // see 429 - Rate Limit Exceeded HTTP error
                HttpContext.Current.Response.StatusCode = 409;
                HttpContext.Current.Response.End();
            }

            CheckIpAddress(ip);
        }

        #endregion

        #region CheckIpAddress
        /// <summary>
        /// چک میکنیم که آیا آی پی مورد نظر در لیست 
        /// آی پی های است یا خیر ، اگر از حد تعیین شده زیاد بود درخواست هایش لذا بلاک شود
        /// </summary>
        /// <param name="ip">آی پی شخص</param>
        private static void CheckIpAddress(string ip)
        {
            if (!IpAdresses.Value.ContainsKey(ip))
            {
                IpAdresses.Value[ip] = 1;
            }
            else if (IpAdresses.Value[ip] == BannedRequests)
            {
                Banned.Value.Push(ip);
                IpAdresses.Value.Remove(ip);
            }
            else
            {
                IpAdresses.Value[ip]++;
            }
        }
        #endregion

        #region Timers

        /// <summary>
        ///در این متد یک تایمر برای برای حذف آی پی  از لیست آی پی ها
        /// </summary>
        private static Timer CreateTimer()
        {
            var timer = GetTimer(ReductionInterval);
            timer.Elapsed += TimerElapsed;
            return timer;
        }

        /// <summary>
        ///ساخت تایمر برای حذف آی پی  بلاک شده از لیست آی پی های بلاک شده 
        /// </summary>
        /// <returns></returns>
        private static Timer CreateBanningTimer()
        {
            var timer = GetTimer(ReleaseInterval);
            timer.Elapsed += delegate { Banned.Value.Pop(); };
            return timer;
        }

        /// <summary>
        /// Creates a simple timer instance and starts it.
        /// </summary>
        /// <param name="interval">The interval in milliseconds.</param>
        private static Timer GetTimer(int interval)
        {
            var timer = new Timer { Interval = interval };
            timer.Start();

            return timer;
        }

        /// <summary>
        /// کم کردن تعداد درخواست هر آی پی در هر ثانیه
        /// </summary>
        private static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var key in IpAdresses.Value.Keys)
            {
                IpAdresses.Value[key]--;
                if (IpAdresses.Value[key] == 0)
                    IpAdresses.Value.Remove(key);
            }
        }

        #endregion

    }
}
