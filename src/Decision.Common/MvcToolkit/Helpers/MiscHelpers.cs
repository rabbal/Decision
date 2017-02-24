using System.Web.Mvc;

namespace NTierMvcFramework.Common.MvcToolkit.Helpers
{
    public static class MiscHelpers
    {
        public static bool IsDebug(this HtmlHelper htmlHelper)
        {
#if DEBUG
            return true;
#else
      return false;
#endif
        }

    }
}