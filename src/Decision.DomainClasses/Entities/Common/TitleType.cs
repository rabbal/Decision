using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.Entities.Common
{
    /// <summary>
    /// انواع عنوان ها
    /// </summary>
    public enum TitleType : int
    {
        /// <summary>
        ///شامل عنوان تدریس/درس/عنوان اولویت تصویب شده برای آموزش توسط متقاضی/عنوان موضوع مورد علاقه برای آموزش توسط متقاضی
        /// </summary>
        [Display(Name = "آموزشی")]
        CourseContent = 1,
        /// <summary>
        /// عنوان آموزش ضمن خدمت
        /// </summary>
        [Display(Name = "آموزش ضمن خدمت")]
        InServiceCourseType = 2,
        /// <summary>
        /// عنوان شخص 
        /// <remarks>مهندس/دکتر/یا هرعنوانی</remarks>
        /// </summary>
        [Display(Name = "شخص")]
        Person = 3,
        /// <summary>
        /// عنوان پست سازمانی
        /// </summary>
        [Display(Name = "پست سازمانی")]
        OrganizationPostTitle = 4,
        /// <summary>
        /// عنوان رشته دانشگاهی
        /// </summary>
        [Display(Name = "رشته دانشگاهی")]
        StudyField = 5,
        /// <summary>
        /// سمت متقاضی
        /// </summary>
        [Display(Name = "سمت متقاضی")]
        ApplicantPosition = 6
    }
}
