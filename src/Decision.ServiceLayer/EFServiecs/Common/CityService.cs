using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Settings;

namespace Decision.ServiceLayer.EFServiecs.Common
{
    /// <summary>
    /// کلاس ارائه دهنده سروسیس های لازم برای اعمال روی شهر
    /// </summary>
    public class CityService : ICityService
    {
        #region Fields

        private readonly HttpContextBase _httpContextBase;

        #endregion

        #region Ctor

        public CityService(HttpContextBase httpContextBase)
        {
            _httpContextBase = httpContextBase;
        }
        #endregion
        public IList<SelectListItem> GetAsSelectListByStateNameAsync(string state, string selected, string path)
        {
            var lst = from e in XDocument.Load(_httpContextBase.Server.MapPath(path)).Root.Elements(OwnConstants.State) where e.Attribute(OwnConstants.Name).Value == state select e;
            var cities = lst.SelectMany(a => a.Elements(OwnConstants.City));
            return
                cities.OrderBy(a => a.Value).Select(a => new SelectListItem { Value = a.Value, Text = a.Value, Selected = a.Value == selected })
                    .ToList();
        }
    }
}
