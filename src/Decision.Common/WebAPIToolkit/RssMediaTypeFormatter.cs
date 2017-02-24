using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NTierMvcFramework.Common.Infrastructure;

namespace NTierMvcFramework.Common.WebAPIToolkit
{
    /// <summary>
    /// todo:httpConfiguration.Formatters.Insert(0, new RssMediaTypeFormatter());
    /// </summary>
    public class RssMediaTypeFormatter : MediaTypeFormatter
    {
        private readonly string rss = "application/rss+xml";

        private readonly Func<Type, bool> typeisIRss = type => typeof(IRss).IsAssignableFrom(type);

        private readonly Func<Type, bool> typeisIRssCollection = type => typeof(IEnumerable<IRss>).
            IsAssignableFrom(type);

        public RssMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(rss));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return typeisIRss(type) || typeisIRssCollection(type);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream,
            HttpContent content, TransportContext transportContext)
        {
            RssFeedBuilder builder;
            builder = typeisIRss(type)
                ? new RssFeedBuilder((IRss) value)
                : new RssFeedBuilder((IEnumerable<IRss>) value);
            builder.BuildSyndicationFeed(writeStream, content.Headers.ContentType.MediaType);
            return Task.FromResult(0);
        }
    }
}