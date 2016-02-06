using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Decision.ViewModel.ReferentialApplicant;

namespace Decision.ServiceLayer.Contracts.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس ارجاع متقاضی
    /// </summary>
    public interface IReferentialApplicantService
    {
        /// <summary>
        /// واکشی ارجاع متقاضی برای ویرایش
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EditReferentialApplicantViewModel> GetForEditAsync(Guid id);

        /// <summary>
        /// حذف ارجاع متقاضی
        /// </summary>
        /// <param name="id">آی دی  متقاضی</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// ویرایش ارجاع متقاضی
        /// </summary>
        /// <param name="viewModel">ویو مدل ویرایش ارجاع متقاضی</param>
        /// <returns></returns>
        Task EditAsync(EditReferentialApplicantViewModel viewModel);

        /// <summary>
        /// درج ارجاع متقاضی جدید
        /// </summary>
        /// <param name="viewModel">ویو مدل درج ارجاع متقاضی</param>
        void Create(AddReferentialApplicantViewModel viewModel);

        Task<IEnumerable<Guid>> GetRefersApplicantIds();

        bool CanManageApplicant(Guid ApplicantId);
        Task FinishReferApplicant(Guid ApplicantId);
    }
}