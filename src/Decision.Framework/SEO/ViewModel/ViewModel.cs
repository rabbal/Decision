using Decision.Framework.SEO.OpenGraph.ObjectTypes;
using Decision.Framework.SEO.Referrer;
using Decision.Framework.SEO.Twitter;

namespace Decision.Framework.SEO.ViewModel
{
    /// <summary>
    ///     A base class for a view's model.
    /// </summary>
    public class ViewModel
    {
        public ViewModel()
        {
            ReferrerMode = ReferrerMode.NoneWhenDowngrade;
            Index = Follow = true;
        }
        public string LastModified { get; set; }
        public string Keywords { get; set; }
        public string Author { get; set; }
        /// <summary>
        /// description for richsnippets and microdata
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// meta description content and short than <see cref="Description"/>
        /// </summary>
        public string MetaDescription { get; set; }
        public string Title { get; set; }
        public ReferrerMode ReferrerMode { get; set; }
        public bool Index { get; set; }
        public bool Follow { get; set; }
        public OpenGraphMetadata OpenGraph { get; set; }
        public TwitterCard TwitterCard { get; set; }
    }
}