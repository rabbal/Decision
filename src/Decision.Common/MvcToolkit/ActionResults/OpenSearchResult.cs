using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace NTierMvcFramework.Common.MvcToolkit.ActionResults
{
    public class OpenSearchResult : ActionResult
    {
        public string ShortName { set; get; }
        public string Description { set; get; }
        public string SearchForm { set; get; }
        public string FavIconUrl { set; get; }
        public string SearchUrlTemplate { set; get; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var response = context.HttpContext.Response;
            writeToResponse(response);
        }

        private void writeToResponse(HttpResponseBase response)
        {
            response.ContentEncoding = Encoding.UTF8;
            response.ContentType = "application/opensearchdescription+xml";
            using (var xmlWriter = XmlWriter.Create(response.Output, new XmlWriterSettings { Indent = true }))
            {
                xmlWriter.WriteStartElement("OpenSearchDescription", "http://a9.com/-/spec/opensearch/1.1/");

                xmlWriter.WriteElementString(nameof(ShortName), ShortName);
                xmlWriter.WriteElementString(nameof(Description), Description);
                xmlWriter.WriteElementString("InputEncoding", "UTF-8");
                xmlWriter.WriteElementString(nameof(SearchForm), SearchForm);

                xmlWriter.WriteStartElement("Url");
                xmlWriter.WriteAttributeString("type", "text/html");
                xmlWriter.WriteAttributeString("template", SearchUrlTemplate);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Image");
                xmlWriter.WriteAttributeString("width", "16");
                xmlWriter.WriteAttributeString("height", "16");
                xmlWriter.WriteString(FavIconUrl);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
                xmlWriter.Close();
            }
        }
    }
}