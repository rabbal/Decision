using Elmah;

namespace Decision.Framework.WebAPIToolkit.ExceptionHandling
{
    public sealed class ElmahErrorAttribute :
        ExceptionFilterAttribute
    {
        public override void OnException(
           HttpActionExecutedContext actionExecutedContext)
        {

            if (actionExecutedContext.Exception != null)
                ErrorSignal.FromCurrentContext().Raise(actionExecutedContext.Exception);

            base.OnException(actionExecutedContext);
        }
    }
}