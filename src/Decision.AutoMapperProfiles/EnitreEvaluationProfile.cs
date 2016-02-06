using System;
using System.Web.Mvc;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.ViewModel.EntireEvaluation;
using DNT.Extensions;

// ReSharper disable UseStringInterpolation
// ReSharper disable ConvertPropertyToExpressionBody

namespace Decision.AutoMapperProfiles
{
    /// <summary>
    /// تنظیمات مربوط به
    /// AutoMapper 
    /// برای کلاس ارزیابی از متقاضی 
    /// </summary>
    public class EntireEvaluationProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddEntireEvaluationViewModel, EntireEvaluation>()
                .ForMember(d => d.Brief, m => m.MapFrom(a => a.Brief.ToPersianContent(true)))
                .ForMember(d => d.Content, m => m.MapFrom(a => a.Content.ToPersianContent(true)))
                .ForMember(d => d.StrongPoint, m => m.MapFrom(a => a.StrongPoint.ToPersianContent(true)))
                .ForMember(d => d.Foible, m => m.MapFrom(a => a.Foible.ToPersianContent(true)))
                .IgnoreAllNonExisting();
            CreateMap<EditEntireEvaluationViewModel, EntireEvaluation>()
                   .ForMember(d => d.Brief, m => m.MapFrom(a => a.Brief.ToPersianContent(true)))
                .ForMember(d => d.Content, m => m.MapFrom(a => a.Content.ToPersianContent(true)))
                .ForMember(d => d.StrongPoint, m => m.MapFrom(a => a.StrongPoint.ToPersianContent(true)))
                .ForMember(d => d.Foible, m => m.MapFrom(a => a.Foible.ToPersianContent(true)))
                .ForMember(d => d.LastModifiedDate, m => m.MapFrom(a => DateTime.Now)).IgnoreAllNonExisting();
            CreateMap<EntireEvaluation, EditEntireEvaluationViewModel>().IgnoreAllNonExisting();

            CreateMap<EntireEvaluation, EntireEvaluationViewModel>()
                .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.Creator.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.LasModifier.UserName))
                .ForMember(d => d.EvaluatorName, m => m.MapFrom(s => s.Evaluator.FirstName + " " + s.Evaluator.LastName)).IgnoreAllNonExisting();
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}