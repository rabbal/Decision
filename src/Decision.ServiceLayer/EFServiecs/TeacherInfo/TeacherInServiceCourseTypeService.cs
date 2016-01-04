using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.TeacherInfo;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.TeacherInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.TeacherInServiceCourseType;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.TeacherInfo
{
    /// <summary>
    /// کلاس ارائه دهنده سروسیس های لازم برای اعمال روی تعداد ساعت یک نوع ضمن خدمت برای استاد
    /// </summary>
    public class TeacherInServiceCourseTypeService : ITeacherInServiceCourseTypeService
    {
        #region Fields

        private readonly ITitleService _titleService;
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<TeacherInServiceCourseType> _TeacherInServiceCourseTypes;
        #endregion

        #region Ctor

        public TeacherInServiceCourseTypeService(IUnitOfWork unitOfWork,ITitleService titleService, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _TeacherInServiceCourseTypes = _unitOfWork.Set<TeacherInServiceCourseType>();
            _mappingEngine = mappingEngine;
            _titleService = titleService;
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _TeacherInServiceCourseTypes.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region GetForEdit
        public async  Task<EditTeacherInServiceCourseTypeViewModel> GetForEditAsync(Guid id)
        {
           var viewModel=await _TeacherInServiceCourseTypes.AsNoTracking().ProjectTo<EditTeacherInServiceCourseTypeViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
            if (viewModel == null) return null;
            viewModel.InServiceCourseTypeTitles =
                await
                    _titleService.GetAsSelectListItemAsync(TitleType.InServiceCourseType,
                        viewModel.InServiceCourseTypeTitleId);
            return viewModel;
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditTeacherInServiceCourseTypeViewModel viewModel)
        {
            var TeacherInServiceCourseType = await _TeacherInServiceCourseTypes.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, TeacherInServiceCourseType);
            TeacherInServiceCourseType.LasModifierId = _userManager.GetCurrentUserId();
        }
        #endregion

        #region Create

        public async Task<TeacherInServiceCourseTypeViewModel> Create(AddTeacherInServiceCourseTypeViewModel viewModel)
        {
            var TeacherInServiceCourseType = _mappingEngine.Map<TeacherInServiceCourseType>(viewModel);
            TeacherInServiceCourseType.CreatorId = _userManager.GetCurrentUserId();
            _TeacherInServiceCourseTypes.Add(TeacherInServiceCourseType);
            await _unitOfWork.SaveChangesAsync();
            return await GetTeacherInServiceCourseTypeViewModel(TeacherInServiceCourseType.Id);

        }
        #endregion

        #region GetPagedList
        public async  Task<TeacherInServiceCourseTypeListViewModel> GetPagedListAsync(TeacherInServiceCourseTypeSearchRequest request)
        {
            var TeacherInServiceCourseTypes =
                _TeacherInServiceCourseTypes.Where(a => a.TeacherId == request.TeacherId).AsNoTracking()
                    .Include(a => a.Creator).Include(a => a.LasModifier)
                    .Include(a => a.InServiceCourseTypeTitle).OrderByDescending(a => a.CreateDate).AsQueryable();

            var selectedTeacherInServiceCourseTypes = TeacherInServiceCourseTypes.ProjectTo<TeacherInServiceCourseTypeViewModel>(_mappingEngine);

            var query =await   selectedTeacherInServiceCourseTypes
                .Skip((request.PageIndex - 1)*10)
                .Take(10).ToListAsync();

            return new TeacherInServiceCourseTypeListViewModel { SearchRequest = request, TeacherInServiceCourseTypes = query };
        }
        #endregion

        #region IsInDb
        public Task<bool> IsInDb(Guid id)
        {
            return _TeacherInServiceCourseTypes.AnyAsync(a => a.Id == id);
        }
        #endregion


        public async  Task FillEditViewModel(EditTeacherInServiceCourseTypeViewModel viewModel)
        {
            viewModel.InServiceCourseTypeTitles =
               await
                   _titleService.GetAsSelectListItemAsync(TitleType.InServiceCourseType,
                       viewModel.InServiceCourseTypeTitleId);
        }

        public Task<TeacherInServiceCourseTypeViewModel> GetTeacherInServiceCourseTypeViewModel(Guid guid)
        {
            return _TeacherInServiceCourseTypes
                .Include(a => a.Creator).Include(a => a.LasModifier)
                .Include(a => a.InServiceCourseTypeTitle)
                .AsNoTracking()
                .ProjectTo<TeacherInServiceCourseTypeViewModel>(_mappingEngine)
                .FirstOrDefaultAsync(a=>a.Id==guid);
        }

        public async  Task FillAddViewModel(AddTeacherInServiceCourseTypeViewModel viewModel)
        {
            viewModel.InServiceCourseTypeTitles =
             await
                 _titleService.GetAsSelectListItemAsync(TitleType.InServiceCourseType,
                     viewModel.InServiceCourseTypeTitleId);
        }

        public async  Task<AddTeacherInServiceCourseTypeViewModel> GetForCreate(Guid TeacherId)
        {
            return new AddTeacherInServiceCourseTypeViewModel
            {
                InServiceCourseTypeTitles = await
                    _titleService.GetAsSelectListItemAsync(TitleType.InServiceCourseType, null)
            };
        }
    }
}