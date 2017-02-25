namespace Decision.Web.Infrastructure.Mapper
{
    /// <summary>
    /// تنظیمات مربوط به
    /// AutoMapper 
    /// برای کلاس سوال 
    /// </summary>
    public class QuestionProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddQuestionViewModel, Question>().IgnoreAllNonExisting();
            CreateMap<EditQuestionViewModel, Question>().IgnoreAllNonExisting();
            CreateMap<Question, EditQuestionViewModel>().IgnoreAllNonExisting();

            CreateMap<Question, QuestionViewModel>()
                .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.CreatedBy.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.ModifiedBy.UserName))
                .ForMember(d => d.Options, m => m.MapFrom(s => s.AnswerOptions))
                .IgnoreAllNonExisting();
        }

        public override string ProfileName => GetType().Name;
    }
}