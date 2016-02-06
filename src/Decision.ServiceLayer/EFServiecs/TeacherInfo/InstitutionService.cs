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
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.Utility;
using Decision.ViewModel.Institution;
using EFSecondLevelCache;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده سرویس دهنده عملیات مرتبط با موسسه آموزشی 
    /// </summary>
    public class InstitutionService : IInstitutionService
    {
        #region Fields

        private readonly IDbSet<Institution> _institutions;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IMappingEngine _mappingEngine;

        #endregion

        #region Ctor

        public InstitutionService(IUnitOfWork unitOfWork, IApplicationUserManager userManager,
            IMappingEngine mappingEngine)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mappingEngine = mappingEngine;
            _institutions = _unitOfWork.Set<Institution>();

        }

        #endregion

        #region IsNameExist

        public async Task<bool> IsNameExistAsync(string name, Guid? id)
        {
            var institutions = _institutions.Select(a => new {Id = a.Id, Name = a.Name}).AsQueryable();
            return id == null
                ? (await institutions.AnyAsync(a => a.Name.GetFriendlyPersianName() == name.GetFriendlyPersianName()))
                : (await institutions.AnyAsync(a => a.Id != id.Value && a.Name.GetFriendlyPersianName() == name.GetFriendlyPersianName()));
        }

        #endregion

        #region Create

        public async  Task<InstitutionViewModel> Create(AddInstitutionViewModel viewModel)
        {
            var institution = _mappingEngine.Map<Institution>(viewModel);
            institution.CreatorId = _userManager.GetCurrentUserId();
            _institutions.Add(institution);
            await _unitOfWork.SaveChangesAsync();
            return await GetInstitutionViewModel(institution.Id);
        }

        #endregion

        #region Edit

        public async Task EditAsync(EditInstitutionViewModel viewModel)
        {
            var institution = await _institutions.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, institution);
            institution.LasModifierId = _userManager.GetCurrentUserId();
        }

        #endregion

        #region Delete

        public Task DeleteAsync(Guid id)
        {
            return _institutions.Where(a => a.Id == id).DeleteAsync();
        }

        #endregion

        #region GetPagedList

        public async  Task<InstitutionListViewModel> GetPagedListAsync(InstitutionSearchRequest request)
        {
            var institutions =
                _institutions.AsNoTracking()
                    .Include(a => a.Creator)
                    .Include(a => a.LasModifier)
                    .OrderByDescending(a => a.Name)
                    .AsQueryable();

            if (request.Term.HasValue())
                institutions = institutions.Where(a => a.Name.Contains(request.Term));

            var selectedInstitutions = institutions.ProjectTo<InstitutionViewModel>(_mappingEngine);

            var query =await 
                selectedInstitutions
                    .Skip((request.PageIndex - 1)*10)
                    .Take(10).ToListAsync();

            return new InstitutionListViewModel {Request = request, Institutions = query};
        }

        #endregion

        #region GetForEdit

        public Task<EditInstitutionViewModel> GetForEditAsync(Guid id)
        {
            return
                _institutions.AsNoTracking()
                    .ProjectTo<EditInstitutionViewModel>(_mappingEngine)
                    .FirstOrDefaultAsync(a => a.Id == id);
        }

        #endregion

        #region GetAsSelectListItem

        public async Task<IEnumerable<SelectListItem>> GetAsSelectListItemAsync(Guid? selectedId)
        {

            var institutions = await _institutions.AsNoTracking()
                  .OrderByDescending(a => a.Name)
                  .ProjectTo<SelectListItem>(_mappingEngine)
                  .Cacheable()
                  .ToListAsync();
            if (selectedId.HasValue) institutions.ForEach(a => a.Selected = a.Value == selectedId.Value.ToString());
            return institutions;
        }

        #endregion

        public Task<bool> IsInDb(Guid id)
        {
            return _institutions.AnyAsync(a => a.Id == id);
        }


        public Task<InstitutionViewModel> GetInstitutionViewModel(Guid id)
        {
           return  _institutions.AsNoTracking()
                .Include(a => a.Creator)
                .Include(a => a.LasModifier).ProjectTo<InstitutionViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}