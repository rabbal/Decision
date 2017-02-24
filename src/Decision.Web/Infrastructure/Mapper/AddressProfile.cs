using System;

using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.Utility;
using Decision.ViewModel.Address;
using DNT.Extensions;

namespace Decision.AutoMapperProfiles
{
    /// <summary>
    /// تنظیمات مرتبط با 
    /// automapper 
    /// برای کلاس آدرس
    /// </summary>
    public class AddressProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddAddressViewModel, Address>()
                .ForMember(d => d.Location, m => m.MapFrom(a => a.Location.ToPersianContent(true)))
                .ForMember(d => d.PhoneNumber, m => m.MapFrom(a => a.PhoneNumber.GetPersianNumber()))
                .IgnoreAllNonExisting();

            CreateMap<EditAddressViewModel, Address>()
                 .ForMember(d => d.Location, m => m.MapFrom(a => a.Location.ToPersianContent(true)))
                .ForMember(d => d.PhoneNumber, m => m.MapFrom(a => a.PhoneNumber.GetPersianNumber()))
                .IgnoreAllNonExisting();

            CreateMap<Address, EditAddressViewModel>().IgnoreAllNonExisting();

            CreateMap<Address, AddressViewModel>()
                .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.CreatedBy.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.ModifiedBy.UserName)).IgnoreAllNonExisting();

        }

        public override string ProfileName { get { return GetType().Name; } }
    }
}
