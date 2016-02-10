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
        
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<EducationalExperience> _educationalExperiences;
        #endregion

        #region Ctor

        public EducationalExperienceService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _educationalExperiences = _unitOfWork.Set<EducationalExperience>();
            _mappingEngine = mappingEngine;
        }
        #endregion

        public async Task<EditEducationalExperienceViewModel> GetForEditAsync(Guid id)
        {
            var viewModel = await _educationalExperiences.AsNoTracking().ProjectTo<EditEducationalExperienceViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
           
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
        }

        public async Task<EducationalExperienceViewModel> Create(AddEducationalExperienceViewModel viewModel)
        {
            var educationalExperience = _mappingEngine.Map<EducationalExperience>(viewModel);
            _educationalExperiences.Add(educationalExperience);
            await _unitOfWork.SaveAllChangesAsync(auditUserId:_userManager.GetCurrentUserId());
            return await GetEducationalExperienceViewModel(educationalExperience.Id);
        }

        public async Task<EducationalExperienceListViewModel> GetPagedListAsync(EducationalExperienceSearchRequest request)
        {
            var educationalExperiences =
                _educationalExperiences.Where(a => a.ApplicantId == request.ApplicantId)
                    .AsNoTracking()
                    .Include(a => a.CreatedBy)
                    .Include(a => a.ModifiedBy).OrderByDescending(a=>a.CreatedOn)
                    .AsQueryable();

            var selectedEducationalExperiences = educationalExperiences.ProjectTo<EducationalExperienceViewModel>(_mappingEngine);
            var resultsToSkip = (request.PageIndex - 1)*10;
            var query = await selectedEducationalExperiences
                .Skip(()=>resultsToSkip)
                .Take(10).ToListAsync();

            return new EducationalExperienceListViewModel { SearchRequest = request, EducationalExperiences = query };
        }
        
        public Task<bool> IsInDb(Guid id)
        {
            return _educationalExperiences.AnyAsync(a => a.Id == id);
        }
        
        public Task<EducationalExperienceViewModel> GetEducationalExperienceViewModel(Guid guid)
        {
            return
                _educationalExperiences
                 .Include(a => a.CreatedBy)
                    .Include(a => a.ModifiedBy)
                    .ProjectTo<EducationalExperienceViewModel>(_mappingEngine)
                    .FirstOrDefaultAsync(a => a.Id == guid);
        }
    }
}