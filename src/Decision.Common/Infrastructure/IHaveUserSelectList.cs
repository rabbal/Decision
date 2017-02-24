using System.Web.Mvc;

namespace Decision.Common.Infrastructure
{
    public interface IHaveUserSelectList
    {
        SelectListItem[] AvailableUsers { get; set; }
    }
}