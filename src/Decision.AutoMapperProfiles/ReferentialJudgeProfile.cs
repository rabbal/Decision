using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.ReferentialApplicant;

// ReSharper disable UseStringInterpolation
// ReSharper disable ConvertPropertyToExpressionBody

namespace Decision.AutoMapperProfiles
{
    /// <summary>
    /// تنظیمات مربوط به
    /// AutoMapper 
    /// برای کلاس ارجاع متقاضی 
    /// </summary>
    public class ReferentialApplicantProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddReferentialApplicantViewModel, ReferentialApplicant>().IgnoreAllNonExisting();
            CreateMap<EditReferentialApplicantViewModel, ReferentialApplicant>().IgnoreAllNonExisting();
            CreateMap<ReferentialApplicant, EditReferentialApplicantViewModel>().IgnoreAllNonExisting();
            

            CreateMap<ReferentialApplicant, ReferentialApplicantViewModel>()
                .ForMember(d => d.ReferencedFromName, m => m.MapFrom(s => s.ReferencedFrom.UserName))
                .ForMember(d => d.ReferencedToName, m => m.MapFrom(s => s.ReferencedTo.UserName))
                .ForMember(d => d.ApplicantName, m => m.MapFrom(s => string.Format("{0} {1}", s.Applicant.FirstName, s.Applicant.LastName)));
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}