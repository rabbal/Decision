using System.Collections.Generic;
using System.Web.Mvc;

namespace Decision.ServiceLayer.Interfaces.Common
{
    public interface ICityService
    {
      IList<SelectListItem> GetAsSelectListByStateNameAsync(string state,string selected,string path);
       
    }

}
