using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.EducationalBackground;
using EntityFramework.Extensions;

namespace Decision.ServiceLayer.EFServiecs.ApplicantInfo
{
    /// <summary>
    /// کلاس ارائه دهنده سروسیس های لازم برای اعمال روی سابقه تحصیلی متقاضی
    /// </summary>
    public class EducationalBackgroundService : IEducationalBackgroundService
    {
        #region Fields
        private const int a4Width = 595;
        private const int a4height = 842;
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<EducationalBackground> _educationalBackgrounds;
        #endregion

        #region Ctor

        public EducationalBackgroundService(IUnitOfWork unitOfWork,
            IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _educationalBackgrounds = _unitOfWork.Set<EducationalBackground>();
            _mappingEngine = mappingEngine;
        }
        #endregion

        #region GetForEditAsync
        public async Task<EditEducationalBackgroundViewModel> GetForEditAsync(Guid id)
        {
            var viewModel =
                await
                    _educationalBackgrounds.AsNoTracking()
                        .ProjectTo<EditEducationalBackgroundViewModel>(_mappingEngine)
                        .FirstOrDefaultAsync(a => a.Id == id);

            return viewModel;
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _educationalBackgrounds.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditEducationalBackgroundViewModel viewModel)
        {
            var educationalBackground = await _educationalBackgrounds.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, educationalBackground);
        }
        #endregion

        #region Create
        public async Task<EducationalBackgroundViewModel> Create(AddEducationalBackgroundViewModel viewModel)
        {
            var educationalBackground = _mappingEngine.Map<EducationalBackground>(viewModel);
           
            _educationalBackgrounds.Add(educationalBackground);
            await _unitOfWork.SaveAllChangesAsync(auditUserId:_userManager.GetCurrentUserId());

            return await _educationalBackgrounds
                .Include(a => a.CreatedBy)
                .Include(a => a.ModifiedBy)
                .AsNoTracking().ProjectTo<EducationalBackgroundViewModel>(_mappingEngine).FirstOrDefaultAsync();
        }
        #endregion

        #region GetPagedList
        public async Task<EducationalBackgroundListViewModel> GetPagedListAsync(EducationalBackgroundSearchRequest request)
        {
            var educationalBackgrounds =
                _educationalBackgrounds.Where(a => a.ApplicantId == request.ApplicantId)
                    .Include(a => a.CreatedBy)
                    .Include(a => a.ModifiedBy)
                    .AsNoTracking()
                    .OrderByDescending(a => a.CreatedOn)
                    .AsQueryable();

            var selectedEducationalBackgrounds = educationalBackgrounds.ProjectTo<EducationalBackgroundViewModel>(_mappingEngine);

            var resultToSkip = (request.PageIndex - 1)*10;
            var query = await selectedEducationalBackgrounds
                .Skip(()=>resultToSkip)
                .Take(10).ToListAsync();

            return new EducationalBackgroundListViewModel { SearchRequest = request, EducationalBackgrounds = query };
        }
        #endregion
        
        public Task<bool> IsInDb(Guid guid)
        {
            return _educationalBackgrounds.AnyAsync(a => a.Id == guid);
        }
    }
}