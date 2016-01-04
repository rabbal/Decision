using System;
using Decision.DomainClasses.Entities.TeacherInfo;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.EducationalExperience
{
    /// <summary>
    /// کلاسی برای کپسوله سازی اطلاعات جستجو و مرتب سازی سابقه های آموزشی
    /// </summary>
    public class EducationalExperienceSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// نوع سابقه آموزشی
        /// </summary>
        public EducationalExperienceType? Type { get; set; }

        /// <summary>
        /// آی دی استاد مرتبط با سابقه تدریس
        /// </summary>
        public  Guid TeacherId { get; set; }
    }
}