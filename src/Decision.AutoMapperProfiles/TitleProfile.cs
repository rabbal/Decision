using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.Common;
using Decision.ViewModel.Title;
using DNT.Extensions;

namespace Decision.AutoMapperProfiles
{
    /// <summary>
    /// پروفایل مربوط به 
    /// Automapper 
    /// کلاس عنوان
    /// </summary>
   public  class TitleProfile:Profile
    {
        protected override void Configure()
        {
            CreateMap<AddTitleViewModel, Title>()
                  .ForMember(d => d.Name, m => m.MapFrom(a => a.Name.GetPersianNumbers()))
                .IgnoreAllNonExisting();
            CreateMap<EditTitleViewModel, Title>()
                .ForMember(d => d.Name, m => m.MapFrom(a => a.Name.GetPersianNumbers()))
                .ForMember(d => d.LastModifiedDate, m => m.MapFrom(a => DateTime.Now))
                .ForMember(d => d.Type, m => m.Ignore())
                .ForMember(d => d.Category, m => m.Ignore()).IgnoreAllNonExisting();

            CreateMap<Title, EditTitleViewModel>().IgnoreAllNonExisting();

            CreateMap<Title, SelectListItem>()
                .ForMember(d => d.Text, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.Value, m => m.MapFrom(s => s.Id.ToString())).IgnoreAllNonExisting();

            CreateMap<Title, TitleViewModel>()
                .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.Creator.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.LasModifier.UserName));
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}
