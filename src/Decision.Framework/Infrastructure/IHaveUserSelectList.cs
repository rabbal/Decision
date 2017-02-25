using System.Web.Mvc;

namespace Decision.Framework.Infrastructure
{
    public interface IHaveUserSelectList
    {
        SelectListItem[] AvailableUsers { get; set; }
    }
}