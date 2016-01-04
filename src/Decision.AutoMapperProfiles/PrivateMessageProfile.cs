﻿using System;
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
                .ForMember(d => d.Attachments, m => m.MapFrom(s => s.Attachments))
                .ForMember(d => d.SenderUserName, m => m.MapFrom(s => s.Sender.UserName))
                .IgnoreAllNonExisting();

            CreateMap<List<Message>, List<MessageViewModel>>().IgnoreAllNonExisting();

            CreateMap<MessageAttachment, AttachmentViewModel>().IgnoreAllNonExisting();
           
            CreateMap<Conversation, InBoxViewModel>()
                .ForMember(d => d.SenderUserName, m => m.MapFrom(s => s.Sender.UserName)).IgnoreAllNonExisting();

            CreateMap<Conversation, OutBoxViewModel>()
                .ForMember(d => d.RecieverUserName, m => m.MapFrom(s => s.Receiver.UserName)).IgnoreAllNonExisting();
        }

        public override string ProfileName => GetType().Name;
    }
}