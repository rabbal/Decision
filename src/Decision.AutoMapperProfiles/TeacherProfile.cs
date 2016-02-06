using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.Utility;
using Decision.ViewModel.Home;
using Decision.ViewModel.Applicant;
using DNT.Extensions;

namespace Decision.AutoMapperProfiles
{
    public class ApplicantProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddApplicantViewModel, Applicant>()
                  .ForMember(d => d.AccountNumber, m => m.MapFrom(a => a.AccountNumber.GetPersianNumber()))
                 .ForMember(d => d.BankBranch, m => m.MapFrom(a => a.BankBranch.ToPersianContent(true)))
                 .ForMember(d => d.BankName, m => m.MapFrom(a => a.BankName.ToPersianContent(true)))
                 .ForMember(d => d.BirthCertificateNumber, m => m.MapFrom(a => a.BirthCertificateNumber.GetPersianNumber()))
                 .ForMember(d => d.NationalCode, m => m.MapFrom(a => a.NationalCode))
                .IgnoreAllNonExisting();

            CreateMap<EditApplicantViewModel, Applicant>()
                  .ForMember(d => d.AccountNumber, m => m.MapFrom(a => a.AccountNumber.GetPersianNumber()))
                 .ForMember(d => d.BankBranch, m => m.MapFrom(a => a.BankBranch.ToPersianContent(true)))
                 .ForMember(d => d.BankName, m => m.MapFrom(a => a.BankName.ToPersianContent(true)))
                 .ForMember(d => d.BirthCertificateNumber, m => m.MapFrom(a => a.BirthCertificateNumber.GetPersianNumber()))
                 .ForMember(d => d.NationalCode, m => m.MapFrom(a => a.NationalCode))
                .ForMember(d => d.LastModifiedDate, m => m.MapFrom(a => DateTime.Now)).IgnoreAllNonExisting();

            CreateMap<Applicant, EditApplicantViewModel>().IgnoreAllNonExisting();

            CreateMap<Applicant, ApplicantDetailsViewModel>()
                .ForMember(a => a.TrainingCourseDetails, m => m.Ignore())
              .ForMember(a => a.PositionName, m => m.MapFrom(s => s.Position.Name))
              .ForMember(a => a.CreatorUserName, m => m.MapFrom(s => s.Creator.UserName))
              .ForMember(a => a.LastModifierUserName, m => m.MapFrom(s => s.LasModifier.UserName))
              .ForMember(a => a.ApproveByName, m => m.MapFrom(s => s.ApproveBy.UserName))
              .ForMember(d => d.FullName, m => m.MapFrom(s => s.FirstName + " " + s.LastName)).IgnoreAllNonExisting();

            CreateMap<Applicant, ApplicantViewModel>()
                .ForMember(a => a.PositionName, m => m.MapFrom(s => s.Position.Name))
                .ForMember(a => a.CreatorUserName, m => m.MapFrom(s => s.Creator.UserName))
                .ForMember(a => a.LastModifierUserName, m => m.MapFrom(s => s.LasModifier.UserName))
                .ForMember(a => a.ApproveByName, m => m.MapFrom(s => s.ApproveBy.UserName))
                .ForMember(d => d.FullName, m => m.MapFrom(s => s.FirstName + " " + s.LastName)).IgnoreAllNonExisting();

            CreateMap<Applicant, ReferApplicantViewModel>()
               .ForMember(a => a.PositionName, m => m.MapFrom(s => s.Position.Name))
               .ForMember(d => d.FullName, m => m.MapFrom(s => s.FirstName + " " + s.LastName)).IgnoreAllNonExisting();

            CreateMap<Applicant, NewAddedApplicantViewModel>()
              .ForMember(d => d.FullName, m => m.MapFrom(s => s.FirstName + " " + s.LastName)).IgnoreAllNonExisting();

            CreateMap<Applicant, ApplicantWithTopScoreViewModel>()
            .ForMember(d => d.FullName, m => m.MapFrom(s => s.FirstName + " " + s.LastName)).IgnoreAllNonExisting();


        }

        public override string ProfileName => GetType().Name;
    }
}
