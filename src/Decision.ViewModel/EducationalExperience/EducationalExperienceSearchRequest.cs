using System;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.EducationalExperience
{
    /// <summary>
    /// کلاسی برای کپسوله سازی اطلاعات جستجو و مرتب سازی سابقه های آموزشی
    /// </summary>
    public class EducationalExperienceSearchRequest : BaseSearchRequest
    {

        /// <summary>
        /// آی دی متقاضی مرتبط با سابقه تدریس
        /// </summary>
        public  Guid ApplicantId { get; set; }
    }
}