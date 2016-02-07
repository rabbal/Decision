using System.Web.Mvc;
using System.Web.UI;
using Decision.Common.Filters;
using Decision.Common.Json;
using Decision.DataLayer.Context;
using Decision.ServiceLayer.Contracts.Common;

namespace Decision.Web.Controllers
{
    
    [RoutePrefix("BaseSetting/City")]
    [Route("{action}")]
    public partial class CityController : Controller
    {
	    #region	Fields
        private const string IranCitiesPath = "~/App_Data/IranCities.xml";
	    private readonly IUnitOfWork _unitOfWork;
		private readonly ICityService _cityService;
	    #endregion 

		#region	Ctor
		public CityController(IUnitOfWork unitOfWork,ICityService cityService){
		_unitOfWork=unitOfWork;
		_cityService=cityService;
		}
		#endregion 

        #region GetCities
        [AjaxOnly]
        [Mvc5Authorize()]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual ActionResult GetCities(string id)
        {
            return new JsonNetResult
            {
                Data =
                    _cityService.GetAsSelectListByStateNameAsync(id, null,
                        IranCitiesPath),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion

    }
}
