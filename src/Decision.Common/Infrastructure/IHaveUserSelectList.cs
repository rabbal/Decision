using System.Web.Mvc;

namespace NTierMvcFramework.Common.Infrastructure
{

    public interface IHaveUserSelectList
    {
        SelectListItem[] AvailableUsers { get; set; }
    }
}