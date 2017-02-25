using System.Linq;
using System.Web;

namespace Decision.Framework.WebAPIToolkit
{
    /// <summary>
    /// todo:httpConfiguration.Services.Replace(typeof(IHostBufferPolicySelector), new FileUploadBufferPolicySelector());
    /// </summary>
    public class FileUploadBufferPolicySelector : WebHostBufferPolicySelector
    {
        private static readonly string[] _unbufferedControllers = new string[] { "image", "video" };
        public override bool UseBufferedInputStream(object hostContext)
        {
            var context = hostContext as HttpContextBase;
            if (context != null)
            {
                var controller = context.Request.RequestContext.RouteData.Values["controller"].ToString().
                ToLower();
                if (_unbufferedControllers.Contains(controller))
                    return false;
            }
            return true;
        }
       
    }
}