using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.Common.Helpers.Extentions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.TeacherInfo;
using Decision.ServiceLayer.Contracts.TeacherInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.ResearchExperience;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.TeacherInfo
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
            researchExperience.LasModifierId = _userManager.GetCurrentUserId();
        }
        #endregion

        #region Create
        public async  Task<ResearchExperienceViewModel> Create(AddResearchExperienceViewModel viewModel)
        {
            var researchExperience = _mappingEngine.Map<ResearchExperience>(viewModel);
            researchExperience.CreatorId = _userManager.GetCurrentUserId();
            _researchExperiences.Add(researchExperience);
            await _unitOfWork.SaveChangesAsync();
            return await GetResearchExperienceViewModel(researchExperience.Id);
        }
        #endregion

        #region GetPagedList
        public async Task<ResearchExperienceListViewModel> GetPagedListAsync(ResearchExperienceSearchRequest request)
        {
            var cities =
                _researchExperiences.Include(a => a.Creator)
                    .Include(a => a.LasModifier)
                    .Where(a => a.TeacherId == request.TeacherId)
                    .AsNoTracking()
                    .OrderByDescending(a => a.Title)
                    .AsQueryable();

            var selectedCities = cities.ProjectTo<ResearchExperienceViewModel>(_mappingEngine);


            var query = await selectedCities
                .Skip((request.PageIndex - 1) * 10)
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
                _researchExperiences.Include(a => a.Creator)
                    .Include(a => a.LasModifier)
                    .ProjectTo<ResearchExperienceViewModel>(_mappingEngine)
                    .FirstOrDefaultAsync(a => a.Id == guid);
        }
    }
}