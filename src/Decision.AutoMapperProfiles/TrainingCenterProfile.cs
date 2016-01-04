using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.TeacherInfo;
using Decision.ViewModel.TrainingCenter;
using DNT.Extensions;

namespace Decision.AutoMapperProfiles
{
    /// <summary>
    /// تنظیمات مربوط به 
    /// AutoMapper
    /// برای کلاس مراکز کارآموزی
    /// </summary>
    public class TrainingCenterProfile:Profile
    {
          protected override void Configure()
        {
            CreateMap<AddTrainingCenterViewModel, TrainingCenter>()
                .ForMember(d => d.CenterName, m => m.MapFrom(a => a.CenterName.ToPersianContent(true)))
                .ForMember(d => d.Description, m => m.MapFrom(a => a.Description.ToPersianContent(true)))
                .ForMember(d => d.PhoneNumber1, m => m.MapFrom(a => a.PhoneNumber1.ToPersianContent(true)))
                .ForMember(d => d.PhoneNumber2, m => m.MapFrom(a => a.PhoneNumber2.ToPersianContent(true)))
                .ForMember(d => d.Location, m => m.MapFrom(a => a.Location.ToPersianContent(true)))
                .IgnoreAllNonExisting();

            CreateMap<EditTrainingCenterViewModel, TrainingCenter>()
                .ForMember(d => d.CenterName, m => m.MapFrom(a => a.CenterName.ToPersianContent(true)))
                .ForMember(d => d.Description, m => m.MapFrom(a => a.Description.ToPersianContent(true)))
                .ForMember(d => d.PhoneNumber1, m => m.MapFrom(a => a.PhoneNumber1.ToPersianContent(true)))
                .ForMember(d => d.PhoneNumber2, m => m.MapFrom(a => a.PhoneNumber2.ToPersianContent(true)))
                .ForMember(d => d.Location, m => m.MapFrom(a => a.Location.ToPersianContent(true)))
                .ForMember(d => d.LastModifiedDate, m => m.MapFrom(a => DateTime.Now)).IgnoreAllNonExisting();
            CreateMap<TrainingCenter, EditTrainingCenterViewModel>().IgnoreAllNonExisting();

            CreateMap<TrainingCenter, SelectListItem>()
            .ForMember(d => d.Text, m => m.MapFrom(s => s.CenterName))
            .ForMember(d => d.Value, m => m.MapFrom(s => s.Id.ToString())).IgnoreAllNonExisting();

              CreateMap<TrainingCenter, TrainingCenterViewModel>()
                  .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.Creator.UserName))
                  .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.LasModifier.UserName))
                  .IgnoreAllNonExisting();
        }

        public override string ProfileName { get { return GetType().Name; }}
    }
   
}
