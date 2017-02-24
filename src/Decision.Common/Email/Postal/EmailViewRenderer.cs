using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NTierMvcFramework.Common.Email.Postal
{
    public class EmailViewRenderer : IEmailViewRenderer
    {
        public EmailViewRenderer(ViewEngineCollection viewEngines)
        {
            this.viewEngines = viewEngines;
            EmailViewDirectoryName = "EmailTemplate";
        }

        private readonly ViewEngineCollection viewEngines;

        public string EmailViewDirectoryName { get; set; }

        public string Render(Email email, string viewName = null)
        {
            viewName = viewName ?? email.ViewName;
            var controllerContext = CreateControllerContext();
            var view = CreateView(viewName, controllerContext);
            var viewOutput = RenderView(view, email.ViewData, controllerContext, email.ImageEmbedder);
            return viewOutput;
        }

        private ControllerContext CreateControllerContext()
        {
            // A dummy HttpContextBase that is enough to allow the view to be rendered.
            var httpContext = new HttpContextWrapper(
                new HttpContext(
                    new HttpRequest("", UrlRoot(), ""),
                    new HttpResponse(TextWriter.Null)
                )
            );
            var routeData = new RouteData();
            routeData.Values["controller"] = EmailViewDirectoryName;
            var requestContext = new RequestContext(httpContext, routeData);
            var stubController = new StubController();
            var controllerContext = new ControllerContext(requestContext, stubController);
            stubController.ControllerContext = controllerContext;
            return controllerContext;
        }

        private static string UrlRoot()
        {
            var httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                return "http://localhost";
            }

            return httpContext.Request.Url.GetLeftPart(UriPartial.Authority) +
                   httpContext.Request.ApplicationPath;
        }

        private IView CreateView(string viewName, ControllerContext controllerContext)
        {
            var result = viewEngines.FindView(controllerContext, viewName, null);
            if (result.View != null)
                return result.View;

            throw new Exception(
                "Email view not found for " + viewName +
                ". Locations searched:" + Environment.NewLine +
                string.Join(Environment.NewLine, result.SearchedLocations)
            );
        }

        private static string RenderView(IView view, ViewDataDictionary viewData, ControllerContext controllerContext, ImageEmbedder imageEmbedder)
        {
            using (var writer = new StringWriter())
            {
                var viewContext = new ViewContext(controllerContext, view, viewData, new TempDataDictionary(), writer);
                viewData[ImageEmbedder.ViewDataKey] = imageEmbedder;
                view.Render(viewContext, writer);
                viewData.Remove(ImageEmbedder.ViewDataKey);
                return writer.GetStringBuilder().ToString();
            }
        }

        private class StubController : Controller { }
    }
}
