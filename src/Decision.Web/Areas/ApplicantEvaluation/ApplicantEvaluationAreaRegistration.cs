using System.Web.Mvc;

namespace Decision.Web.Areas.ApplicantEvaluation
{
    public class ApplicantEvaluationAreaRegistration : AreaRegistration 
    {
        public override string AreaName => nameof(ApplicantEvaluation);

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ApplicantEvaluation_default",
                "ApplicantEvaluation/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}