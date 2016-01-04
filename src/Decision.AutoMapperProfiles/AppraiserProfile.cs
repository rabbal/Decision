using System;
using System.Web.Mvc;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.ViewModel.Appraiser;
using DNT.Extensions;

namespace Decision.AutoMapperProfiles
{
    /// <summary>
    /// تنظیمات مربوط به
    /// AutoMapper 
    /// برای کلاس ارزیاب 
    /// </summary>
    public class AppraiserProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddAppraiserViewModel, Appraiser>()
                .ForMember(d => d.LastName, m => m.MapFrom(a => a.LastName.ToPersianContent(true)))
                .ForMember(d => d.FirstName, m => m.MapFrom(a => a.FirstName.ToPersianContent(true)))
                .ForMember(d => d.CellPhone, m => m.MapFrom(a => a.CellPhone.ToPersianContent(true)))
                
                .IgnoreAllNonExisting();
            CreateMap<EditAppraiserViewModel, Appraiser>()
                .ForMember(d => d.LastName, m => m.MapFrom(a => a.LastName.ToPersianContent(true)))
                .ForMember(d => d.FirstName, m => m.MapFrom(a => a.FirstName.ToPersianContent(true)))
                .ForMember(d => d.CellPhone, m => m.MapFrom(a => a.CellPhone.ToPersianContent(true)))
                .ForMember(d => d.LastModifiedDate, m => m.MapFrom(a => DateTime.Now)).IgnoreAllNonExisting();
            CreateMap<Appraiser, EditAppraiserViewModel>().IgnoreAllNonExisting();

            CreateMap<Appraiser, SelectListItem>()
               .ForMember(d => d.Text, m => m.MapFrom(s => s.FirstName +" " +s.LastName))
               .ForMember(d => d.Value, m => m.MapFrom(s => s.Id.ToString())).IgnoreAllNonExisting();

            CreateMap<Appraiser, AppraiserViewModel>()
                .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.Creator.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.LasModifier.UserName))
                .ForMember(d => d.AppraiserTitleName, m => m.MapFrom(s => s.AppraiserTitle.Name));
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}