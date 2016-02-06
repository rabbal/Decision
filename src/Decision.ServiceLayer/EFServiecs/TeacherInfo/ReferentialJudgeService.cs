using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.ReferentialApplicant;
using EntityFramework.Extensions;

namespace Decision.ServiceLayer.EFServiecs.ApplicantInfo
{
    /// <summary>
    /// کلاس ارائه دهنده سروسیس های لازم برای اعمال روی ارجاع متقاضی
    /// </summary>
    public class ReferentialApplicantService : IReferentialApplicantService
    {
        #region Fields

        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<ReferentialApplicant> _referentialApplicants;
        #endregion

        #region Ctor

        public ReferentialApplicantService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _referentialApplicants = _unitOfWork.Set<ReferentialApplicant>();
            _mappingEngine = mappingEngine;
        }
        #endregion

        #region GetForEdit
        public Task<EditReferentialApplicantViewModel> GetForEditAsync(Guid id)
        {
            return _referentialApplicants.AsNoTracking().ProjectTo<EditReferentialApplicantViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _referentialApplicants.Where(a => a.ApplicantId == id && !a.FinishedDate.HasValue)
                .UpdateAsync(a => new ReferentialApplicant
            {
                FinishedDate = DateTime.Now
            });
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditReferentialApplicantViewModel viewModel)
        {
            var referentialApplicant = await _referentialApplicants.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, referentialApplicant);
        }
        #endregion

        #region Create
        public void Create(AddReferentialApplicantViewModel viewModel)
        {
            var referentialApplicant = _mappingEngine.Map<ReferentialApplicant>(viewModel);
            referentialApplicant.ReferencedFromId = _userManager.GetCurrentUserId();
            _referentialApplicants.Add(referentialApplicant);
        }

        public async Task<IEnumerable<Guid>> GetRefersApplicantIds()
        {
            var currentUser = _userManager.GetCurrentUserId();
            return
                await
                    _referentialApplicants.Where(a => a.ReferencedToId == currentUser).Select(a => a.ApplicantId).ToListAsync();
        }

        public bool CanManageApplicant(Guid ApplicantId)
        {
            var currentUserId = _userManager.GetCurrentUserId();
            var isOperator = _userManager.IsOperator();
            return !isOperator || _referentialApplicants.Any(
                a => a.ApplicantId == ApplicantId && a.ReferencedToId == currentUserId && !a.FinishedDate.HasValue);
        }


        #endregion
        public Task FinishReferApplicant(Guid ApplicantId)
        {
            return DeleteAsync(ApplicantId);
        }
    }
}