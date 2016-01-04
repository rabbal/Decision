using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.TeacherInfo;
using Decision.Utility;
using Decision.ViewModel.TrainingCourse;

namespace Decision.AutoMapperProfiles
{
    /// <summary>
    /// کلاس پروفایل مربوط به 
    /// Automapper
    /// دوره های کارآموزی
    /// </summary>
    public class TrainingCourseProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddTrainingCourseViewModel, TrainingCourse>()
                .ForMember(d => d.CourseCode, m => m.MapFrom(a => a.CourseCode.GetPersianNumber()))
                .IgnoreAllNonExisting();
            CreateMap<EditTrainingCourseViewModel, TrainingCourse>()
                .ForMember(d => d.CourseCode, m => m.MapFrom(a => a.CourseCode.GetPersianNumber()))
                .ForMember(d => d.LastModifiedDate, m => m.MapFrom(a => DateTime.Now)).IgnoreAllNonExisting();
            CreateMap<TrainingCourse, EditTrainingCourseViewModel>().IgnoreAllNonExisting();

            CreateMap<TrainingCourse, SelectListItem>()
              .ForMember(d => d.Text, m => m.MapFrom(s => s.CourseCode))
              .ForMember(d => d.Value, m => m.MapFrom(s => s.Id.ToString())).IgnoreAllNonExisting();

            CreateMap<TrainingCourse, TrainingCourseViewModel>()
                .ForMember(d => d.CreatorUserName, m => m.MapFrom(s => s.Creator.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.LasModifier.UserName)).IgnoreAllNonExisting();

        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}
