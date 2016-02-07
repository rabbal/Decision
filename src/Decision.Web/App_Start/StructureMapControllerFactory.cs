using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Decision.Common.HiddenField;
using Decision.IocConfig;

namespace Decision.Web
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        #region Fields
        private readonly IEncryptSettingsProvider _settings;
        #endregion

        #region Ctor
        public StructureMapControllerFactory()
        {
            _settings = new EncryptSettingsProvider();
        }

        #endregion

        #region override CreateController

        private IRijndaelStringEncrypter GetDecrypter(RequestContext requestContext)
        {
            var decrypter = new RijndaelStringEncrypter(_settings, requestContext.GetActionKey());
            return decrypter;
        }
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            var routeData = requestContext.RouteData;
            if (routeData.Values.ContainsKey("MS_DirectRouteMatches"))
            {
                routeData = ((IEnumerable<RouteData>)routeData.Values["MS_DirectRouteMatches"]).First();
            }

            var parameters = requestContext.HttpContext.Request.Params;
            var encryptedParamKeys = parameters.AllKeys.Where(x => x.StartsWith(_settings.EncryptionPrefix)).ToList();

            IRijndaelStringEncrypter decrypter = null;

            foreach (var key in encryptedParamKeys)
            {
                if (decrypter == null)
                {
                    decrypter = GetDecrypter(requestContext);
                }

                var oldKey = key.Replace(_settings.EncryptionPrefix, string.Empty);
                var oldValue = decrypter.Decrypt(parameters[key]);
                if (routeData.Values[oldKey] != null)
                {
                    if (routeData.Values[oldKey].ToString() != oldValue)
                        throw new ApplicationException("Form values is modified!");
                }

                routeData.Values[oldKey] = oldValue;
            }

            decrypter?.Dispose();

            return base.CreateController(requestContext, controllerName);
        }

        #endregion

        #region override GetControllerInstance


        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                //var url = requestContext.HttpContext.Request.RawUrl;
                ////string.Format("Page not found: {0}", url).LogException();

                //requestContext.RouteData.Values["controller"] = MVC.Search.Name;
                //requestContext.RouteData.Values["action"] = MVC.Search.ActionNames.Index;
                //requestContext.RouteData.Values["keyword"] = url.GenerateSlug().Replace("-", " ");
                //requestContext.RouteData.Values["categoryId"] = 0;
                //return SampleObjectFactory.Container.GetInstance(typeof(SearchController)) as Controller;
                throw new InvalidOperationException($"Page not found: {requestContext.HttpContext.Request.RawUrl}");
            }
            var controller = ProjectObjectFactory.Container.GetInstance(controllerType) as Controller;
            if (controller != null)
            {
                controller.TempDataProvider = ProjectObjectFactory.Container.GetInstance<ITempDataProvider>();

            }
            return controller;
        }
        #endregion
    }
}




















