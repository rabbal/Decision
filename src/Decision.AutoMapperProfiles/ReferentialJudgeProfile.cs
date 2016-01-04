using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.TeacherInfo;
using Decision.ViewModel.ReferentialTeacher;

// ReSharper disable UseStringInterpolation
// ReSharper disable ConvertPropertyToExpressionBody

namespace Decision.AutoMapperProfiles
{
    /// <summary>
    /// تنظیمات مربوط به
    /// AutoMapper 
    /// برای کلاس ارجاع استاد 
    /// </summary>
    public class ReferentialTeacherProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddReferentialTeacherViewModel, ReferentialTeacher>().IgnoreAllNonExisting();
            CreateMap<EditReferentialTeacherViewModel, ReferentialTeacher>().IgnoreAllNonExisting();
            CreateMap<ReferentialTeacher, EditReferentialTeacherViewModel>().IgnoreAllNonExisting();
            

            CreateMap<ReferentialTeacher, ReferentialTeacherViewModel>()
                .ForMember(d => d.ReferencedFromName, m => m.MapFrom(s => s.ReferencedFrom.UserName))
                .ForMember(d => d.ReferencedToName, m => m.MapFrom(s => s.ReferencedTo.UserName))
                .ForMember(d => d.TeacherName, m => m.MapFrom(s => string.Format("{0} {1}", s.Teacher.FirstName, s.Teacher.LastName)));
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}