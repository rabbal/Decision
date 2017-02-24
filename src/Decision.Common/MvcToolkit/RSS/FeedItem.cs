using System;

namespace Decision.Common.MvcToolkit.RSS
{
    public class FeedItem
    {
        public string Title { set; get; }
        public string AuthorName { set; get; }
        public string Content { set; get; }
        public string Url { set; get; }
        public DateTime ModifiedOn { set; get; }
        public DateTime PublishedOn { set; get; }
    }
}
