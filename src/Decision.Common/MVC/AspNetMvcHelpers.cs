
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Decision.Common.Helpers;
using Decision.Common.Helpers.Extentions;

namespace Decision.Common.MVC
{
    public static class AspNetMvcHelpers
    {
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

        #region Persia.Net

        //public static MvcHtmlString FarsiDate(this HtmlHelper html, DateTime dateTime)
        //{
        //    var tag = new TagBuilder("span");
        //    tag.MergeAttribute("dir", "ltr");
        //    tag.AddCssClass("farsi-date");
        //    tag.SetInnerText(Calendar.ConvertToPersian(dateTime).ToString("W"));
        //    return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        //}

        //public static MvcHtmlString FarsiDateAndTime(this HtmlHelper html, DateTime dateTime)
        //{
        //    return MvcHtmlString.Create(FarsiTime(html, dateTime).ToHtmlString() + "  ,  " + FarsiDate(html, dateTime).ToHtmlString());
        //}
        //public static MvcHtmlString FarsiTime(this HtmlHelper html, DateTime dateTime)
        //{
        //    var tag = new TagBuilder("span");
        //    tag.MergeAttribute("dir", "ltr");
        //    tag.AddCssClass("farsi-time");
        //    tag.SetInnerText(Calendar.ConvertToPersian(dateTime).ToString("R"));
        //    return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        //}
        //public static MvcHtmlString FarsiRemaining(this HtmlHelper html, DateTime dateTime)
        //{
        //    var tag = new TagBuilder("span");
        //    tag.MergeAttribute("dir", "rtl");
        //    tag.AddCssClass("farsi-remaining");
        //    tag.SetInnerText(Calendar.ConvertToPersian(dateTime).ToRelativeDateString("TY"));
        //    return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        //}

        #endregion

        #region GetListOfErrors

        public static List<string> GetListOfErrors(this ModelStateDictionary modelState)
        {
            var list = modelState.ToList();
            var listErrors = new List<string>();
            foreach (var keyValuePair in list)
            {
                listErrors.AddRange(keyValuePair.Value.Errors.Select(error => error.ErrorMessage));
            }
            return listErrors;
        }

        #endregion

        #region Sorting


        public static string DeterminateSorting(string sortBy, string name, string sortDirection)
        {
            if (sortBy == "Non") return "fa-sort";
            if (sortDirection == "Desc")
            {
                return name == sortBy ? "fa-sort-desc" : "fa-sort";
            }
            return name == sortBy ? "fa-sort-asc" : "fa-sort";
        }

        #endregion

        #region ActionLinks

        //public static MvcHtmlString SideBarActionLink(this HtmlHelper htmlHelper, string linkText,
        //    Task<ActionResult> taskResult, string classes)
        //{

        //}

        //public static MvcHtmlString SideBarActionLink(this HtmlHelper htmlHelper, string linkText,
        //  ActionResult taskResult, string classes)
        //{

        //}
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
        public static MvcHtmlString Image(this HtmlHelper html, byte[] image, string alt, string cssClass, int size = 100)
        {
            var img = string.Empty;

            if (!image.Take(4).SequenceEqual(new byte[] { 0, 0, 0, 0 }))
            {
                img = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(image.ResizeImageFile(size,size)));
            }

            return new MvcHtmlString("<img src='" + img + "' class='" + cssClass + "' alt='" + alt + "' width='" + size + "' height='" + size + "'/>");

        }

    }
}
