using System;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.ViewModel.Interview;
using DNT.Extensions;

// ReSharper disable UseStringInterpolation
// ReSharper disable ConvertPropertyToExpressionBody

namespace Decision.AutoMapperProfiles
{
    /// <summary>
    /// تنظیمات مربوط به
    /// AutoMapper 
    /// برای کلاس مصاحبه 
    /// </summary>
    public class InterviewProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddInterviewViewModel, Interview>()
                 .ForMember(d => d.Body, m => m.MapFrom(a => a.Body.ToPersianContent(true)))
                 .ForMember(d => d.Brief, m => m.MapFrom(a => a.Brief.ToPersianContent(true)))
                 
                .IgnoreAllNonExisting();
            CreateMap<EditInterviewViewModel, Interview>()
                  .ForMember(d => d.Body, m => m.MapFrom(a => a.Body.ToPersianContent(true)))
                 .ForMember(d => d.Brief, m => m.MapFrom(a => a.Brief.ToPersianContent(true)))
                .ForMember(d => d.LastModifiedDate, m => m.MapFrom(a => DateTime.Now)).IgnoreAllNonExisting();
            CreateMap<Interview, EditInterviewViewModel>().IgnoreAllNonExisting();

            CreateMap<Interview, InterviewViewModel>()
                .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.Creator.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.LasModifier.UserName))
                .ForMember(d => d.InterviewerName,
                    m => m.MapFrom(s => s.Interviewer.FirstName + " " + s.Interviewer.LastName)).IgnoreAllNonExisting();
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}