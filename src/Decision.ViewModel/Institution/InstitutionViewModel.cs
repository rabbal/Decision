using System;
using System.ComponentModel;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Institution
{
    /// <summary>
    /// ویو مدل نمایش موسسه آموزشی
    /// </summary>
    public class InstitutionViewModel: BaseViewModel
    {
        #region Properties
        /// <summary>
        /// آی دی موسسه
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// نام موسسه
        /// </summary>
        [DisplayName("نام")]
        public  string Name { get; set; }

        /// <summary>
        /// توضیحاتی در مورد موسسه
        /// </summary>
        [DisplayName("توضیحات")]
        public  string Description { get; set; }
        #endregion 
    }
}