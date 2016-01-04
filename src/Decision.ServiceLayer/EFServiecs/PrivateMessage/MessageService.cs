using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.Common.Helpers.Extentions;
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
        private readonly IDbSet<MessageAttachment> _attachments;
        #endregion

        #region Ctor

        public MessageService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _conversations = _unitOfWork.Set<Conversation>();
            _messages = _unitOfWork.Set<Message>();
            _attachments = _unitOfWork.Set<MessageAttachment>();
            _mappingEngine = mappingEngine;

        }
        #endregion

        #region Create
        public void Create(AddConversationViewModel viewModel)
        {
            var conversation = new Conversation
            {
                Subject = viewModel.Subject,
                SenderId = _userManager.GetCurrentUserId(),
                ReceiverId = viewModel.ReciverId,
                StartDate = DateTime.Now
            };

            _conversations.Add(conversation);

            var message = new Message
            {
                SenderId = _userManager.GetCurrentUserId(),
                Content = viewModel.Content.ToSafeHtml(),
                SendDate = DateTime.Now,
                ConversationId = conversation.Id
            };
            _messages.Add(message);
            if (viewModel.Attachments == null || !viewModel.Attachments.Any()) return;

            foreach (var newFile in viewModel.Attachments.Where(attachment => attachment.HasFile()).Select(attachment => new MessageAttachment
            {
                ContentType = attachment.ContentType,
                Extension = Path.GetExtension(attachment.FileName),
                FriendlyName = Path.GetFileNameWithoutExtension(attachment.FileName),
                Data = attachment.InputStream.ConvertToByteArrary(attachment.ContentLength),
                MessageId = message.Id
            }))
            {
                _attachments.Add(newFile);
            }
        }

        public void Create(AddMessageViewModel viewModel)
        {
            var newMessageToConversation = new Message
            {
                ConversationId = viewModel.ConversationId,
                Content = viewModel.Content.ToSafeHtml(),
                ReplyId = viewModel.ReplyId,
                SendDate = DateTime.Now,
                SenderId = _userManager.GetCurrentUserId()
            };
            _messages.Add(newMessageToConversation);

            if (viewModel.Attachments == null || !viewModel.Attachments.Any()) return;

            foreach (var newFile in viewModel.Attachments.Where(attachment => attachment.HasFile()).Select(attachment => new MessageAttachment
            {
                ContentType = attachment.ContentType,
                Extension = Path.GetExtension(Path.GetFileName(attachment.FileName)),
                FriendlyName = Path.GetFileNameWithoutExtension(attachment.FileName),
                Data = attachment.InputStream.ConvertToByteArrary(attachment.ContentLength),
                MessageId = newMessageToConversation.Id
            }))
            {
                _attachments.Add(newFile);
            }
        }

        #endregion

        #region Get Conversations
        public async Task<IEnumerable<InBoxViewModel>> GetInBox(int page)
        {
            var currentUserId = _userManager.GetCurrentUserId();
            return
                await
                    _conversations.Where(a => a.ReceiverId == currentUserId)
                        .Include(a => a.Sender).AsNoTracking()
                        .ProjectTo<InBoxViewModel>(_mappingEngine)
                        .OrderByDescending(a => a.StartDate)
                        .Skip((page - 1) * 5)
                        .Take(5)
                        .ToListAsync();
        }

        public async Task<IEnumerable<OutBoxViewModel>> GetOutBox(int page)
        {
            var currentUserId = _userManager.GetCurrentUserId();
            return await _conversations.Where(a => a.SenderId == currentUserId)
                .Include(a => a.Receiver)
                .AsNoTracking()
                .ProjectTo<OutBoxViewModel>(_mappingEngine)
                .OrderByDescending(a => a.StartDate)
                .Skip((page - 1) * 5)
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
                        .Include(a => a.Attachments)
                        .Include(a=>a.Sender)
                        .OrderBy(a => a.SendDate)
                        .ProjectTo<MessageViewModel>(_mappingEngine)
                        .ToListAsync();
            
            return new MessageListViewModel
            {
                Messages = messages,
                AddMessageViewModel = new AddMessageViewModel
                {
                    ConversationId = conversationId,
                    ReplyId = messages.First().Id
                }
            };
        }
        #endregion

        #region Attachment
        public System.Threading.Tasks.Task<MessageAttachment> GetAttachment(Guid attachmentId)
        {
            return _attachments.FirstOrDefaultAsync(a => a.Id == attachmentId);
        }
        public async System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<AttachmentViewModel>> GetAttachments(Guid messageId)
        {
            return await _attachments.ProjectTo<AttachmentViewModel>(_mappingEngine).ToListAsync();
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
            return _conversations.Where(a => a.Id == conversationId & a.SenderId != currentUser).UpdateAsync(a => new Conversation { IsSeen = true });
        }

        public long NewMessgesCount()
        {
            var currentUserId = _userManager.GetCurrentUserId();
            return _conversations.Where(a => a.ReceiverId == currentUserId && !a.IsSeen).LongCount();
        }
        #endregion
    }
}
