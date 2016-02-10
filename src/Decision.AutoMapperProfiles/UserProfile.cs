using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses;
using Decision.DomainClasses.Entities;
using Decision.DomainClasses.Entities.Users;
using Decision.Utility;
using Decision.ViewModel.Account;
using Decision.ViewModel.User;
using EditUserViewModel = Decision.ViewModel.User.EditUserViewModel;


namespace Decision.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<User, UserViewModel>()
                
                .IgnoreAllNonExisting();

            CreateMap<AddUserViewModel, User>()
                
                .IgnoreAllNonExisting();

            CreateMap<EditUserViewModel, User>()
                .ForMember(d => d.Roles, m => m.Ignore())
                 .IgnoreAllNonExisting();

            CreateMap<User, EditUserViewModel>().ForMember(d=>d.Roles,m=>m.Ignore()).IgnoreAllNonExisting();

            CreateMap<User, SelectListItem>()
               .ForMember(d => d.Text, m => m.MapFrom(s => s.UserName))
               .ForMember(d => d.Value, m => m.MapFrom(s => s.Id)).IgnoreAllNonExisting();
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }

}
