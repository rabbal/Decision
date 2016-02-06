using System;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.Article;
using Decision.ViewModel.ArticleEvaluation;
using DNT.Extensions;

// ReSharper disable UseStringInterpolation

namespace Decision.AutoMapperProfiles
{
    public class ArticleProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddArticleViewModel, Article>()
                .ForMember(d => d.Brief, m => m.MapFrom(a => a.Brief.ToPersianContent(true)))
                .IgnoreAllNonExisting();

            CreateMap<EditArticleViewModel, Article>()
                .ForMember(d => d.Brief, m => m.MapFrom(a => a.Brief.ToPersianContent(true)))
                .IgnoreAllNonExisting();

            CreateMap<Article, EditArticleViewModel>()
                .IgnoreAllNonExisting();

            CreateMap<Article, ArticleDetails>()
                .ForMember(d => d.ApplicantFullName, m => m.MapFrom(s => s.Applicant.FirstName + " " + s.Applicant.LastName))
                .ForMember(d => d.TotalScore, m => m.UseValue(100f)).IgnoreAllNonExisting();

            CreateMap<Article, ArticleViewModel>()
                .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.CreatedBy.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.ModifiedBy.UserName))
                .IgnoreAllNonExisting();

        }

        public override string ProfileName => GetType().Name;
    }
}