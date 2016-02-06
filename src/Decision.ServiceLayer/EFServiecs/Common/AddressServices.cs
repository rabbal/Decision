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
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.Address;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.Common
{
    public class AddressService : IAddressService
    {
        #region Fields
        private readonly IMappingEngine _mappingEngine;
        private readonly IStateService _stateService;
        private readonly ICityService _cityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<Address> _addresses;
        #endregion

        #region Ctor
        public AddressService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine, IStateService stateService, ICityService cityService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _addresses = _unitOfWork.Set<Address>();
            _mappingEngine = mappingEngine;
            _cityService = cityService;
            _stateService = stateService;
        }
        #endregion

        #region GetForEditAsync
        public async  Task<EditAddressViewModel> GetForEditAsync(Guid id,string path)
        {
            var viewModel=await _addresses.AsNoTracking().ProjectTo<EditAddressViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
            if (viewModel == null) return null;
            viewModel.States = _stateService.GetAsSelectListItemAsync(viewModel.State, path);
            viewModel.Cities = _cityService.GetAsSelectListByStateNameAsync(viewModel.State, viewModel.City, path);
            return viewModel;
        }

        #endregion

        #region DeleteAsync
        public Task DeleteAsync(Guid id)
        {
            return _addresses.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region EditAsync
        public async Task EditAsync(EditAddressViewModel viewModel)
        {
            var address = await _addresses.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, address);
            address.LasModifierId = _userManager.GetCurrentUserId();
        }
        #endregion

        #region Create
        public async  Task<AddressViewModel> Create(AddAddressViewModel viewModel)
        {
            var address = _mappingEngine.Map<Address>(viewModel);
            address.CreatorId = _userManager.GetCurrentUserId();
            _addresses.Add(address);
            await _unitOfWork.SaveChangesAsync();
            return await GetAddressViewModel(address.Id);
        }
        #endregion

        #region GetAddressesAsync
        public async Task<AddressListViewModel> GetAddressesAsync(AddressSearchRequest request)
        {
            return new AddressListViewModel
            {
                Addresses = await _addresses.AsNoTracking()
                    .Where(a => a.ApplicantId == request.ApplicantId)
                    .ProjectTo<AddressViewModel>(_mappingEngine)
                    .OrderByDescending(a => a.CreateDate)
                    .Skip((request.PageIndex - 1)*5)
                    .Take(5)
                    .ToListAsync(),
                Request = request
            };
        }

        #endregion

        #region IsInDb
        public Task<bool> IsInDb(Guid id)
        {
            return _addresses.AnyAsync(a => a.Id == id);
        }


        #endregion

        #region Fill
        public void FillAddViewModel(AddAddressViewModel viewModel, string path)
        {
            viewModel.States = _stateService.GetAsSelectListItemAsync(viewModel.State, path);
            viewModel.Cities = _cityService.GetAsSelectListByStateNameAsync(viewModel.State, viewModel.City, path);
        }

        public void FillEditViewModel(EditAddressViewModel viewModel, string path)
        {
            viewModel.States = _stateService.GetAsSelectListItemAsync(viewModel.State, path);
            viewModel.Cities = _cityService.GetAsSelectListByStateNameAsync(viewModel.State, viewModel.City, path);
        }

        public AddAddressViewModel GetForCreate(Guid ApplicantId,string path)
        {
            return new AddAddressViewModel
            {
                States = _stateService.GetAsSelectListItemAsync(null, path),
                Cities = new List<SelectListItem>(),
                ApplicantId = ApplicantId
                
            };
        }
        #endregion


        public Task<AddressViewModel> GetAddressViewModel(Guid guid)
        {
            return
                _addresses.AsNoTracking()
                    .ProjectTo<AddressViewModel>(_mappingEngine)
                    .FirstOrDefaultAsync(a => a.Id == guid);
        }
    }
}