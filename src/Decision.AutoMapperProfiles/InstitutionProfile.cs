using System;
using System.Web.Mvc;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.Institution;
using DNT.Extensions;

namespace Decision.AutoMapperProfiles
{
    /// <summary>
    /// تنظیمات مربوط به
    /// AutoMapper 
    /// برای کلاس موسسه آموزشی 
    /// </summary>
    public class InstitutionProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddInstitutionViewModel, Institution>()
                   .ForMember(d => d.Description, m => m.MapFrom(a => a.Description.ToPersianContent(true)))
                   .ForMember(d => d.Name, m => m.MapFrom(a => a.Name.ToPersianContent(true)))
                .IgnoreAllNonExisting();

            CreateMap<EditInstitutionViewModel, Institution>()
                 .ForMember(d => d.Description, m => m.MapFrom(a => a.Description.ToPersianContent(true)))
                   .ForMember(d => d.Name, m => m.MapFrom(a => a.Name.ToPersianContent(true)))
                .ForMember(d => d.LastModifiedDate, m => m.MapFrom(a => DateTime.Now)).IgnoreAllNonExisting();
            CreateMap<Institution, EditInstitutionViewModel>().IgnoreAllNonExisting();

            CreateMap<Institution, SelectListItem>()
                .ForMember(d => d.Text, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.Value, m => m.MapFrom(s => s.Id.ToString())).IgnoreAllNonExisting();

            CreateMap<Institution, InstitutionViewModel>()
                .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.Creator.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.LasModifier.UserName)).IgnoreAllNonExisting();
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}