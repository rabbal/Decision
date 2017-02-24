using AutoMapper;
using Decision.Common.Utility;
using Decision.DomainClasses.Users;
using Decision.Models.Administrator.Users;
using Decision.Models.Web.Account;
using Decision.Web.Infrastructure.Mapper.Extensions;

namespace Decision.Web.Infrastructure.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>()
                .ProjectUsing(src => new UserViewModel
                {
                    RegisteredOn = src.RegisteredOn.ToShamsiDateTime()
                });

            CreateMap<EditUserViewModel, User>();

            CreateMap<RegisterViewModel, User>()
                .ForMember(d => d.UserName, s => s.MapFrom(a => a.UserName.ToLowerInvariant()))
                //.ForMember(d => d.TrimmedDisplayName, s => s.MapFrom(a => a.DisplayName.TrimmedNormalizeDisplayName()))
                //.ForMember(d => d.DisplayName, s => s.MapFrom(a => a.DisplayName.NormalizeDisplayName()))
                .ForMember(d => d.Email, s => s.MapFrom(a => a.Email.FixGmailDots()));
        }
    }
}