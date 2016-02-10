using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Decision.AutoMapperProfiles.Extentions;
using Decision.DomainClasses.Entities.PrivateMessage;
using Decision.ViewModel.PrivateMessage;

namespace Decision.AutoMapperProfiles
{
   

    public class PrivateMessageProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Message, MessageViewModel>()
                .ForMember(d => d.SenderUserName, m => m.MapFrom(s => s.Sender.UserName))
                .IgnoreAllNonExisting();

            CreateMap<List<Message>, List<MessageViewModel>>().IgnoreAllNonExisting();
            
            CreateMap<Conversation, InBoxViewModel>()
               .IgnoreAllNonExisting();

            CreateMap<Conversation, OutBoxViewModel>()
                .IgnoreAllNonExisting();
        }

        public override string ProfileName => GetType().Name;
    }
}
