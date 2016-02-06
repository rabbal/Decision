using System;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.ViewModel.AnswerOption;

namespace Decision.AutoMapperProfiles
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