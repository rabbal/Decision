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
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.TrainingCourse;
using EFSecondLevelCache;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.ApplicantInfo
{
    /// <summary>
    /// کلاس سرویس دهنده کار با کلاس دوره های کارآموزی
    /// </summary>
    public class TrainingCourseService : ITrainingCourseService
    {

        #region Fields

        private readonly IDbSet<TrainingCenter> _centers;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<TrainingCourse> _courses;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor

        public TrainingCourseService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _unitOfWork = unitOfWork;
            _mappingEngine = mappingEngine;
            _userManager = userManager;
            _courses = _unitOfWork.Set<TrainingCourse>();
          
            _centers = _unitOfWork.Set<TrainingCenter>();
        }
        #endregion

        #region GetByTrainingCenterIdAsync
        public Task<IEnumerable<TrainingCourseViewModel>> GetByTrainingCenterIdAsync(Guid centerId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GetForEditAsync
        public Task<EditTrainingCourseViewModel> GetForEditAsync(Guid id)
        {
            return _courses.ProjectTo<EditTrainingCourseViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
        }
        #endregion

        #region DeleteAsync
        public Task DeleteAsync(Guid id)
        {
            return _courses.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region EditAsync
        public async Task EditAsync(EditTrainingCourseViewModel viewModel)
        {
            var course = await _courses.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, course);
            course.LasModifierId = _userManager.GetCurrentUserId();

        }

        #endregion

        #region Create
        public async  Task<TrainingCourseViewModel> Create(AddTrainingCourseViewModel viewModel)
        {
            var course = _mappingEngine.Map<TrainingCourse>(viewModel);
            course.CreatorId = _userManager.GetCurrentUserId();
            _courses.Add(course);
            await _unitOfWork.SaveChangesAsync();
            return await GetTrainingCourseViewModel(course.Id);
        }

        #endregion

        #region GetPagedListAsync
        public async  Task<TrainingCourseListViewModel> GetPagedListAsync(TrainingCourseSearchRequest request)
        {
            var courses =
                _courses.AsNoTracking()
                    .Where(a => a.TrainingCenterId == request.TrainingCenterId)
                    .Include(a => a.Creator)
                    .Include(a => a.LasModifier)
                    .OrderBy(a => a.CreateDate)
                    .AsQueryable();

            if (request.Term.HasValue())
                courses = courses.Where(a => a.CourseCode.Contains(request.Term));

            var result = courses.ProjectTo<TrainingCourseViewModel>(_mappingEngine);

            var query =await  result
                    .Skip((request.PageIndex - 1) * 10)
                    .Take(10)
                    .ToListAsync();

            return new TrainingCourseListViewModel { SearchRequest = request, TrainingCourses = query };
        }
        #endregion

        #region GetAsSelectListByTrainingCenterIdAsync
        public async Task<IEnumerable<SelectListItem>> GetAsSelectListByTrainingCenterIdAsync(Guid trainingCenterId, Guid? selectedId)
        {
            var courses = await _courses.AsNoTracking()
                   .Where(a => a.TrainingCenterId == trainingCenterId)
                   .OrderByDescending(a => a.CourseCode)
                   .ProjectTo<SelectListItem>(_mappingEngine)
                   .Cacheable()
                   .ToListAsync();
            if (selectedId != null) courses.ForEach(a => a.Selected = a.Value == selectedId.Value.ToString());
            return courses;
        }
        #endregion

        #region IsExistCourseCode
        public Task<bool> IsExistCourseCode(string code, Guid? id, Guid centerId)
        {
            return id == null
                ? _courses.AnyAsync(a => a.TrainingCenterId == centerId && a.CourseCode == code)
                : _courses.AnyAsync(a => a.Id != id.Value && a.TrainingCenterId == centerId && a.CourseCode == code);
        }
        #endregion

        #region IsInDb
        public Task<bool> IsInDb(Guid id)
        {
            return _courses.AnyAsync(a => a.Id == id);
        }
        #endregion

        #region GetCenterId
        public Task<TrainingCourse> Get(Guid id)
        {
            return _courses.Where(a => a.Id == id).AsNoTracking().Include(a=>a.TrainingCenter).FirstOrDefaultAsync();
        }
        #endregion

        public Task<TrainingCourse> GetTrainingCourseOfApplicant(Guid courseId)
        {
            return _courses.Include(a => a.TrainingCenter).FirstOrDefaultAsync(a => a.Id == courseId);
        }

        public Task<TrainingCourseViewModel> GetTrainingCourseViewModel(Guid id)
        {
            return _courses.Include(a=>a.Creator).Include(a=>a.LasModifier).ProjectTo<TrainingCourseViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
