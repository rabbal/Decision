using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Decision.Common.Extentions;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Settings;

namespace Decision.ServiceLayer.EFServiecs.Common
{
    /// <summary>
    /// نشان دهنده سرویس دهنده عملیات مرتبط با استان 
    /// </summary>
    public class StateService : IStateService
    {
        #region Fields

        private readonly HttpContextBase _httpContextBase;

        #endregion

        #region Ctor

        public StateService(HttpContextBase httpContextBase)
        {
            _httpContextBase = httpContextBase;
        }
        #endregion
        public IList<SelectListItem> GetAsSelectListItemAsync(string selected, string path)
        {
            var lst = from e in XDocument.Load(_httpContextBase.Server.MapPath(path)).Root.Elements(OwnConstants.State) select e;
            var states = lst.Select(a => a.Attribute(OwnConstants.Name).Value);
            return
                states.OrderBy(a => a).Select(a => new SelectListItem { Value = a, Text = a, Selected = selected.HasValue() && a == selected })
                    .ToList();

        }
    }
}
