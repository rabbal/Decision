using System;
using System.Web;
using System.Web.Mvc;
using Decision.Framework.Extensions;
using Decision.Framework.MvcToolkit.Video;

namespace Decision.Framework.MvcToolkit.Helpers
{
    public static class HtmlHelperExtentions
    {
        public static DashboardHtmlHelper Dashboard(this HtmlHelper htmlHelper)
        {
            return new DashboardHtmlHelper(htmlHelper);
        }

        public static FrameworkHtmlHelper Framework(this HtmlHelper htmlHelper)
        {
            return new FrameworkHtmlHelper(htmlHelper);
        }

        public static MvcHtmlString GeneratorReturnUrl(this HtmlHelper htmlHelper, HttpContextBase contextBase,
            UrlHelper urlHelper)
        {
            var currentUrl = contextBase.Request.RawUrl;
            if (currentUrl == "/")
            {
                currentUrl = urlHelper.Action("Index", "Home");
            }
            return MvcHtmlString.Create(currentUrl);
        }

        public static MvcHtmlString HelpAlert(this HtmlHelper html, string iconPath, params string[] notes)
        {
            var div = new TagBuilder("div");
            div.AddCssClass("alert alert-info help-alert margin-bottom-0");
            var icon = new TagBuilder("img");
            icon.Attributes.Add("src", iconPath);
            icon.Attributes.Add("alt", "tip");
            div.InnerHtml = icon.ToString(TagRenderMode.Normal);
            var builder = new System.Text.StringBuilder();
            builder.Append(div.InnerHtml);
            foreach (var note in notes)
            {
                var br = new TagBuilder("br");
                builder.Append(note + br.ToString(TagRenderMode.StartTag));
            }
            div.InnerHtml = builder.ToString();
            return MvcHtmlString.Create(div.ToString());
        }


        //#region Images
        //public static MvcHtmlString Image(this HtmlHelper html, byte[] image, string alt, string cssClass, int size = 100)
        //{
        //    var img = string.Empty;

        //    if (!image.Take(4).SequenceEqual(new byte[] { 0, 0, 0, 0 }))
        //    {
        //        img = $"data:image/png;base64,{Convert.ToBase64String(image.ResizeImageFile(size, size))}";
        //    }

        //    return new MvcHtmlString("<img src='" + img + "' class='" + cssClass + "' alt='" + alt + "' width='" + size + "' height='" + size + "'/>");

        //}
        //#endregion


        public static string GetSummaryFromText(this HtmlHelper html, string text, int max)
        {
            return text.GetSummaryFromText(max);
        }

        public static string GetSummaryFromHtml(this HtmlHelper html, string htmlContent, int max)
        {
            return htmlContent.GetSummaryFromHtml(max);
        }


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


        //usage
        //@Html.YouTubePlayer("Casablanca", "iLdqKUkkM6w", new YouTubePlayerOption()
        //                         {
        //                             Border = true
        //                         })
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

    }
}
