using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Decision.Utility;

namespace Decision.Common.Helpers
{
    public static class SeoExtensions
    {
        #region Fields
        private const string SeparatorTitle = " - ";
        private const int MaxLenghtTitle = 60;
        private const int MaxLenghtSlug = 45;
        private const int MaxLenghtDescription = 170;
        #endregion

        #region Social Snippet
        public static string GenerateSocialSnippet()
        {
            return null;
        }
        #endregion

        #region Rich Snippet

        public static string AuthorGooglePlusLink(string googlePlusId, string virtualImageUrl, int imageWidth, int imageHeight)
        {
            var url = "https://plus.google.com/" + googlePlusId + "?rel=author";
            var div =
                string.Format(" <a itemprop=\"social\" rel=\"author\" href=\"{0}\">"
                              + "<img src=\"{1}\" width=\"{2}\" height=\"{3}\" alt=\"g+\" /></a>", url, virtualImageUrl,
                    imageWidth, imageHeight);
            return div;
        }
        public static string GenerateRichSnippetForRating(string personName, string personWritedItemsUrl, string itemTitle, int ratersCount, int ratingValue, string itemtype = "Product")
        {
            itemtype = string.Format("http://schema.org/{0}", itemtype);

            var div = string.Format("<div itemscope itemtype=\"{0}\" class=\"aggregateRating\">" +
                                     "<span itemprop=\"name\">{1}</span>" +
                                     "<div itemprop=\"aggregateRating\" itemscope itemtype=\"http://schema.org/AggregateRating\">" +
                                     "Rated <span itemprop=\"ratingValue\">{2}</span>/5 based on <span itemprop=\"reviewCount\">{3}</span>" +
                                     "readers reviews</div></div>", itemtype, itemTitle, ratingValue, ratersCount);
            if (!string.IsNullOrEmpty(personName))
                div +=
                    string.Format("<div itemscope itemtype=\"http://schema.org/Person\" class=\"aggregateRating\">" +
                                  "<span itemprop=\"name\">{0}</span> More About Author" +
                                  " <a href=\"{1}\" itemprop=\"url\">اطلاعات بیشتر در مورد نویسنده</a></div>",
                        personName, personWritedItemsUrl);

            return div;
        }
        #endregion

        #region MetaTag
        private const string FaviconPath = "~/Content/favicon/favicon.ico";

        public static string GenerateMetaTag(string title, string description, string canonicalUrl, string googlePlusUrl, bool allowIndexPage, bool allowCache,
            bool allowFollowLinks, string author = "", string lastmodified = "", string expires = "never",
            string applicationName = "web app", string language = "fa",
            CacheControlType cacheControlType = CacheControlType.Private, bool allowTranslate = true)
        {
            title = title.Substring(0, title.Length <= MaxLenghtTitle ? title.Length : MaxLenghtTitle).Trim();
            description =
                description.Substring(0,
                    description.Length <= MaxLenghtDescription ? description.Length : MaxLenghtDescription).Trim();

            var meta = "";
            meta += string.Format("<meta charset=\"utf-8\"/>\n");
            meta += string.Format("<title>{0}</title>\n", title);

            meta +=
                string.Format(
                    "<meta content=\"width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no\" name=\"viewport\">");

            if (!string.IsNullOrEmpty(googlePlusUrl))
                meta += string.Format("<link rel=\"author\" href=\"{0}\"/>\n", googlePlusUrl);
            meta += string.Format("<link rel=\"canonical\" href=\"{0}\"/>\n", canonicalUrl);
            meta += string.Format("<link rel=\"shortcut icon\" href=\"{0}\" type=\"image/x-icon\" />\n", FaviconPath);
            meta += string.Format("<meta name=\"application-name\" content=\"{0}\" />\n", applicationName);
            meta += string.Format("<meta name=\"msapplication-config\" content=\"/browserconfig.xml\" />\n");
            meta += string.Format("<meta http-equiv=\"content-language\" content=\"{0}\"/>\n", language);
            if (allowTranslate)
                meta += string.Format("<meta name=\"google\" content=\"notranslate\" />\n");

            meta += string.Format("<meta name=\"description\" content=\"{0}\"/>\n", description);
            meta += string.Format("<meta http-equiv=\"Cache-control\" content=\"{0}\"/>\n",
                EnumHelper.GetEnumDescription(typeof(CacheControlType), cacheControlType.ToString()));

            meta += string.Format("<meta name=\"robots\" content=\"{0}, {1}, {2}\" />\n",
                allowIndexPage ? "index" : "noindex", allowFollowLinks ? "follow" : "nofollow",
                allowCache ? "archive" : "noarchive");
            meta += string.Format("<meta name=\"expires\" content=\"{0}\"/>\n", expires);

            if (!string.IsNullOrEmpty(lastmodified))
                meta += string.Format("<meta name=\"last-modified\" content=\"{0}\"/>\n", lastmodified);

            if (!string.IsNullOrEmpty(author))
                meta += string.Format("<meta name=\"author\" content=\"{0}\"/>\n", author);

            //------------------------------------Google & Bing Doesn't Use Meta Keywords ...
            //meta += string.Format("<meta name=\"keywords\" content=\"{0}\"/>\n", keywords);

            return meta;
        }

        #endregion

        #region Slug
        public static string GenerateSlug(this string title)
        {
            var slug = RemoveAccent(title).ToLower();
            slug = Regex.Replace(slug, @"[^a-z0-9-\u0600-\u06FF]", "-");
            slug = Regex.Replace(slug, @"\s+", "-").Trim();
            slug = Regex.Replace(slug, @"-+", "-");
            slug = slug.Substring(0, slug.Length <= MaxLenghtSlug ? slug.Length : MaxLenghtSlug).Trim();

            return slug.RemoveDiacritics();
        }
        private static string RemoveAccent(this string text)
        {
            var bytes = Encoding.GetEncoding("UTF-8").GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        }
        #endregion

        #region Title
        public static string ResolveTitleForUrl(this HtmlHelper htmlHelper, string title)
        {
            return string.IsNullOrEmpty(title)
                ? string.Empty
                : Regex.Replace(Regex.Replace(title, "[^\\w]", "-"), "[-]{2,}", "-");
        }

        public static string ResolveTitleForUrl(string title)
        {
            return string.IsNullOrEmpty(title)
                ? string.Empty
                : Regex.Replace(Regex.Replace(title, "[^\\w]", "-"), "[-]{2,}", "-");
        }


        public static string GeneratePageTitle(params string[] crumbs)
        {
            var title = "";

            for (var i = 0; i < crumbs.Length; i++)
            {
                title += string.Format
                            (
                                "{0}{1}",
                                crumbs[i],
                                (i < crumbs.Length - 1) ? SeparatorTitle : string.Empty
                            );
            }

            title = title.Substring(0, title.Length <= MaxLenghtTitle ? title.Length : MaxLenghtTitle).Trim();

            return title.RemoveDiacritics();
        }

      
        #endregion

        #region GeneratePageDescription
          public static string GeneratePageDescription(string description)
        {
            return
                description.Substring(0,
                    description.Length <= MaxLenghtDescription ? description.Length : MaxLenghtDescription).Trim();
        }
        #endregion

        #region Enum
        public enum CacheControlType
        {
            [Description("public")]
            Public,
            [Description("private")]
            Private,
            [Description("no-cache")]
            Nocache,
            [Description("no-store")]
            Nostore
        }
        #endregion

        #region GetQueryString of referrer search engine
        public static string GetKeywords(string urlReferrer)
        {
            string searchQuery;
            var url = new Uri(urlReferrer);
            var query = HttpUtility.ParseQueryString(urlReferrer);
            switch (url.Host)
            {
                case "google":
                case "daum":
                case "msn":
                case "bing":
                case "ask":
                case "altavista":
                case "alltheweb":
                case "live":
                case "najdi":
                case "aol":
                case "seznam":
                case "search":
                case "szukacz":
                case "pchome":
                case "kvasir":
                case "sesam":
                case "ozu":
                case "mynet":
                case "ekolay":
                    searchQuery = query["q"];
                    break;
                case "naver":
                case "netscape":
                case "mama":
                case "mamma":
                case "terra":
                case "cnn":
                    searchQuery = query["query"];
                    break;
                case "virgilio":
                case "alice":
                    searchQuery = query["qs"];
                    break;
                case "yahoo":
                    searchQuery = query["p"];
                    break;
                case "onet":
                    searchQuery = query["qt"];
                    break;
                case "eniro":
                    searchQuery = query["search_word"];
                    break;
                case "about":
                    searchQuery = query["terms"];
                    break;
                case "voila":
                    searchQuery = query["rdata"];
                    break;
                case "baidu":
                    searchQuery = query["wd"];
                    break;
                case "yandex":
                    searchQuery = query["text"];
                    break;
                case "szukaj":
                    searchQuery = query["wp"];
                    break;
                case "yam":
                    searchQuery = query["k"];
                    break;
                case "rambler":
                    searchQuery = query["words"];
                    break;
                default:
                    searchQuery = query["q"];
                    break;
            }
            return searchQuery;
        }
        #endregion

        #region  SeoUrls
        public static string ToSeoUrl(this string url)
        {
            // make the url lowercase
            var encodedUrl = (url ?? "").ToLower();

            // replace & with and
            encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");

            // remove characters
            encodedUrl = encodedUrl.Replace("'", "");

            // remove invalid characters
            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9-\u0600-\u06FF]", "-");

            // remove duplicates
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            // trim leading & trailing characters
            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }
        #endregion

    }
}