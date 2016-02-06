using System;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.WorkExperience
{
    /// <summary>
    /// ویومدل جست و جوی سابقه کاری
    /// </summary>
    public class WorkExperienceSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// آی دی متقاضی مربوط به سابقه کاری
        /// </summary>
        public Guid ApplicantId { get; set; }
    }
}