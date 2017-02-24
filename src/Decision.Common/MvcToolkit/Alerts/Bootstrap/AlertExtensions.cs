using System.Collections.Generic;
using System.Web.Mvc;

namespace NTierMvcFramework.Common.MvcToolkit.Alerts.Bootstrap
{
    public static class AlertExtensions
    {
        const string Alerts = "_Alerts";
        public static List<Alert> GetAlerts(this TempDataDictionary tempData)
        {
            if (!tempData.ContainsKey(Alerts))
            {
                tempData[Alerts] = new List<Alert>();
            }
            return (List<Alert>)tempData[Alerts];
        }

        public static ActionResult WithInfo(this ActionResult result, string message)
        {
            return new AlertDecoratorResult { InnerResult = result, AlertClass = "alert-info", Message = message };
        }
        public static ActionResult WithError(this ActionResult result, string message)
        {
            return new AlertDecoratorResult { InnerResult = result, AlertClass = "alert-danger", Message = message };
        }
        public static ActionResult WithWarning(this ActionResult result, string message)
        {
            return new AlertDecoratorResult { InnerResult = result, AlertClass = "alert-warning", Message = message };
        }
        public static ActionResult WithSuccess(this ActionResult result, string message)
        {
            return new AlertDecoratorResult { InnerResult = result, AlertClass = "alert-success", Message = message };
        }
    }
}