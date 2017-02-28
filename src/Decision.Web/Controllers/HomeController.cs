using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Decision.Framework.Configuration;
using Decision.Framework.MvcToolkit.ActionResults;
using Decision.Framework.MvcToolkit.Filters;
using Decision.Framework.SEO.OpenGraph.Enums;
using Decision.Framework.SEO.OpenGraph.Media;
using Decision.Framework.SEO.OpenGraph.ObjectTypes.Facebook;
using Decision.Framework.SEO.OpenGraph.ObjectTypes.Standard;
using Decision.Framework.SEO.OpenGraph.Structs;
using Decision.Framework.SEO.Twitter.Cards;
using Decision.ViewModels;
using Decision.Web.Infrastructure.Constants;
using Decision.Web.Infrastructure.Services;
using Decision.Web.Infrastructure.Services.Contracts;
using DNTBreadCrumb;

namespace Decision.Web.Controllers
{
    [RoutePrefix("")]
    [BreadCrumb(Title = "خانه", UseDefaultRouteUrl = true, RemoveAllDefaultRouteValues = true,
        Order = 0, GlyphIcon = "fa fa-home")]
    public partial class HomeController : Controller
    {

        #region Fields (6)
        private readonly IAppConfiguration _configuration;
        private readonly IBrowserConfigService _browserConfigService;
        private readonly IFeedService _feedService;
        private readonly IOpenSearchService _openSearchService;
        private readonly IRobotsService _robotsService;
        private readonly ISitemapService _sitemapService;

        #endregion

        #region Constructors (1)

        public HomeController(
            IAppConfiguration configuration,
            IBrowserConfigService browserConfigService,
            IFeedService feedService,
            IOpenSearchService openSearchService,
            IRobotsService robotsService,
            ISitemapService sitemapService)
        {
            _browserConfigService = browserConfigService;
            _feedService = feedService;
            _configuration = configuration;
            _openSearchService = openSearchService;
            _robotsService = robotsService;
            _sitemapService = sitemapService;
        }

        #endregion

        #region Index
        public virtual ActionResult Index()
        {
            
            return View();
        }

        #endregion

        #region About
        [Route("About-Us")]
        public virtual ActionResult About()
        {
            throw new NotImplementedException();

        }
        #endregion

        #region Contact
        [Route("Contact-Us")]
        public virtual ActionResult Contact()
        {
            throw new NotImplementedException();
        }

        #endregion
        
    }
}