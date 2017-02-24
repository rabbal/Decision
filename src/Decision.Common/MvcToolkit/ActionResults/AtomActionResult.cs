using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Decision.Common.Constants;

namespace Decision.Common.MvcToolkit.ActionResults
{
    /// <summary>
    /// Represents a class that is used to render an Atom 1.0 feed by using an <see cref="SyndicationFeed"/> instance 
    /// representing the feed.
    /// </summary>
    public sealed class AtomActionResult : ActionResult
    {
        private readonly SyndicationFeed _syndicationFeed;
        
        public AtomActionResult(SyndicationFeed syndicationFeed)
        {
            this._syndicationFeed = syndicationFeed;
        }
        
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = ContentType.Atom;

            var feedFormatter = new Atom10FeedFormatter(this._syndicationFeed);

            var xmlWriterSettings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8
            };

            if (HttpContext.Current.IsDebuggingEnabled)
            {
                // Indent the XML for easier viewing but only in Debug mode. In Release mode, everything is output on 
                // one line for best performance.
                xmlWriterSettings.Indent = true;
            }

            using (var xmlWriter = XmlWriter.Create(context.HttpContext.Response.Output, xmlWriterSettings))
            {
                feedFormatter.WriteTo(xmlWriter);
            }
        }
    }
}