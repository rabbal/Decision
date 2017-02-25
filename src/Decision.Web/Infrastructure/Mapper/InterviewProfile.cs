namespace Decision.Web.Infrastructure.Mapper
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