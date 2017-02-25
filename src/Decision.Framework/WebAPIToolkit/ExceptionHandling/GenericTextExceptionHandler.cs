using System.Text;
using Decision.Framework.WebAPIToolkit.Results;

namespace Decision.Framework.WebAPIToolkit.ExceptionHandling
{
    public class GenericTextExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new InternalServerErrorTextPlainResult(
                "An unhandled exception occurred; check the log for more information.",
                Encoding.UTF8, context.Request);
        }
    }
}