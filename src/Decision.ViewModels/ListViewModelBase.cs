using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Decision.ViewModels
{
    public abstract class ListViewModelBase
    {
        public IList<SelectListItem> SortByItems { get; set; }   
    }
}
