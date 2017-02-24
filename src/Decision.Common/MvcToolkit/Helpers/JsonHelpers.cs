using System.Web;
using System.Web.Mvc;
using Decision.Common.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Decision.Common.MvcToolkit.Helpers
{
    public static class JsonHelpers
    {
        public static IHtmlString Json(this HtmlHelper helper, object data)
        {
            var setting = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new JsonConverter[]
                {
                     new StringEnumConverter()
                },
                StringEscapeHandling = StringEscapeHandling.EscapeHtml
            };
            return MvcHtmlString.Create(JsonConvert.SerializeObject(data, setting));
        }


        public static IHtmlString JsonFor<T>(this HtmlHelper helper,object data)
        {
            return helper.Raw(data.ToJson());
        }
    }
}