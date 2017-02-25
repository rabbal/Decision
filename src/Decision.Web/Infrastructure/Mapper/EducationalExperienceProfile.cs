// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable UseStringInterpolation

namespace Decision.Web.Infrastructure.Mapper
{
    /// <summary>
    /// تنظیمات مربوط به
    /// AutoMapper 
    /// برای کلاس سابقه آموزشی 
    /// </summary>
    public class EducationalExperienceProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddEducationalExperienceViewModel, EducationalExperience>()
                .IgnoreAllNonExisting();

            CreateMap<EditEducationalExperienceViewModel, EducationalExperience>()
                .IgnoreAllNonExisting();

            CreateMap<EducationalExperience, EditEducationalExperienceViewModel>().IgnoreAllNonExisting();

            CreateMap<EducationalExperience, EducationalExperienceViewModel>()
                .ForMember(d=>d.CreatorUserName,m=>m.MapFrom(s=>s.CreatedBy.UserName))
                .ForMember(d => d.LastModifierUserName, m => m.MapFrom(s => s.ModifiedBy.UserName))
                .IgnoreAllNonExisting();
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}