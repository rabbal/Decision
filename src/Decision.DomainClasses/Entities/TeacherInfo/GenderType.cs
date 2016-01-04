using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.Entities.TeacherInfo
{
    /// <summary>
    /// انواع جنسیت
    /// </summary>
    public enum  GenderType
    {
        /// <summary>
        /// مذکر
        /// </summary>
        [Display(Name = "مذکر")]
        Male,
        /// <summary>
        /// مونث
        /// </summary>
        [Display(Name = "مونث")]
        FeMale
    }
}
