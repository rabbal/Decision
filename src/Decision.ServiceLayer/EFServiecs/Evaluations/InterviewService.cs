using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.Common.Helpers.Extentions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.ServiceLayer.Contracts.Evaluations;
using Decision.ServiceLayer.Contracts.Users;
using Decision.Utility;
using Decision.ViewModel.Interview;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.Evaluations
{
    /// <summary>
    /// کلاس ارائه دهنده سروسیس های لازم برای اعمال روی مصاحبات
    /// </summary>
    public class InterviewService : IInterviewService
    {
        #region Fields

        private readonly IAppraiserService _appraiserService;
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<Interview> _interviews;
        #endregion

        #region Ctor

        public InterviewService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine, IAppraiserService appraiserService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _interviews = _unitOfWork.Set<Interview>();
            _mappingEngine = mappingEngine;
            _appraiserService = appraiserService;
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _interviews.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region Edit

        public async Task EditAsync(EditInterviewViewModel viewModel)
        {
            var interview = await _interviews.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, interview);
            interview.LasModifierId = _userManager.GetCurrentUserId();

            if (viewModel.AttachmentScan.HasValue())
                interview.Attachment = Convert.FromBase64String(viewModel.AttachmentScan);
            else if (viewModel.AttachmentFile.HasFile())
            {
                interview.Attachment =
                    viewModel.AttachmentFile.InputStream.ConvertToByteArrary(viewModel.AttachmentFile.ContentLength);
            }
        }
        #endregion

        #region Create
        public async Task<InterviewViewModel> Create(AddInterviewViewModel viewModel)
        {
            var interview = _mappingEngine.Map<Interview>(viewModel);
            interview.CreatorId = _userManager.GetCurrentUserId();
            if (viewModel.AttachmentScan.HasValue())
                interview.Attachment = Convert.FromBase64String(viewModel.AttachmentScan);
            else if (viewModel.AttachmentFile.HasFile())
            {
                interview.Attachment =
                    viewModel.AttachmentFile.InputStream.ConvertToByteArrary(viewModel.AttachmentFile.ContentLength);
            }
            _interviews.Add(interview);
            await _unitOfWork.SaveChangesAsync();
            return await _interviews.Include(a => a.Interviewer)
                .Include(a => a.Creator).Include(a => a.LasModifier).AsNoTracking()
                .ProjectTo<InterviewViewModel>(_mappingEngine)
                .FirstOrDefaultAsync(a => a.Id == interview.Id);
        }
        #endregion

        #region GetPagedList
        public async Task<InterviewListViewModel> GetPagedListAsync(InterviewSearchRequest request)
        {
            var interviews = _interviews.Where(a => a.TeacherId == request.TeacherId).Include(a => a.Interviewer)
                .Include(a => a.Creator).Include(a => a.LasModifier).AsNoTracking()
                .OrderByDescending(a => a.InterviewDate).AsQueryable();

            if (request.Term.HasValue())
                interviews = interviews.Where(a => a.Body.Contains(request.Term) || a.Brief.Contains(request.Term));

            var selectedInterviews = interviews.ProjectTo<InterviewViewModel>(_mappingEngine);

            var query = await selectedInterviews
                .Skip((request.PageIndex - 1) * 10)
                .Take(10)
                .ToListAsync();

            return new InterviewListViewModel { SearchRequest = request, Interviews = query };
        }
        #endregion

        #region GetForEdit
        public async Task<EditInterviewViewModel> GetForEditAsync(Guid id)
        {
            var viewModel =
                await
                    _interviews.AsNoTracking()
                        .ProjectTo<EditInterviewViewModel>(_mappingEngine)
                        .FirstOrDefaultAsync(a => a.Id == id);
            if (viewModel == null) return null;
            viewModel.Interviewers = await _appraiserService.GetAsSelectedListItem(viewModel.InterviewerId);
            return viewModel;
        }
        #endregion

        #region IsInDb
        public Task<bool> IsInDb(Guid id)
        {
            return _interviews.AnyAsync(a => a.Id == id);
        }
        #endregion


        public async Task FillEditViewModel(EditInterviewViewModel viewModel)
        {
            viewModel.Interviewers = await _appraiserService.GetAsSelectedListItem(viewModel.InterviewerId);
        }
        public async Task FillAddViewModel(AddInterviewViewModel viewModel)
        {
            viewModel.Interviewers = await _appraiserService.GetAsSelectedListItem(null);
        }


        public async Task<AddInterviewViewModel> GetForCreate(Guid TeacherId)
        {
            return new AddInterviewViewModel
            {
                TeacherId = TeacherId,
                Interviewers = await _appraiserService.GetAsSelectedListItem(null)
            };
        }


        public Task<byte[]> GetAttachment(Guid id)
        {
            return _interviews.Where(a => a.Id == id).Select(a => a.Attachment).FirstOrDefaultAsync();
        }
    }
}