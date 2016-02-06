using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.Common.Helpers.Extentions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.Utility;
using Decision.ViewModel.TrainingCenter;
using EFSecondLevelCache;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.ApplicantInfo
{
    public class TrainingCenterService : ITrainingCenterService
    {
        #region Fields
        private readonly IMappingEngine _mappingEngine;
        private readonly IStateService _stateService;
        private readonly ICityService _cityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IAddressService _addressService;
        private readonly IDbSet<TrainingCenter> _trainingCenters;
        #endregion

        #region Ctor

        public TrainingCenterService(IUnitOfWork unitOfWork, IApplicationUserManager userManager,
            IStateService StateService, ICityService cityService,
            IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _trainingCenters = _unitOfWork.Set<TrainingCenter>();
            _mappingEngine = mappingEngine;
            _cityService = cityService;
            _stateService = StateService;
        }
        #endregion

        #region GetForEditAsync
        public async Task<EditTrainingCenterViewModel> GetForEditAsync(Guid id, string path)
        {
            var viewModel = await _trainingCenters.AsNoTracking().ProjectTo<EditTrainingCenterViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
            if (viewModel == null) return null;
            viewModel.States = _stateService.GetAsSelectListItemAsync(viewModel.State, path);
            viewModel.Cities = _cityService.GetAsSelectListByStateNameAsync(viewModel.State, viewModel.City, path);
            return viewModel;
        }

        #endregion

        #region DeleteAsync
        public Task DeleteAsync(Guid id)
        {
            return _trainingCenters.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region EditAsync
        public async Task EditAsync(EditTrainingCenterViewModel viewModel)
        {
            var trainingCenter = await _trainingCenters.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, trainingCenter);
            trainingCenter.LasModifierId = _userManager.GetCurrentUserId();
        }
        #endregion

        #region Create
        public async Task<TrainingCenterViewModel> Create(AddTrainingCenterViewModel viewModel)
        {
            var trainingCenter = _mappingEngine.Map<TrainingCenter>(viewModel);
            trainingCenter.CreatorId = _userManager.GetCurrentUserId();
            _trainingCenters.Add(trainingCenter);
            await _unitOfWork.SaveChangesAsync();
            return await GetCenterViewModel(trainingCenter.Id);
        }
        #endregion

        #region GetPagedListAsync
        public async  Task<TrainingCenterListViewModel> GetPagedListAsync(TrainingCenterSearchRequest request)
        {
            var trainingCenters = _trainingCenters.AsNoTracking().Include(a => a.Creator).Include(a => a.LasModifier).OrderBy(a => a.CreateDate).AsQueryable();

            if (request.State.HasValue())
            {
                trainingCenters = trainingCenters.Where(a => a.State == request.State);
                if (request.City.HasValue())
                    trainingCenters = trainingCenters.Where(a => a.City == request.City);
            }

            if (request.Term.HasValue())
                trainingCenters = trainingCenters.Where(a => a.CenterName.Contains(request.Term));

            var result = trainingCenters.ProjectTo<TrainingCenterViewModel>(_mappingEngine);


            var query = await result
                .Skip((request.PageIndex - 1)*10)
                .Take(10)
                .ToListAsync();

            return new TrainingCenterListViewModel { SearchRequest = request, TrainingCenters = query };
        }
        #endregion

        #region IsNameExistAsync
        public async Task<bool> IsNameExistAsync(string name, Guid? id, string city)
        {
            var trainingCenters = await _trainingCenters.Where(a => a.City == city).Select(a => new { Id = a.Id, Name = a.CenterName }).ToListAsync();
            return id == null
                ? trainingCenters.Any(a => a.Name.GetFriendlyPersianName() == name.GetFriendlyPersianName())
                : trainingCenters.Any(a => a.Id != id.Value && a.Name.GetFriendlyPersianName() == name.GetFriendlyPersianName());
        }
        #endregion

        #region IsInDb
        public Task<bool> IsInDb(Guid id)
        {
            return _trainingCenters.AnyAsync(a => a.Id == id);
        }
        #endregion

        #region GetAsSelectListItemAsync
        public async Task<IEnumerable<SelectListItem>> GetAsSelectListItemAsync(string city, Guid? selected)
        {
            var centers = city.HasValue()
                ? await _trainingCenters.AsNoTracking()
                    .Where(a => a.City == city)
                    .OrderBy(a => a.CenterName)
                    .ProjectTo<SelectListItem>(_mappingEngine)
                    .Cacheable()
                    .ToListAsync()
                : new List<SelectListItem>();
            if (selected.HasValue) centers.ForEach(a => a.Selected = a.Value == selected.Value.ToString());
            return centers;
        }
        #endregion

        #region Fill
        public Task FillEditViewMode(EditTrainingCenterViewModel viewModel, string path)
        {
            viewModel.States = _stateService.GetAsSelectListItemAsync(viewModel.State, path);
            viewModel.Cities =
                 _cityService.GetAsSelectListByStateNameAsync(viewModel.State, viewModel.City, path);
            return Task.FromResult(0);
        }

        public Task FillAddViewMolde(AddTrainingCenterViewModel viewModel, string path)
        {
            viewModel.States = _stateService.GetAsSelectListItemAsync(viewModel.State, path);
            viewModel.Cities =
                _cityService.GetAsSelectListByStateNameAsync(viewModel.State, viewModel.City, path);
            return Task.FromResult(0);
        }

        public Task<AddTrainingCenterViewModel> GetForCreate(string path)
        {
            var viewModel = new AddTrainingCenterViewModel
            {
                States = _stateService.GetAsSelectListItemAsync(null, path)
            };
            return Task.FromResult(viewModel);
        }
        #endregion

        #region GetCityAndState
        public Task<Tuple<string, string>> GetCityAndState(Guid id)
        {
            return
                _trainingCenters.Where(a => a.Id == id)
                    .Select(a => new Tuple<string, string>(a.State, a.City))
                    .FirstAsync();
        }
        #endregion
        public Task<TrainingCenterViewModel> GetCenterViewModel(Guid id)
        {
            return
                _trainingCenters.Include(a=>a.Creator).Include(a=>a.LasModifier).ProjectTo<TrainingCenterViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}