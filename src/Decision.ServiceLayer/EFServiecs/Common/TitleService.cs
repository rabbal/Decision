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
using Decision.DomainClasses.Entities.Common;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.Users;
using Decision.Utility;
using Decision.ViewModel.Title;
using EFSecondLevelCache;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.Common
{
    public class TitleService : ITitleService
    {
        #region Fields

        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<Title> _titles;
        #endregion

        #region Ctor

        public TitleService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _titles = _unitOfWork.Set<Title>();
            _mappingEngine = mappingEngine;
        }
        #endregion

        #region GetForEdit
        public Task<EditTitleViewModel> GetForEditAsync(Guid id)
        {
            return _titles.AsNoTracking().ProjectTo<EditTitleViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _titles.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditTitleViewModel viewModel)
        {
            var title = await _titles.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, title);
            title.LasModifierId = _userManager.GetCurrentUserId();
        }
        #endregion

        #region Create
        public async  Task<TitleViewModel> Create(AddTitleViewModel viewModel)
        {
            var title = _mappingEngine.Map<Title>(viewModel);
            title.CreatorId = _userManager.GetCurrentUserId();
            _titles.Add(title);
            await _unitOfWork.SaveChangesAsync();
            return await GetTitleViewModel(title.Id);
        }
        #endregion

        #region GetPagedList
        public async Task<TitleListViewModel> GetPagedList(TitleSearchRequest request)
        {
            var titles = _titles.AsNoTracking().Include(a => a.Creator).Include(a => a.LasModifier).OrderByDescending(a => a.CreateDate).AsQueryable();

            if (request.Name.HasValue())
                titles = titles.Where(a => a.Name.Contains(request.Name));
            if ((int)request.Type != 0)
                titles = titles.Where(a => a.Type == request.Type);
            if ((int)request.Category != 0 && request.Type==TitleType.CourseContent)
                titles = titles.Where(a => a.Category == request.Category);

            var selectedTitles = titles.ProjectTo<TitleViewModel>(_mappingEngine);

            var query = await selectedTitles
                .Skip((request.PageIndex - 1)*10)
                .Take(10)
                .ToListAsync();

            return new TitleListViewModel { Request = request, Titles = query };
        }
        #endregion

        #region IsByNameExist
        public async Task<bool> IsByNameExist(string name, Guid? id, TitleType type, TitleCategory category)
        {

            var titles = await _titles.Select(a => new { Id = a.Id, Name = a.Name, Type = a.Type, Category = a.Category }).ToListAsync();
            return id == null
                ? titles.Any(
                    a =>
                        a.Name.GetFriendlyPersianName() == name.GetFriendlyPersianName() && a.Type == type &&
                        a.Category == category)
                : titles.Any(
                    a =>
                        a.Id != id & a.Name.GetFriendlyPersianName() == name.GetFriendlyPersianName() && a.Type == type &&
                        a.Category == category);

        }
        #endregion

        #region IsEnableCategorySelection
        public Task<bool> IsEnableCategorySelection(TitleType type)
        {
            return Task.FromResult(type == TitleType.CourseContent);
        }
        #endregion

        #region IsInDb
        public Task<bool> IsInDb(Guid id)
        {
            return _titles.AnyAsync(a => a.Id == id);
        }
        #endregion

        #region GetAsSelectListItemAsync
        public async Task<IEnumerable<SelectListItem>> GetAsSelectListItemAsync(TitleType type, Guid? selectedId)
        {
            var titles = await _titles.AsNoTracking()
                .Where(a => a.Type == type).OrderByDescending(a => a.Name)
                .ProjectTo<SelectListItem>(_mappingEngine)
                .Cacheable()
                .ToListAsync();
            if (selectedId != null) titles.ForEach(a => a.Selected = selectedId.Value.ToString() == a.Value);
            return titles;
        }
        #endregion


        public Task<TitleViewModel> GetTitleViewModel(Guid guid)
        {
           return  _titles.AsNoTracking()
                .Include(a => a.Creator)
                .Include(a => a.LasModifier).ProjectTo<TitleViewModel>(_mappingEngine)
                .FirstOrDefaultAsync(a => a.Id == guid);
        }
    }
}