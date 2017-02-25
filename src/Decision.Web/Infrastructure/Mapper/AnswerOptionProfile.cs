namespace Decision.Web.Infrastructure.Mapper
{
    /// <summary>
    /// تنظیمات مربوط به
    /// AutoMapper 
    /// برای گزینه های سوال چند گزینه ای 
    /// </summary>
    public class AnswerOptionProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddAnswerOptionViewModel, AnswerOption>().IgnoreAllNonExisting();
            CreateMap<EditAnswerOptionViewModel, AnswerOption>().IgnoreAllNonExisting();
            CreateMap<AnswerOption, EditAnswerOptionViewModel>().IgnoreAllNonExisting();

            CreateMap<AnswerOption, AnswerOptionViewModel>().IgnoreAllNonExisting();
        }

        public override string ProfileName => GetType().Name;
    }
}