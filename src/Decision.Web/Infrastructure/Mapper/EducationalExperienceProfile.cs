using System;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.EducationalExperience;
using DNT.Extensions;

// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable UseStringInterpolation

namespace Decision.AutoMapperProfiles
{
    /// <summary>
    /// تنظیمات مربوط به
    /// AutoMapper 
    /// برای کلاس سابقه آموزشی 
    /// </summary>
    public class EducationalExperienceProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddEducationalExperienceViewModel, EducationalExperience>()
                .IgnoreAllNonExisting();

            CreateMap<EditEducationalExperienceViewModel, EducationalExperience>()
                .IgnoreAllNonExisting();

            CreateMap<EducationalExperience, EditEducationalExperienceViewModel>().IgnoreAllNonExisting();

            CreateMap<EducationalExperience, EducationalExperienceViewModel>()
                .ForMember(d=>d.CreatorUserName,m=>m.MapFrom(s=>s.CreatedBy.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.ModifiedBy.UserName))
                .IgnoreAllNonExisting();
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}