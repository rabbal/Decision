using System.Collections.Generic;

namespace Decision.ViewModel.ReferentialTeacher
{
    /// <summary>
    /// ویو مدل نمایش لیست ارجاعات استاد
    /// </summary>
    public class ReferentialTeacherListViewModel
    {
        /// <summary>
        /// لیست ویو مدل نمایش ارجاعات استاد
        /// </summary>
        public IEnumerable<ReferentialTeacherViewModel> ReferentialTeachers { get; set; }
    }
}