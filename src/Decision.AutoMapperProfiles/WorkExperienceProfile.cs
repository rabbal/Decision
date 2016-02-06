using System;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.WorkExperience;
using DNT.Extensions;

namespace Decision.AutoMapperProfiles
{
    /// <summary>
    /// تنظیمات مربوط به
    /// AutoMapper 
    /// برای کلاس سابقه کاری 
    /// </summary>
    public class WorkExperienceProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddWorkExperienceViewModel, WorkExperience>()
                .ForMember(d => d.OfficeName, m => m.MapFrom(a => a.OfficeName.ToPersianContent(true)))
                .IgnoreAllNonExisting();

            CreateMap<EditWorkExperienceViewModel, WorkExperience>()
                .ForMember(d => d.OfficeName, m => m.MapFrom(a => a.OfficeName.ToPersianContent(true)))
                .IgnoreAllNonExisting();

            CreateMap<WorkExperience, EditWorkExperienceViewModel>().IgnoreAllNonExisting();

            CreateMap<WorkExperience, WorkExperienceViewModel>()
                .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.CreatedBy.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.ModifiedBy.UserName)).IgnoreAllNonExisting();
        }
    }
}