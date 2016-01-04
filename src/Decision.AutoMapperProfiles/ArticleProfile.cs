using System;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.TeacherInfo;
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
                .ForMember(d => d.Content, m => m.MapFrom(a => a.Content.ToPersianContent(true)))
                .ForMember(d => d.Code, m => m.MapFrom(a => a.Code.GetPersianNumbers()))
                .IgnoreAllNonExisting();
            CreateMap<EditArticleViewModel, Article>()
                  .ForMember(d => d.Brief, m => m.MapFrom(a => a.Brief.ToPersianContent(true)))
                .ForMember(d => d.Content, m => m.MapFrom(a => a.Content.ToPersianContent(true)))
                .ForMember(d => d.Code, m => m.MapFrom(a => a.Code.GetPersianNumbers()))
                .ForMember(d => d.LastModifiedDate, m => m.MapFrom(a => DateTime.Now)).IgnoreAllNonExisting();
            CreateMap<Article, EditArticleViewModel>()
                .IgnoreAllNonExisting();
            CreateMap<Article, ArticleDetails>()
                .ForMember(d => d.TeacherFullName, m => m.MapFrom(s => s.Teacher.FirstName + " " + s.Teacher.LastName))
                .ForMember(d => d.ArticleCode, m => m.MapFrom(s => s.Code))
                .ForMember(d => d.TotalScore, m => m.UseValue(100f)).IgnoreAllNonExisting();

            CreateMap<Article, ArticleViewModel>()
                .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.Creator.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.LasModifier.UserName))
                .IgnoreAllNonExisting();

        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}