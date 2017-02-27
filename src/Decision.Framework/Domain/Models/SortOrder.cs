using System.ComponentModel.DataAnnotations;

namespace Decision.Framework.Domain.Models
{
    public enum SortOrder
    {
        [Display(Name = "نزولی")]
        Desc = 0,
        [Display(Name = "صعودری")]
        Asc,
    }
}
