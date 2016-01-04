
using System.Web.Mvc;
namespace Decision.Web.Controllers
{
    public partial class ReportController : Controller
    {
        public virtual ActionResult Design()
        {
            return View();
        }

        //public virtual ActionResult GetReportTemplate()
        //{
        //    StiReport report = new StiReport();
        //    //report.Load(Server.MapPath("~/Content/Report/Report.mrt"));


        //    return StiMvcDesigner.GetReportTemplateResult(report);
        //}

        //public virtual ActionResult GetLocalization()
        //{
        //    string path = Server.MapPath("~/Content/Report/");
        //    string name = StiMvcDesigner.GetLocalizationName(Request);
        //    return StiMvcDesigner.GetLocalizationResult(path + name);
        //}

        //public virtual FileResult ExportReport()
        //{
        //    StiReport report = StiMvcDesigner.GetReportObject(this.Request);
        //    return StiMvcDesigner.ExportReportResult(this.Request, report);
        //}

        //public virtual ActionResult GetReportSnapshot()
        //{
        //    return StiMvcDesigner.GetReportSnapshotResult(this.Request);
        //}

        //public virtual ActionResult DataProcessing()
        //{
        //    return StiMvcDesigner.DataProcessingResult(this.Request);
        //}
    }
}