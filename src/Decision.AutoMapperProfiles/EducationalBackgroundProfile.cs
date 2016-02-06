using System;
using System.Web.Mvc;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.EducationalBackground;
using DNT.Extensions;

// ReSharper disable UseStringInterpolation
// ReSharper disable ConvertPropertyToExpressionBody

namespace Decision.AutoMapperProfiles
{
    public class EducationalBackgroundProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddEducationalBackgroundViewModel, EducationalBackground>()
                .ForMember(d => d.Description, m => m.MapFrom(a => a.Description.ToPersianContent(true)))
                .ForMember(d => d.Supervisor, m => m.MapFrom(a => a.Supervisor.ToPersianContent(true)))
                .ForMember(d => d.ThesisTopic, m => m.MapFrom(a => a.ThesisTopic.ToPersianContent()))
                .ForMember(d => d.Advisor, m => m.MapFrom(a => a.Advisor.ToPersianContent()))
                
                .IgnoreAllNonExisting();
            CreateMap<EditEducationalBackgroundViewModel, EducationalBackground>()
                 .ForMember(d => d.Description, m => m.MapFrom(a => a.Description.ToPersianContent(true)))
                .ForMember(d => d.Supervisor, m => m.MapFrom(a => a.Supervisor.ToPersianContent(true)))
                .ForMember(d => d.ThesisTopic, m => m.MapFrom(a => a.ThesisTopic.ToPersianContent()))
                .ForMember(d => d.Advisor, m => m.MapFrom(a => a.Advisor.ToPersianContent()))

                .ForMember(d => d.LastModifiedDate, m => m.MapFrom(a => DateTime.Now)).IgnoreAllNonExisting();
            CreateMap<EducationalBackground, EditEducationalBackgroundViewModel>().IgnoreAllNonExisting();

            CreateMap<EducationalBackground, EducationalBackgroundViewModel>()
                .ForMember(d => d.InstitutionName, m => m.MapFrom(s => s.Institution.Name))
                .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.Creator.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.LasModifier.UserName))
                .ForMember(s => s.StudyFieldName, m => m.MapFrom(s => s.StudyField.Name)).IgnoreAllNonExisting();
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}