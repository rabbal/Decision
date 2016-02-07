using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.Common.Extentions;
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
        
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<Interview> _interviews;
        #endregion

        #region Ctor

        public InterviewService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _interviews = _unitOfWork.Set<Interview>();
            _mappingEngine = mappingEngine;
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
        }
        #endregion

        #region Create
        public async Task<InterviewViewModel> Create(AddInterviewViewModel viewModel)
        {
            var interview = _mappingEngine.Map<Interview>(viewModel);
           
            _interviews.Add(interview);
            await _unitOfWork.SaveChangesAsync();
            return await _interviews
                .Include(a => a.CreatedBy).Include(a => a.ModifiedBy).AsNoTracking()
                .ProjectTo<InterviewViewModel>(_mappingEngine)
                .FirstOrDefaultAsync(a => a.Id == interview.Id);
        }
        #endregion

        #region GetPagedList
        public async Task<InterviewListViewModel> GetPagedListAsync(InterviewSearchRequest request)
        {
            var interviews = _interviews.Where(a => a.ApplicantId == request.ApplicantId)
                .Include(a => a.CreatedBy).Include(a => a.ModifiedBy).AsNoTracking()
                .OrderByDescending(a => a.InterviewDate).AsQueryable();

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
           
            return viewModel;
        }
        #endregion

        #region IsInDb
        public Task<bool> IsInDb(Guid id)
        {
            return _interviews.AnyAsync(a => a.Id == id);
        }
        #endregion
    }
}