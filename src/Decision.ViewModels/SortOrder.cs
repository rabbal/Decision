using System.ComponentModel.DataAnnotations;

namespace Decision.ViewModels
{
    public enum SortOrder
    {
        [Display(Name = "نزولی")]
        Desc = 0,
        [Display(Name = "صعودری")]
        Asc,
    }
}
