using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.TeacherInfo;
using Decision.ServiceLayer.Contracts.TeacherInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.ReferentialTeacher;
using EntityFramework.Extensions;

namespace Decision.ServiceLayer.EFServiecs.TeacherInfo
{
    /// <summary>
    /// کلاس ارائه دهنده سروسیس های لازم برای اعمال روی ارجاع استاد
    /// </summary>
    public class ReferentialTeacherService : IReferentialTeacherService
    {
        #region Fields

        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<ReferentialTeacher> _referentialTeachers;
        #endregion

        #region Ctor

        public ReferentialTeacherService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _referentialTeachers = _unitOfWork.Set<ReferentialTeacher>();
            _mappingEngine = mappingEngine;
        }
        #endregion

        #region GetForEdit
        public Task<EditReferentialTeacherViewModel> GetForEditAsync(Guid id)
        {
            return _referentialTeachers.AsNoTracking().ProjectTo<EditReferentialTeacherViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _referentialTeachers.Where(a => a.TeacherId == id && !a.FinishedDate.HasValue)
                .UpdateAsync(a => new ReferentialTeacher
            {
                FinishedDate = DateTime.Now
            });
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditReferentialTeacherViewModel viewModel)
        {
            var referentialTeacher = await _referentialTeachers.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, referentialTeacher);
        }
        #endregion

        #region Create
        public void Create(AddReferentialTeacherViewModel viewModel)
        {
            var referentialTeacher = _mappingEngine.Map<ReferentialTeacher>(viewModel);
            referentialTeacher.ReferencedFromId = _userManager.GetCurrentUserId();
            _referentialTeachers.Add(referentialTeacher);
        }

        public async Task<IEnumerable<Guid>> GetRefersTeacherIds()
        {
            var currentUser = _userManager.GetCurrentUserId();
            return
                await
                    _referentialTeachers.Where(a => a.ReferencedToId == currentUser).Select(a => a.TeacherId).ToListAsync();
        }

        public bool CanManageTeacher(Guid TeacherId)
        {
            var currentUserId = _userManager.GetCurrentUserId();
            var isOperator = _userManager.IsOperator();
            return !isOperator || _referentialTeachers.Any(
                a => a.TeacherId == TeacherId && a.ReferencedToId == currentUserId && !a.FinishedDate.HasValue);
        }


        #endregion
        public Task FinishReferTeacher(Guid TeacherId)
        {
            return DeleteAsync(TeacherId);
        }
    }
}