using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.ApplicantInServiceCourseType;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.ApplicantInfo
{
    /// <summary>
    /// کلاس ارائه دهنده سروسیس های لازم برای اعمال روی تعداد ساعت یک نوع ضمن خدمت برای متقاضی
    /// </summary>
    public class ApplicantInServiceCourseTypeService : IApplicantInServiceCourseTypeService
    {
        #region Fields

        private readonly ITitleService _titleService;
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<ApplicantInServiceCourseType> _ApplicantInServiceCourseTypes;
        #endregion

        #region Ctor

        public ApplicantInServiceCourseTypeService(IUnitOfWork unitOfWork,ITitleService titleService, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _ApplicantInServiceCourseTypes = _unitOfWork.Set<ApplicantInServiceCourseType>();
            _mappingEngine = mappingEngine;
            _titleService = titleService;
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _ApplicantInServiceCourseTypes.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region GetForEdit
        public async  Task<EditApplicantInServiceCourseTypeViewModel> GetForEditAsync(Guid id)
        {
           var viewModel=await _ApplicantInServiceCourseTypes.AsNoTracking().ProjectTo<EditApplicantInServiceCourseTypeViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
            if (viewModel == null) return null;
            viewModel.InServiceCourseTypeTitles =
                await
                    _titleService.GetAsSelectListItemAsync(TitleType.InServiceCourseType,
                        viewModel.InServiceCourseTypeTitleId);
            return viewModel;
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditApplicantInServiceCourseTypeViewModel viewModel)
        {
            var ApplicantInServiceCourseType = await _ApplicantInServiceCourseTypes.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, ApplicantInServiceCourseType);
            ApplicantInServiceCourseType.LasModifierId = _userManager.GetCurrentUserId();
        }
        #endregion

        #region Create

        public async Task<ApplicantInServiceCourseTypeViewModel> Create(AddApplicantInServiceCourseTypeViewModel viewModel)
        {
            var ApplicantInServiceCourseType = _mappingEngine.Map<ApplicantInServiceCourseType>(viewModel);
            ApplicantInServiceCourseType.CreatorId = _userManager.GetCurrentUserId();
            _ApplicantInServiceCourseTypes.Add(ApplicantInServiceCourseType);
            await _unitOfWork.SaveChangesAsync();
            return await GetApplicantInServiceCourseTypeViewModel(ApplicantInServiceCourseType.Id);

        }
        #endregion

        #region GetPagedList
        public async  Task<ApplicantInServiceCourseTypeListViewModel> GetPagedListAsync(ApplicantInServiceCourseTypeSearchRequest request)
        {
            var ApplicantInServiceCourseTypes =
                _ApplicantInServiceCourseTypes.Where(a => a.ApplicantId == request.ApplicantId).AsNoTracking()
                    .Include(a => a.Creator).Include(a => a.LasModifier)
                    .Include(a => a.InServiceCourseTypeTitle).OrderByDescending(a => a.CreateDate).AsQueryable();

            var selectedApplicantInServiceCourseTypes = ApplicantInServiceCourseTypes.ProjectTo<ApplicantInServiceCourseTypeViewModel>(_mappingEngine);

            var query =await   selectedApplicantInServiceCourseTypes
                .Skip((request.PageIndex - 1)*10)
                .Take(10).ToListAsync();

            return new ApplicantInServiceCourseTypeListViewModel { SearchRequest = request, ApplicantInServiceCourseTypes = query };
        }
        #endregion

        #region IsInDb
        public Task<bool> IsInDb(Guid id)
        {
            return _ApplicantInServiceCourseTypes.AnyAsync(a => a.Id == id);
        }
        #endregion


        public async  Task FillEditViewModel(EditApplicantInServiceCourseTypeViewModel viewModel)
        {
            viewModel.InServiceCourseTypeTitles =
               await
                   _titleService.GetAsSelectListItemAsync(TitleType.InServiceCourseType,
                       viewModel.InServiceCourseTypeTitleId);
        }

        public Task<ApplicantInServiceCourseTypeViewModel> GetApplicantInServiceCourseTypeViewModel(Guid guid)
        {
            return _ApplicantInServiceCourseTypes
                .Include(a => a.Creator).Include(a => a.LasModifier)
                .Include(a => a.InServiceCourseTypeTitle)
                .AsNoTracking()
                .ProjectTo<ApplicantInServiceCourseTypeViewModel>(_mappingEngine)
                .FirstOrDefaultAsync(a=>a.Id==guid);
        }

        public async  Task FillAddViewModel(AddApplicantInServiceCourseTypeViewModel viewModel)
        {
            viewModel.InServiceCourseTypeTitles =
             await
                 _titleService.GetAsSelectListItemAsync(TitleType.InServiceCourseType,
                     viewModel.InServiceCourseTypeTitleId);
        }

        public async  Task<AddApplicantInServiceCourseTypeViewModel> GetForCreate(Guid ApplicantId)
        {
            return new AddApplicantInServiceCourseTypeViewModel
            {
                InServiceCourseTypeTitles = await
                    _titleService.GetAsSelectListItemAsync(TitleType.InServiceCourseType, null)
            };
        }
    }
}