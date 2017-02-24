using System;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.ViewModel.Interview;
using DNT.Extensions;
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
                .IgnoreAllNonExisting();
            CreateMap<EditInterviewViewModel, Interview>()
                  .ForMember(d => d.Body, m => m.MapFrom(a => a.Body.ToPersianContent(true)))
                .IgnoreAllNonExisting();
            CreateMap<Interview, EditInterviewViewModel>().IgnoreAllNonExisting();

            CreateMap<Interview, InterviewViewModel>()
                .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.CreatedBy.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.ModifiedBy.UserName))
                .IgnoreAllNonExisting();
        }

        public override string ProfileName => GetType().Name;
    }
}