using System;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.Article;

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

         
            CreateMap<Article, ArticleViewModel>()
                .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.CreatedBy.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.ModifiedBy.UserName))
                .IgnoreAllNonExisting();

        }

        public override string ProfileName => GetType().Name;
    }
}