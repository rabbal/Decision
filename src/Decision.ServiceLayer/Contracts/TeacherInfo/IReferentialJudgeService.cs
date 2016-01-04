using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Decision.ViewModel.ReferentialTeacher;

namespace Decision.ServiceLayer.Contracts.TeacherInfo
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس ارجاع استاد
    /// </summary>
    public interface IReferentialTeacherService
    {
        /// <summary>
        /// واکشی ارجاع استاد برای ویرایش
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EditReferentialTeacherViewModel> GetForEditAsync(Guid id);

        /// <summary>
        /// حذف ارجاع استاد
        /// </summary>
        /// <param name="id">آی دی  استاد</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ویرایش ارجاع استاد
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش ارجاع استاد</param>
        /// <returns></returns>
        Task EditAsync(EditReferentialTeacherViewModel viewModel);

        /// <summary>
        /// درج ارجاع استاد جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج ارجاع استاد</param>
        void Create(AddReferentialTeacherViewModel viewModel);

        Task<IEnumerable<Guid>> GetRefersTeacherIds();

        bool CanManageTeacher(Guid TeacherId);
        Task FinishReferTeacher(Guid TeacherId);
    }
}