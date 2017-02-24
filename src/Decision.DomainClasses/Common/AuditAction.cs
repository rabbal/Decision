using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.Common
{
    public enum AuditAction
    {
        [Display(Name = "درج")]
        Create,
        [Display(Name = "ویرایش")]
        Update,
        [Display(Name = "حذف فیزیکی")]
        Delete,
        [Display(Name = "حذف نرم")]
        SoftDelete,
    }
}
