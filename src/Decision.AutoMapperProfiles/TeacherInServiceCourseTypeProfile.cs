using System;
using System.Web.Mvc;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.ApplicantInServiceCourseType;

namespace Decision.AutoMapperProfiles
{
    /// <summary>
    /// تنظیمات مربوط به
    /// AutoMapper 
    /// برای کلاس تعداد ساعت یک نوع ضمن خدمت برای متقاضی 
    /// </summary>
    public class ApplicantInServiceCourseTypeProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddApplicantInServiceCourseTypeViewModel, ApplicantInServiceCourseType>()
                  
                .IgnoreAllNonExisting();
            CreateMap<EditApplicantInServiceCourseTypeViewModel, ApplicantInServiceCourseType>().ForMember(d => d.LastModifiedDate, m => m.MapFrom(a => DateTime.Now)).IgnoreAllNonExisting();
            CreateMap<ApplicantInServiceCourseType, EditApplicantInServiceCourseTypeViewModel>().IgnoreAllNonExisting();

            CreateMap<ApplicantInServiceCourseType, ApplicantInServiceCourseTypeViewModel>()
                .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.Creator.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.LasModifier.UserName))
                .ForMember(d => d.InServiceCourseTypeTitleName, m => m.MapFrom(s => s.InServiceCourseTypeTitle.Name));
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}