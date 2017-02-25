using System.Collections.Generic;

namespace Decision.Web.Infrastructure.Mapper
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
