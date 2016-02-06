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
using Decision.ViewModel.EducationalExperience;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.ApplicantInfo
{
    /// <summary>
    /// کلاس ارائه دهنده سروسیس های لازم برای اعمال روی سابقه آموزشی
    /// </summary>
    public class EducationalExperienceService : IEducationalExperienceService
    {
        #region Fields

        private readonly ITitleService _titleService;
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<EducationalExperience> _educationalExperiences;
        #endregion

        #region Ctor

        public EducationalExperienceService(IUnitOfWork unitOfWork, ITitleService titleService, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _titleService = titleService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _educationalExperiences = _unitOfWork.Set<EducationalExperience>();
            _mappingEngine = mappingEngine;
        }
        #endregion

        public async Task<EditEducationalExperienceViewModel> GetForEditAsync(Guid id)
        {
            var viewModel = await _educationalExperiences.AsNoTracking().ProjectTo<EditEducationalExperienceViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
            if (viewModel == null) return null;
            viewModel.Titles = await _titleService.GetAsSelectListItemAsync(TitleType.CourseContent, viewModel.TitleId);
            return viewModel;
        }

        public Task DeleteAsync(Guid id)
        {
            return _educationalExperiences.Where(a => a.Id == id).DeleteAsync();
        }

        public async Task EditAsync(EditEducationalExperienceViewModel viewModel)
        {
            var educationalExperience = await _educationalExperiences.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, educationalExperience);
            educationalExperience.LasModifierId = _userManager.GetCurrentUserId();
        }

        public async Task<EducationalExperienceViewModel> Create(AddEducationalExperienceViewModel viewModel)
        {
            var educationalExperience = _mappingEngine.Map<EducationalExperience>(viewModel);
            educationalExperience.CreatorId = _userManager.GetCurrentUserId();
            _educationalExperiences.Add(educationalExperience);
            await _unitOfWork.SaveChangesAsync();
            return await GetEducationalExperienceViewModel(educationalExperience.Id);
        }

        public async Task<EducationalExperienceListViewModel> GetPagedListAsync(EducationalExperienceSearchRequest request)
        {
            var educationalExperiences =
                _educationalExperiences.Where(a => a.ApplicantId == request.ApplicantId & a.Type == request.Type)
                    .AsNoTracking()
                    .Include(a => a.Title)
                    .Include(a => a.Creator)
                    .Include(a => a.LasModifier)
                    .OrderBy(a => a.Id)
                    .AsQueryable();

            var selectedEducationalExperiences = educationalExperiences.ProjectTo<EducationalExperienceViewModel>(_mappingEngine);

            var query = await selectedEducationalExperiences
                .Skip((request.PageIndex - 1) * 10)
                .Take(10).ToListAsync();

            return new EducationalExperienceListViewModel { SearchRequest = request, EducationalExperiences = query };
        }


        public Task<bool> IsInDb(Guid id)
        {
            return _educationalExperiences.AnyAsync(a => a.Id == id);
        }


        public async Task FillAddViewModel(AddEducationalExperienceViewModel viewModel)
        {
            viewModel.Titles = await _titleService.GetAsSelectListItemAsync(TitleType.CourseContent, null);
        }

        public async Task FillEditViewModel(EditEducationalExperienceViewModel viewModel)
        {
            viewModel.Titles = await _titleService.GetAsSelectListItemAsync(TitleType.CourseContent, viewModel.TitleId);
        }

        public async Task<AddEducationalExperienceViewModel> GetForCreate(Guid ApplicantId, EducationalExperienceType type)
        {
            return new AddEducationalExperienceViewModel
            {
                Type = type,
                ApplicantId = ApplicantId,
                Titles = await _titleService.GetAsSelectListItemAsync(TitleType.CourseContent, null)
            };
        }

        public Task<EducationalExperienceViewModel> GetEducationalExperienceViewModel(Guid guid)
        {
            return
                _educationalExperiences.Include(a => a.Title)
                 .Include(a => a.Creator)
                    .Include(a => a.LasModifier)
                    .ProjectTo<EducationalExperienceViewModel>(_mappingEngine)
                    .FirstOrDefaultAsync(a => a.Id == guid);
        }
    }
}