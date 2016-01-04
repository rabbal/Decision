using System.Collections.Generic;
using System.Web.Mvc;

namespace Decision.ServiceLayer.Contracts.Common
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس استان
    /// </summary>
    public interface IStateService
    {
        /// <summary>
        /// واکشی لیست استان ها برای نمایش در لیست آبشاری
        /// </summary>
        /// <returns></returns>
        IList<SelectListItem> GetAsSelectListItemAsync(string selected,string path);
    }
}
