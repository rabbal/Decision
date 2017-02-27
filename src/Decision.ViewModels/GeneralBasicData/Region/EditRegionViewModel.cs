using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Decision.DomainClasses;

namespace Decision.ViewModels.GeneralBasicData.Region
{
    public class EditRegionViewModel
    {
        public string Title { get; set; }
        public RegionType Type { get; set; }
        public IList<SelectListItem> ParentRegions { get; set; }
    }
}
