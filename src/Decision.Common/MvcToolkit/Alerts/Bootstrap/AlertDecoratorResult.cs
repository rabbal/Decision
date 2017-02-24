using System.Web.Mvc;

namespace NTierMvcFramework.Common.MvcToolkit.Alerts.Bootstrap
{
    public class AlertDecoratorResult : ActionResult
    {
        public ActionResult InnerResult { get; set; }
        public string Message { get; set; }
        public string AlertClass { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var alerts = context.Controller.TempData.GetAlerts();
            alerts.Add(new Alert { AlertClass = AlertClass, Message = Message });
            InnerResult.ExecuteResult(context);
        }
        
    }
}