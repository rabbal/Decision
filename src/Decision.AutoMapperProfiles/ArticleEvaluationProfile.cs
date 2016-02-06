using System;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.ViewModel.ArticleEvaluation;
using DNT.Extensions;

// ReSharper disable UseStringInterpolation
// ReSharper disable ConvertPropertyToExpressionBody

namespace Decision.AutoMapperProfiles
{
    /// <summary>
    /// تنظیمات مربوط به
    /// AutoMapper 
    /// برای کلاس ارزیابی ار مقاله متقاضی 
    /// </summary>
    public class ArticleEvaluationProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddArticleEvaluationViewModel, ArticleEvaluation>()
                .ForMember(d => d.Brief, m => m.MapFrom(a => a.Brief.ToPersianContent(true)))
                .ForMember(d => d.Content, m => m.MapFrom(a => a.Content.ToPersianContent(true)))
                .ForMember(d => d.Foible, m => m.MapFrom(a => a.Foible.ToPersianContent(true)))
                .IgnoreAllNonExisting();

            CreateMap<ArticleEvaluation, ArticleEvaluationViewModel>()
                .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.Creator.UserName))
                .ForMember(d => d.Score, m => m.UseValue(Convert.ToDecimal(100)))
                .ForMember(d => d.EvaluatorName, m => m.MapFrom(s => s.Evaluator.FirstName + " " + s.Evaluator.LastName))
                .IgnoreAllNonExisting();

        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}