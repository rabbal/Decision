using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.Common.HtmlCleaner;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.PrivateMessage;
using Decision.ServiceLayer.Contracts.PrivateMessage;
using Decision.ServiceLayer.Contracts.Users;
using Decision.Utility;
using Decision.ViewModel.PrivateMessage;
using EntityFramework.Extensions;

namespace Decision.ServiceLayer.EFServiecs.PrivateMessage
{
    public class MessageService : IMessageService
    {
        #region Fields

        private readonly IMappingEngine _mappingEngine;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<Conversation> _conversations;
        private readonly IDbSet<Message> _messages;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor

        public MessageService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _conversations = _unitOfWork.Set<Conversation>();
            _messages = _unitOfWork.Set<Message>();
            _mappingEngine = mappingEngine;

        }
        #endregion

        #region Create
        public void Create(AddConversationViewModel viewModel)
        {
            var currentUserId = _userManager.GetCurrentUserId();
            var sentOn = DateTime.Now;
            var conversation = new Conversation
            {
                Subject = viewModel.Subject,
                SenderId = currentUserId,
                ReceiverId = viewModel.ReciverId,
                SentOn = sentOn
            };
            _conversations.Add(conversation);
            var message = new Message
            {
                SenderId = currentUserId,
                Body = viewModel.Body.ToSafeHtml(),
                SentOn = sentOn,
                ConversationId = conversation.Id
            };
            _messages.Add(message);
        }

        public void Create(AddMessageViewModel viewModel)
        {
            var newMessageToConversation = new Message
            {
                ConversationId = viewModel.ConversationId,
                Body = viewModel.Body.ToSafeHtml(),
                ParentId = viewModel.ParentId,
                SentOn = DateTime.Now,
                SenderId = _userManager.GetCurrentUserId()
            };
            _messages.Add(newMessageToConversation);
        }

        #endregion

        #region Get Conversations
        public async Task<IEnumerable<InBoxViewModel>> GetInBox(int page)
        {
            var currentUserId = _userManager.GetCurrentUserId();
            var resultsToSkip = (page - 1)*5;
            return
                await
                    _conversations.Where(a => a.ReceiverId == currentUserId)
                        .Include(a => a.Sender).AsNoTracking()
                        .ProjectTo<InBoxViewModel>(_mappingEngine)
                        .OrderByDescending(a => a.SentOn)
                        .Skip(()=>resultsToSkip)
                        .Take(5)
                        .ToListAsync();
        }

        public async Task<IEnumerable<OutBoxViewModel>> GetOutBox(int page)
        {
            var currentUserId = _userManager.GetCurrentUserId();
            var resultsToSkip = (page - 1) * 5;
            return await _conversations.Where(a => a.SenderId == currentUserId)
                .Include(a => a.Receiver)
                .AsNoTracking()
                .ProjectTo<OutBoxViewModel>(_mappingEngine)
                .OrderByDescending(a => a.SentOn)
                .Skip(() => resultsToSkip)
                .Take(5)
                .ToListAsync();
        }
        #endregion

        #region Get Messages
        public async Task<MessageListViewModel> GetMessages(Guid conversationId)
        {
            if (!await CheckAccess(conversationId))
                return null;

            await MakeAsSeen(conversationId);
           
            var messages =
                await
                    _messages.Where(a => a.ConversationId == conversationId)
                        .Include(a=>a.Sender)
                        .ToListAsync();
            
            return new MessageListViewModel
            {
                Messages = messages,
                AddMessageViewModel = new AddMessageViewModel
                {
                    ConversationId = conversationId,
                    ParentId = messages.First().Id
                }
            };
        }
        #endregion

       

        #region CheckAccess
        public Task<bool> CheckAccess(Guid conversationId)
        {
            var userId = _userManager.GetCurrentUserId();
            return
                _conversations.AnyAsync(
                    a =>
                        (a.SenderId == userId && a.Id == conversationId) ||
                        ((a.ReceiverId == userId && a.Id == conversationId)));
        }
        #endregion

        #region DeleteAsync
        public Task DeleteAsync(Guid value)
        {
            return _conversations.Where(a => a.Id == value).DeleteAsync();
        }

        #endregion

        #region MakeAsSeen
        private Task MakeAsSeen(Guid conversationId)
        {
            var currentUser = _userManager.GetCurrentUserId();
            return _conversations.Where(a => a.Id == conversationId & a.SenderId != currentUser).UpdateAsync(a => new Conversation { IsRead = true });
        }

        public long NewMessgesCount()
        {
            var currentUserId = _userManager.GetCurrentUserId();
            return _conversations.Where(a => a.ReceiverId == currentUserId && !a.IsRead).LongCount();
        }
        #endregion
    }
}
