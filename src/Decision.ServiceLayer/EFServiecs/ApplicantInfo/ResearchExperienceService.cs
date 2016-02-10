using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.ResearchExperience;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.ApplicantInfo
{
    /// <summary>
    /// کلاس ارائه دهنده سرویس های لازم برای سابقه های پژوهشی
    /// </summary>
    public class ResearchExperienceService : IResearchExperienceService
    {
        #region Fields

        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<ResearchExperience> _researchExperiences;
        #endregion

        #region Ctor

        public ResearchExperienceService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _researchExperiences = _unitOfWork.Set<ResearchExperience>();
            _mappingEngine = mappingEngine;
        }
        #endregion

        #region GetForEdit
        public Task<EditResearchExperienceViewModel> GetForEditAsync(Guid id)
        {
            return _researchExperiences.AsNoTracking().ProjectTo<EditResearchExperienceViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _researchExperiences.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditResearchExperienceViewModel viewModel)
        {
            var researchExperience = await _researchExperiences.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, researchExperience);
        }
        #endregion

        #region Create
        public async  Task<ResearchExperienceViewModel> Create(AddResearchExperienceViewModel viewModel)
        {
            var researchExperience = _mappingEngine.Map<ResearchExperience>(viewModel);
            _researchExperiences.Add(researchExperience);
            await _unitOfWork.SaveChangesAsync();
            return await GetResearchExperienceViewModel(researchExperience.Id);
        }
        #endregion

        #region GetPagedList
        public async Task<ResearchExperienceListViewModel> GetPagedListAsync(ResearchExperienceSearchRequest request)
        {
            var researches =
                _researchExperiences.Include(a => a.CreatedBy)
                    .Include(a => a.ModifiedBy)
                    .Where(a => a.ApplicantId == request.ApplicantId)
                    .AsNoTracking().OrderByDescending(a=>a.CreatedOn)
                    .AsQueryable();

            var selectedCities = researches.ProjectTo<ResearchExperienceViewModel>(_mappingEngine);

            var resultsToSkip = (request.PageIndex - 1)*10;
            var query = await selectedCities
                .Skip(()=>resultsToSkip)
                .Take(10).ToListAsync();

            return new ResearchExperienceListViewModel { SearchRequest = request, ResearchExperiences = query };
        }
        #endregion


        public Task<bool> IsInDb(Guid id)
        {
            return _researchExperiences.AnyAsync(a => a.Id == id);
        }

        public Task<ResearchExperienceViewModel> GetResearchExperienceViewModel(Guid guid)
        {
            return
                _researchExperiences.Include(a => a.CreatedBy)
                    .Include(a => a.ModifiedBy)
                    .ProjectTo<ResearchExperienceViewModel>(_mappingEngine)
                    .FirstOrDefaultAsync(a => a.Id == guid);
        }
    }
}