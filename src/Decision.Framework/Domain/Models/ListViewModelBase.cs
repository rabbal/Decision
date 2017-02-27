using System.Collections.Generic;
using System.Web.Mvc;

namespace Decision.Framework.Domain.Models
{
    public abstract class ListViewModelBase<TViewModel, TListRequest>
    {
        public TListRequest Request { get; set; }
        public IList<TViewModel> Applicants { get; set; }
    }
}
