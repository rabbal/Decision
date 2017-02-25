using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Decision.Framework.MvcToolkit.Helpers
{
    public static class JavaScriptHelpers
    {
        public static string CamelCaseIdForModel(this HtmlHelper helper)
        {
            var input = helper.IdForModel().ToString();
            if (string.IsNullOrEmpty(input) || !char.IsUpper(input[0]))
                return input;

            var stringBuilder = new StringBuilder();

            for (var i = 0; i < input.Length; ++i)
            {
                var flag = i + 1 < input.Length;

                if (i == 0 || !flag || char.IsUpper(input[i + 1]))
                {
                    var character = char.ToLowerInvariant(input[i]);
                    stringBuilder.Append(character);
                }
                else
                {
                    stringBuilder.Append(input.Substring(i));
                    break;
                }
            }
            return stringBuilder.ToString();
        }
    }
}