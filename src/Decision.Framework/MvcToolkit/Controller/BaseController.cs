using System;
using System.Linq;
using System.Web.Mvc;
using Decision.Framework.KendoLinq;
using Decision.Framework.MvcToolkit.Json;
using MvcThrottle;

namespace Decision.Framework.MvcToolkit.Controller
{
    [EnableThrottling]
    public class BaseController : System.Web.Mvc.Controller
    {
        
        protected void StoreData(string index, object value)
        {
            TempData[index] = value;
        }

        protected T RetrieveData<T>(string index)
        {
            return (T)TempData[index];
        }

        #region Methods (4)
        [Obsolete("Do not use standard Json Helper to return json data to the client. Use either JsonError or JsonSuccess instead.")]
        protected JsonResult Json<T>(T data)
        {
            throw new InvalidOperationException("Do not use standard Json Helper to return json data to the client. Use either JsonError or JsonSuccess instead.");
        }

        protected StandardJsonResult JsonValidationError()
        {
            var result = new StandardJsonResult();

            foreach (var validationError in ModelState.Values.SelectMany(v => v.Errors))
            {
                result.AddError(validationError.ErrorMessage);
            }
            return result;
        }

        protected StandardJsonResult JsonError(string errorMessage)
        {
            var result = new StandardJsonResult();

            result.AddError(errorMessage);

            return result;
        }

        protected StandardJsonResult JsonSuccess<T>(T data) => new StandardJsonResult<T> { Data = data };
        protected JsonNetResult BetterJson<T>(T data) => new JsonNetResult { Data = data };
        protected JsonNetResult BetterJson<T>(T data, JsonRequestBehavior behavior) =>

            new JsonNetResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = data };

        protected JsonNetResult BetterJsonValidationError<T>(T data) where T : BaseListResponse
        {
            foreach (var validationError in ModelState.Values.SelectMany(v => v.Errors))
            {
                data.Errors.Add(validationError.ErrorMessage);
            }
            return new JsonNetResult { Data = data };
        }
        #endregion
    }


}

