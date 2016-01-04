using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.Common.Helpers.Extentions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.Evaluations;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.Appraiser;
using EFSecondLevelCache;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.Evaluations
{
    /// <summary>
    /// کلاس ارائه دهنده سروسیس های لازم برای اعمال روی ارزیاب
    /// </summary>
    public class AppraiserService : IAppraiserService
    {
        #region Fields

        private readonly ITitleService _titleService;
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<Appraiser> _appraisers;
        #endregion

        #region Ctor

        public AppraiserService(IUnitOfWork unitOfWork, ITitleService titleService, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _appraisers = _unitOfWork.Set<Appraiser>();
            _titleService = titleService;
            _mappingEngine = mappingEngine;
        }
        #endregion

        #region GetForEdit
        public async Task<EditAppraiserViewModel> GetForEditAsync(Guid id)
        {
            var viewModel = await _appraisers.AsNoTracking().ProjectTo<EditAppraiserViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
            if (viewModel == null) return null;
            viewModel.AppraiserTitles =
                await _titleService.GetAsSelectListItemAsync(TitleType.Person, viewModel.AppraiserTitleId);
            return viewModel;

        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _appraisers.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditAppraiserViewModel viewModel)
        {
            var appraiser = await _appraisers.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, appraiser);
            appraiser.LasModifierId = _userManager.GetCurrentUserId();
        }
        #endregion

        #region Create
        public async Task<AppraiserViewModel> Create(AddAppraiserViewModel viewModel)
        {
            var appraiser = _mappingEngine.Map<Appraiser>(viewModel);
            appraiser.CreatorId = _userManager.GetCurrentUserId();
            _appraisers.Add(appraiser);
            await _unitOfWork.SaveChangesAsync();
            return await GetAppraiserViewModel(appraiser.Id);
        }
        #endregion

        #region GetPagedList
        public async Task<AppraiserListViewModel> GetPagedListAsync(AppraiserSearchRequest request)
        {
            var appraisers = _appraisers.AsNoTracking().Include(a => a.Creator).Include(a => a.LasModifier).Include(a => a.AppraiserTitle).OrderByDescending(a => a.LastName).ThenByDescending(a => a.FirstName).AsQueryable();

            if (request.Term.HasValue())
                appraisers = appraisers.Where(a => a.FirstName.Contains(request.Term) || a.LastName.Contains(request.Term));

            var selectedAppraisers = appraisers.ProjectTo<AppraiserViewModel>(_mappingEngine);

            var query = await selectedAppraisers
                .Skip((request.PageIndex - 1) * 10)
                .Take(10).ToListAsync();

            return new AppraiserListViewModel { SearchRequest = request, Appraisers = query };
        }
        #endregion

        public async Task<AddAppraiserViewModel> GetForCreate()
        {
            return new AddAppraiserViewModel
            {
                AppraiserTitles = await _titleService.GetAsSelectListItemAsync(TitleType.Person, null)
            };
        }

        public async Task FillAddViewModel(AddAppraiserViewModel viewModel)
        {
            viewModel.AppraiserTitles =
                await _titleService.GetAsSelectListItemAsync(TitleType.Person, viewModel.AppraiserTitleId);
        }

        public Task<bool> IsInDb(Guid id)
        {
            return _appraisers.AnyAsync(a => a.Id == id);
        }

        public async Task FillEditViewModel(EditAppraiserViewModel viewModel)
        {
            viewModel.AppraiserTitles =
               await _titleService.GetAsSelectListItemAsync(TitleType.Person, viewModel.AppraiserTitleId);
        }



        public Task<AppraiserViewModel> GetAppraiserViewModel(Guid id)
        {
            return _appraisers.AsNoTracking()
                 .Include(a => a.Creator)
                 .Include(a => a.LasModifier)
                 .Include(a => a.AppraiserTitle).ProjectTo<AppraiserViewModel>(_mappingEngine)
                 .FirstOrDefaultAsync(a => a.Id == id);
        }


        public async Task<System.Collections.Generic.IEnumerable<SelectListItem>> GetAsSelectedListItem(Guid? selected)
        {
            var appraisers = await _appraisers.AsNoTracking().OrderByDescending(a => a.CreateDate)
              .ProjectTo<SelectListItem>(_mappingEngine)
              .Cacheable()
              .ToListAsync();
            if (selected != null) appraisers.ForEach(a => a.Selected = selected.Value.ToString() == a.Value);
            return appraisers;
        }
    }
}