using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.ServiceLayer.Contracts.Evaluations;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.EntireEvaluation;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.Evaluations
{
    /// <summary>
    /// کلاس ارائه دهنده سروسیس های لازم برای اعمال روی ارزیابی از متقاضی
    /// </summary>
    public class EntireEvaluationService : IEntireEvaluationService
    {
        #region Fields
        
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<EntireEvaluation> _entireEvaluations;
        #endregion

        #region Ctor

        public EntireEvaluationService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _entireEvaluations = _unitOfWork.Set<EntireEvaluation>();
            _mappingEngine = mappingEngine;
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _entireEvaluations.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region GetForEdit
        public async Task<EditEntireEvaluationViewModel> GetForEditAsync(Guid id)
        {
            var viewModel =
                await
                    _entireEvaluations.AsNoTracking()
                        .ProjectTo<EditEntireEvaluationViewModel>(_mappingEngine)
                        .FirstOrDefaultAsync(a => a.Id == id);
            //if (viewModel == null)
            //    throw new EntityNotFoundException("Failed to find user with id " + id);
            return viewModel;
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditEntireEvaluationViewModel viewModel)
        {
            var entireEvaluation = await _entireEvaluations.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, entireEvaluation);
        }
        #endregion

        #region Create
        public async  Task<EntireEvaluationViewModel> Create(AddEntireEvaluationViewModel viewModel)
        {
            var entireEvaluation = _mappingEngine.Map<EntireEvaluation>(viewModel);
            _entireEvaluations.Add(entireEvaluation);
            await _unitOfWork.SaveChangesAsync();
            return await _entireEvaluations
                .Include(a => a.CreatedBy).Include(a => a.ModifiedBy).AsNoTracking()
                .ProjectTo<EntireEvaluationViewModel>(_mappingEngine)
                .FirstOrDefaultAsync(a => a.Id == entireEvaluation.Id);
        }
        #endregion

        #region GetPagedList
        public async  Task<EntireEvaluationListViewModel> GetPagedListAsync(EntireEvaluationSearchRequest request)
        {
            var entireEvaluations = _entireEvaluations.Where(a=>a.ApplicantId==request.ApplicantId)
                .Include(a => a.CreatedBy).Include(a => a.ModifiedBy).AsNoTracking()
                .OrderByDescending(a => a.EvaluationDate).AsQueryable();

            var selectedEntireEvaluations = entireEvaluations.ProjectTo<EntireEvaluationViewModel>(_mappingEngine);

            var query =await  selectedEntireEvaluations
                .Skip((request.PageIndex - 1)*10)
                .Take(10).ToListAsync();

            return new EntireEvaluationListViewModel { SearchRequest = request, EntireEvaluations = query };
        }
        #endregion

        #region IsInDb
        public Task<bool> IsInDb(Guid id)
        {
            return _entireEvaluations.AnyAsync(a => a.Id == id);
        }
        #endregion
    }
}