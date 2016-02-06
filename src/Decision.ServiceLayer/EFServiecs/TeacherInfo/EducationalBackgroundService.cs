using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.Common.Helpers.Extentions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.EducationalBackground;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

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
        private readonly ITitleService _titleService;
        private readonly IInstitutionService _institutionService;
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<EducationalBackground> _educationalBackgrounds;
        #endregion

        #region Ctor

        public EducationalBackgroundService(IUnitOfWork unitOfWork, ITitleService titleService,IInstitutionService institutionService,
            IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _educationalBackgrounds = _unitOfWork.Set<EducationalBackground>();
            _mappingEngine = mappingEngine;
            _titleService = titleService;
            _institutionService = institutionService;
        }
        #endregion

        #region GetForEditAsync
        public async  Task<EditEducationalBackgroundViewModel> GetForEditAsync(Guid id)
        {
            var viewModel =
                await
                    _educationalBackgrounds.AsNoTracking()
                        .ProjectTo<EditEducationalBackgroundViewModel>(_mappingEngine)
                        .FirstOrDefaultAsync(a => a.Id == id);
            if (viewModel == null) return null;
            viewModel.StudyFields =
                await _titleService.GetAsSelectListItemAsync(TitleType.StudyField, viewModel.StudyFieldId);
            viewModel.Institutions =await  _institutionService.GetAsSelectListItemAsync(viewModel.InstitutionId);
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

              if (viewModel.AttachmentScan.HasValue())
                  educationalBackground.Attachment = Convert.FromBase64String(viewModel.AttachmentScan).ResizeImageFile(a4Width, a4height);
            else if (viewModel.AttachmentFile.HasFile())
            {
                educationalBackground.Attachment
                    = viewModel.AttachmentFile.InputStream.ResizeImageFile(a4Width, a4height);
            }

            educationalBackground.LasModifierId = _userManager.GetCurrentUserId();
        }
        #endregion

        #region Create
        public async  Task<EducationalBackgroundViewModel> Create(AddEducationalBackgroundViewModel viewModel)
        {
            var educationalBackground = _mappingEngine.Map<EducationalBackground>(viewModel);
            educationalBackground.CreatorId = _userManager.GetCurrentUserId();

            if (viewModel.AttachmentScan.HasValue())
                educationalBackground.Attachment = Convert.FromBase64String(viewModel.AttachmentScan).ResizeImageFile(a4Width, a4height);
            else if (viewModel.AttachmentFile.HasFile())
            {
                educationalBackground.Attachment
                    = viewModel.AttachmentFile.InputStream.ResizeImageFile(a4Width, a4height);
            }
            _educationalBackgrounds.Add(educationalBackground);
           await  _unitOfWork.SaveChangesAsync();

            return await _educationalBackgrounds
                .Include(a => a.Creator)
                .Include(a => a.LasModifier)
                .Include(a => a.StudyField)
                .Include(a => a.Institution)
                .AsNoTracking().ProjectTo<EducationalBackgroundViewModel>(_mappingEngine).FirstOrDefaultAsync();
        }
        #endregion

        #region GetPagedList
        public async  Task<EducationalBackgroundListViewModel> GetPagedListAsync(EducationalBackgroundSearchRequest request)
        {
            var educationalBackgrounds =
                _educationalBackgrounds.Where(a => a.ApplicantId == request.ApplicantId)
                    .Include(a => a.Creator)
                    .Include(a => a.LasModifier)
                    .Include(a => a.StudyField)
                    .Include(a => a.Institution)
                    .AsNoTracking()
                    .OrderByDescending(a => a.GraduationDate)
                    .AsQueryable();
            
            var selectedEducationalBackgrounds = educationalBackgrounds.ProjectTo<EducationalBackgroundViewModel>(_mappingEngine);

            var query = await selectedEducationalBackgrounds
                .Skip((request.PageIndex - 1)*10)
                .Take(10).ToListAsync();

            return new EducationalBackgroundListViewModel { SearchRequest = request, EducationalBackgrounds = query };
        }
        #endregion


        public Task<bool> IsInDb(Guid guid)
        {
            return _educationalBackgrounds.AnyAsync(a => a.Id == guid);
        }

        public async  Task FillEditViewModel(EditEducationalBackgroundViewModel viewModel)
        {
            viewModel.StudyFields =
              await _titleService.GetAsSelectListItemAsync(TitleType.StudyField, viewModel.StudyFieldId);
            viewModel.Institutions = await _institutionService.GetAsSelectListItemAsync(viewModel.InstitutionId);
        }

        public async  Task<AddEducationalBackgroundViewModel> GetForCreate(Guid ApplicantId)
        {
            return new AddEducationalBackgroundViewModel
            {
                ApplicantId = ApplicantId,
                StudyFields =
                    await _titleService.GetAsSelectListItemAsync(TitleType.StudyField, null),
                Institutions = await _institutionService.GetAsSelectListItemAsync(null)
            };
        }

        public async  Task FillAddViewModel(AddEducationalBackgroundViewModel viewModel)
        {
            viewModel.StudyFields =
             await _titleService.GetAsSelectListItemAsync(TitleType.StudyField, viewModel.StudyFieldId);
            viewModel.Institutions = await _institutionService.GetAsSelectListItemAsync(viewModel.InstitutionId);
        }
    }
}