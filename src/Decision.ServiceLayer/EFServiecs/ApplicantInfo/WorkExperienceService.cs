using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.WorkExperience;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.ApplicantInfo
{
    /// <summary>
    /// کلاس ارائه دهنده سروسیس های لازم برای اعمال روی سابقه کاری
    /// </summary>
    public class WorkExperienceService : IWorkExperienceService
    {
        #region Fields
        private readonly ICityService _cityService;
        private readonly IStateService _stateService;
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<WorkExperience> _workExperiences;
        #endregion

        #region Ctor
        public WorkExperienceService(IUnitOfWork unitOfWork, ICityService cityService, IStateService stateService, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _workExperiences = _unitOfWork.Set<WorkExperience>();
            _mappingEngine = mappingEngine;
            _cityService = cityService;
            _stateService = stateService;
        }
        #endregion

        #region GetForEdit
        public async Task<EditWorkExperienceViewModel> GetForEditAsync(Guid id, string path)
        {
            var viewModel = await _workExperiences.AsNoTracking().ProjectTo<EditWorkExperienceViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
            if (viewModel == null) return null;
            viewModel.States = _stateService.GetAsSelectListItemAsync(viewModel.State, path);
            viewModel.Cities = _cityService.GetAsSelectListByStateNameAsync(viewModel.State, viewModel.City, path);
            return viewModel;
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _workExperiences.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditWorkExperienceViewModel viewModel)
        {
            var workExperience = await _workExperiences.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, workExperience);
        }
        #endregion

        #region Create
        public async Task<WorkExperienceViewModel> Create(AddWorkExperienceViewModel viewModel)
        {
            var workExperience = _mappingEngine.Map<WorkExperience>(viewModel);
            _workExperiences.Add(workExperience);
            await _unitOfWork.SaveChangesAsync();
            return await GetWorkExperienceViewModel(workExperience.Id);
        }
        #endregion

        #region GetPagedList
        public async Task<WorkExperienceListViewModel> GetPagedListAsync(WorkExperienceSearchRequest request)
        {
            var works = _workExperiences.Include(a => a.CreatedBy).Include(a => a.ModifiedBy).Where(a => a.ApplicantId == request.ApplicantId).AsNoTracking().OrderByDescending(a => a.TenureBeginDate).AsQueryable();
            var selectedCities = works.ProjectTo<WorkExperienceViewModel>(_mappingEngine);
            var resultsToSkip = (request.PageIndex - 1) * 10;
            var query = await selectedCities
                .Skip(() => resultsToSkip)
                .Take(10).ToListAsync();

            return new WorkExperienceListViewModel { SearchRequest = request, WorkExperiences = query };
        }
        #endregion

        #region Fill
        public Task<bool> IsInDb(Guid id)
        {
            return _workExperiences.AnyAsync(a => a.Id == id);
        }

        public async Task<AddWorkExperienceViewModel> GetForCreate(Guid ApplicantId, string path)
        {
            return new AddWorkExperienceViewModel
            {
                ApplicantId = ApplicantId,
                States = _stateService.GetAsSelectListItemAsync(null, path),
                Cities = new List<SelectListItem>(),
            };
        }

        public async Task FillAddViewModel(AddWorkExperienceViewModel viewModel, string path)
        {
            viewModel.States = _stateService.GetAsSelectListItemAsync(viewModel.State, path);
            viewModel.Cities = _cityService.GetAsSelectListByStateNameAsync(viewModel.State, viewModel.City, path);
        }

        public async Task FillEditViewModel(EditWorkExperienceViewModel viewModel, string path)
        {
            viewModel.States = _stateService.GetAsSelectListItemAsync(viewModel.State, path);
            viewModel.Cities = _cityService.GetAsSelectListByStateNameAsync(viewModel.State, viewModel.City, path);
        }
        #endregion

        public Task<WorkExperienceViewModel> GetWorkExperienceViewModel(Guid guid)
        {
            return
                _workExperiences.Include(a => a.CreatedBy)
                    .Include(a => a.ModifiedBy)
                    .ProjectTo<WorkExperienceViewModel>(_mappingEngine)
                    .FirstOrDefaultAsync(a => a.Id == guid);
        }
    }
}