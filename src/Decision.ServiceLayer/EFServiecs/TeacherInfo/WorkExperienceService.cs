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

        private readonly ITitleService _titleService;
        private readonly ICityService _cityService;
        private readonly IStateService _stateService;
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<WorkExperience> _workExperiences;
        #endregion

        #region Ctor
        public WorkExperienceService(IUnitOfWork unitOfWork,ICityService cityService,IStateService stateService,ITitleService titleService, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _workExperiences = _unitOfWork.Set<WorkExperience>();
            _mappingEngine = mappingEngine;
            _cityService = cityService;
            _stateService = stateService;
            _titleService = titleService;
        }
        #endregion

        #region GetForEdit
        public async Task<EditWorkExperienceViewModel> GetForEditAsync(Guid id,string path)
        {
            var viewModel=await  _workExperiences.AsNoTracking().ProjectTo<EditWorkExperienceViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
            if (viewModel == null) return null;
            viewModel.States = _stateService.GetAsSelectListItemAsync(viewModel.State, path);
            viewModel.Cities = _cityService.GetAsSelectListByStateNameAsync(viewModel.State, viewModel.City, path);
            viewModel.Titles = 
                await _titleService.GetAsSelectListItemAsync(TitleType.OrganizationPostTitle, viewModel.TitleId);
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
            workExperience.LasModifierId = _userManager.GetCurrentUserId();
        }
        #endregion

        #region Create
        public async Task<WorkExperienceViewModel> Create(AddWorkExperienceViewModel viewModel)
        {
            var workExperience = _mappingEngine.Map<WorkExperience>(viewModel);
            workExperience.CreatorId = _userManager.GetCurrentUserId();
            _workExperiences.Add(workExperience);
            await _unitOfWork.SaveChangesAsync();
            return await GetWorkExperienceViewModel(workExperience.Id);
        }
        #endregion

        #region GetPagedList
        public  async Task<WorkExperienceListViewModel> GetPagedListAsync(WorkExperienceSearchRequest request)
        {
            var cities = _workExperiences.Include(a=>a.Creator).Include(a=>a.LasModifier).Include(a=>a.Title).Where(a => a.ApplicantId == request.ApplicantId).AsNoTracking().OrderByDescending(a => a.TenureBeginDate).AsQueryable();
          
            var selectedCities = cities.ProjectTo<WorkExperienceViewModel>(_mappingEngine);

            var query = await selectedCities
                .Skip((request.PageIndex - 1)*10)
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
                Titles = await _titleService.GetAsSelectListItemAsync(TitleType.OrganizationPostTitle, null)
            };
        }

        public async Task FillAddViewModel(AddWorkExperienceViewModel viewModel,string path)
        {
            viewModel.States = _stateService.GetAsSelectListItemAsync(viewModel.State, path);
            viewModel.Cities = _cityService.GetAsSelectListByStateNameAsync(viewModel.State, viewModel.City, path);
            viewModel.Titles = await _titleService.GetAsSelectListItemAsync(TitleType.OrganizationPostTitle, viewModel.TitleId);
        }

        public async Task FillEditViewModel(EditWorkExperienceViewModel viewModel, string path)
        {
            viewModel.States = _stateService.GetAsSelectListItemAsync(viewModel.State, path);
            viewModel.Cities = _cityService.GetAsSelectListByStateNameAsync(viewModel.State, viewModel.City, path);
            viewModel.Titles = await _titleService.GetAsSelectListItemAsync(TitleType.OrganizationPostTitle, viewModel.TitleId);
        }
        #endregion

        public Task<WorkExperienceViewModel> GetWorkExperienceViewModel(Guid guid)
        {
            return
                _workExperiences.Include(a => a.Creator)
                    .Include(a => a.LasModifier)
                    .Include(a => a.Title)
                    .ProjectTo<WorkExperienceViewModel>(_mappingEngine)
                    .FirstOrDefaultAsync(a => a.Id == guid);
        }
    }
}