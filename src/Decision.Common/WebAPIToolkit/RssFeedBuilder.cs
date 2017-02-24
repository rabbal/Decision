using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Syndication;
using System.Xml;
using Decision.Common.Infrastructure;

namespace Decision.Common.WebAPIToolkit
{
    public class RssFeedBuilder
    {
        private readonly string _feedTitle;
        private readonly IEnumerable<IRss> _items;

        public RssFeedBuilder(IRss item) : this(new List<IRss> {item})
        {
        }

        public RssFeedBuilder(IEnumerable<IRss> items, string feedTitle = "My feed")
        {
            _items = items;
            _feedTitle = feedTitle;
        }

        public void BuildSyndicationFeed(Stream stream, string contenttype)
        {
            var items = new List<SyndicationItem>();
            var feed = new SyndicationFeed
            {
                Title = new TextSyndicationContent(_feedTitle)
            };
            var enumerator = _items.GetEnumerator();
            while (enumerator.MoveNext())
            {
                items.Add(BuildSyndicationItem(enumerator.Current));
            }
            feed.Items = items;
            using (var writer = XmlWriter.Create(stream))
            {
                var rssformatter = new Rss20FeedFormatter(feed);
                rssformatter.WriteTo(writer);
            }
        }

        private static SyndicationItem BuildSyndicationItem(IRss singleItem)
        {
            var feedItem = new SyndicationItem
            {
                Title = new TextSyndicationContent(singleItem.Title),
                BaseUri = singleItem.Link,
                LastUpdatedTime = singleItem.CreatedAt,
                Content = new TextSyndicationContent(singleItem.Description)
            };
            feedItem.Authors.Add(new SyndicationPerson {Name = singleItem.Author});
            return feedItem;
        }
    }
}