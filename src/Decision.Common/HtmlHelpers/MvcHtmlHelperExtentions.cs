using System;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Decision.Common.Extentions;
using Decision.Common.Helpers;
using Decision.Utility;

namespace Decision.Common.HtmlHelpers
{
    public static class MvcHtmlHelperExtentions
    {
        #region Input FormControls
        public static MvcHtmlString NoAutoCompleteTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.TextBoxFor(expression, new { @class = "form-control", autocomplete = "off" });
        }
        public static MvcHtmlString NoAutoCompleteTextBoxForLtr<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.TextBoxFor(expression, new { @class = "form-control", autocomplete = "off", dir = "ltr" });
        }
        public static MvcHtmlString NoAutoCompleteTextBoxForNumber<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.TextBoxFor(expression, new { @class = "form-control", autocomplete = "off", dir = "ltr", data_val_number = "لطفا مقدار عددی وارد کنید" });
        }
        public static MvcHtmlString FormControlTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.TextBoxFor(expression, new { @class = "form-control" });
        }
        public static MvcHtmlString FormControlPasswordFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.PasswordFor(expression, new { @class = "form-control" });
        }
        #endregion

        #region UrlGenerators
        public static MvcHtmlString ReturnUrl(this HtmlHelper htmlHelper, HttpContextBase contextBase,
            UrlHelper urlHelper)
        {
            var currentUrl = contextBase.Request.RawUrl;
            if (currentUrl == "/")
            {
                currentUrl = urlHelper.Action("Index", "Home");
            }
            return MvcHtmlString.Create(currentUrl);
        }
        #endregion

        #region HelpAlerts
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="iconPath"></param>
        /// <param name="notes"></param>
        /// <returns></returns>
        public static MvcHtmlString HelpAlert(this HtmlHelper html, string iconPath, params string[] notes)
        {
            var div = new TagBuilder("div");
            div.AddCssClass("alert alert-info help-alert margin-bottom-0");
            var icon = new TagBuilder("img");
            icon.Attributes.Add("src", iconPath);
            icon.Attributes.Add("alt", "tip");
            div.InnerHtml = icon.ToString(TagRenderMode.Normal);
            foreach (var note in notes)
            {
                var br = new TagBuilder("br");
                div.InnerHtml += note + br.ToString(TagRenderMode.StartTag);
            }
            return MvcHtmlString.Create(div.ToString());
        }
        #endregion

        #region Images
        public static MvcHtmlString Image(this HtmlHelper html, byte[] image, string alt, string cssClass, int size = 100)
        {
            var img = string.Empty;

            if (!image.Take(4).SequenceEqual(new byte[] { 0, 0, 0, 0 }))
            {
                img = $"data:image/png;base64,{Convert.ToBase64String(image.ResizeImageFile(size, size))}";
            }

            return new MvcHtmlString("<img src='" + img + "' class='" + cssClass + "' alt='" + alt + "' width='" + size + "' height='" + size + "'/>");

        }
        #endregion

        #region GetSummaryFromHtml

        /// <summary>
        /// get summary of Html with max size
        /// </summary>
        /// <param name="html"></param>
        /// <param name="text"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static string GetSummaryFromHtml(this HtmlHelper html, string text, int max)
        {
            var summaryHtml = string.Empty;
            var words = text.CleanTags().Split(new[] { ' ' });

            for (var i = 0; i < max; i++)
            {
                summaryHtml += words[i];
            }

            return summaryHtml;
        }

        #endregion
        
        #region AparatPlayer
        public static MvcHtmlString AparatPlayer(this HtmlHelper helper, string mediafile, int height, int width)
        {
            var player = @"<embed height=""{0}"" width=""{1}"" flashvars=""config=http://www.aparat.com//video/video/config/videohash/{2}/watchtype/embed"" 
                                allowfullscreen=""true"" 
                                quality=""high"" 
                                name=""aparattv_{2}"" id=""aparattv_{2}"""" src=""http://host10.aparat.com/public/player/aparattv"" 
                                type=""application/x-shockwave-flash"">";

            player = string.Format(player, height, width, mediafile);
            return new MvcHtmlString(player);
        }
        #endregion
        
        #region YouTubePalyer
        //usage 
        //@Html.YouTubePlayer("Casablanca", "iLdqKUkkM6w", new YouTubePlayerOption()
        //                         {
        //                             Border = true
        //                         })
        public class YouTubePlayerOption
        {
            public YouTubePlayerOption()
            {
                Border = false;
            }

            public int Width { get; set; } = 425;
            public int Height { get; set; } = 355;
            public Color PrimaryColor { get; set; } = Color.Black;
            public Color SecondaryColor { get; set; } = Color.Aqua;
            public bool Border { get; set; }
        }

        public class ConvertColorToHexa
        {
            private static readonly char[] HexDigits =
            {
                '0', '1', '2', '3', '4', '5', '6', '7',
                '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
            };

            public static string ConvertColorToHexaString(Color color)
            {
                var bytes = new byte[3];
                bytes[0] = color.R;
                bytes[1] = color.G;
                bytes[2] = color.B;
                var chars = new char[bytes.Length * 2];
                for (var i = 0; i < bytes.Length; i++)
                {
                    int b = bytes[i];
                    chars[i * 2] = HexDigits[b >> 4];
                    chars[i * 2 + 1] = HexDigits[b & 0xF];
                }
                return new string(chars);
            }


        }
        public static MvcHtmlString YouTubePlayer(this HtmlHelper helper, string playerId, string mediaFile, YouTubePlayerOption youtubePlayerOption)
        {

            const string baseUrl = "http://www.youtube.com/v/";

            // YouTube Embedded Code
            var player = @"<div id=""YouTubePlayer_{7}""width:{1}px; height:{2}px;"">
                                 <object width=""{1}"" height=""{2}"">
                                 <param name=""movie"" value=""{6}{0}&fs=1&border={3}&color1={4}&color2={5}""></param>
                                 <param name=""allowFullScreen"" value=""true""></param>
                                 <embed src=""{6}{0}&fs=1&border={3}&color1={4}&color2={5}""
                                 type = ""application/x-shockwave-flash""
                                 width=""{1}"" height=""{2}"" allowfullscreen=""true""></embed>
                                 </object>
                             </div>";

            // Replace All The Value
            player = String.Format(player, mediaFile, youtubePlayerOption.Width, youtubePlayerOption.Height, (youtubePlayerOption.Border ? "1" : "0"), ConvertColorToHexa.ConvertColorToHexaString(youtubePlayerOption.PrimaryColor), ConvertColorToHexa.ConvertColorToHexaString(youtubePlayerOption.SecondaryColor), baseUrl, playerId);

            //Retrun Embedded Code
            return new MvcHtmlString(player);
        }
        #endregion
        
        //#region ToRouteValueDictionaryWithCollection
        //public static RouteValueDictionary ToRouteValueDictionaryWithCollection<T>(this T routeValues)
        //{
        //    var newRouteValues = new RouteValueDictionary();

        //    foreach (var key in routeValues.Keys)
        //    {
        //        var value = routeValues[key];

        //        var vals = value as IEnumerable;
        //        if (vals != null && !(value is string))
        //        {
        //            var index = 0;
        //            foreach (var val in vals)
        //            {
        //                if (val is string || val.GetType().IsPrimitive)
        //                {
        //                    newRouteValues.Add(String.Format("{0}[{1}]", key, index), val);
        //                }
        //                else
        //                {
        //                    var properties = val.GetType().GetProperties();
        //                    foreach (var propInfo in properties)
        //                    {
        //                        newRouteValues.Add(
        //                            String.Format("{0}[{1}].{2}", key, index, propInfo.Name),
        //                            propInfo.GetValue(val));
        //                    }
        //                }
        //                index++;
        //            }
        //        }
        //        else
        //        {
        //            newRouteValues.Add(key, value);
        //        }
        //    }

        //    return newRouteValues;
        //}

        //#endregion


    }
}
