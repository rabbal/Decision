using System;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.TeacherInfo;
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
                .ForMember(d => d.Description, m => m.MapFrom(a => a.Description.ToPersianContent(true)))
                .IgnoreAllNonExisting();
            CreateMap<EditEducationalExperienceViewModel, EducationalExperience>()
                .ForMember(d => d.Description, m => m.MapFrom(a => a.Description.ToPersianContent(true)))
                .ForMember(d => d.LastModifiedDate, m => m.MapFrom(a => DateTime.Now)).IgnoreAllNonExisting();
            CreateMap<EducationalExperience, EditEducationalExperienceViewModel>().IgnoreAllNonExisting();

            CreateMap<EducationalExperience, EducationalExperienceViewModel>()
                .ForMember(d=>d.CreatorUserName,m=>m.MapFrom(s=>s.Creator.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.LasModifier.UserName))
                .ForMember(d => d.TitleName, m => m.MapFrom(s => s.Title.Name)).IgnoreAllNonExisting();
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}