using System;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.ResearchExperience
{
    /// <summary>
    /// ویومدل جست و جوی سابقه های پژوهشی
    /// </summary>
    public class ResearchExperienceSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// آی دی استاد مربوط به سابقه های پژوهشی
        /// </summary>
        public Guid TeacherId { get; set; }
    }
}