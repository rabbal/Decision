using System;
using System.Web.Mvc;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.TeacherInfo;
using Decision.ViewModel.TeacherInServiceCourseType;

namespace Decision.AutoMapperProfiles
{
    /// <summary>
    /// تنظیمات مربوط به
    /// AutoMapper 
    /// برای کلاس تعداد ساعت یک نوع ضمن خدمت برای استاد 
    /// </summary>
    public class TeacherInServiceCourseTypeProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddTeacherInServiceCourseTypeViewModel, TeacherInServiceCourseType>()
                  
                .IgnoreAllNonExisting();
            CreateMap<EditTeacherInServiceCourseTypeViewModel, TeacherInServiceCourseType>().ForMember(d => d.LastModifiedDate, m => m.MapFrom(a => DateTime.Now)).IgnoreAllNonExisting();
            CreateMap<TeacherInServiceCourseType, EditTeacherInServiceCourseTypeViewModel>().IgnoreAllNonExisting();

            CreateMap<TeacherInServiceCourseType, TeacherInServiceCourseTypeViewModel>()
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