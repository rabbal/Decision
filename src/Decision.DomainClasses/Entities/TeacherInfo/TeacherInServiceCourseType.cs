using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.Common;

namespace Decision.DomainClasses.Entities.TeacherInfo
{
    /// <summary>
    /// نشان دهنده این است که یک استاد در  یک نوع دوره ضمن خدمت چند ساعت گذرانده
    /// </summary>
    public class TeacherInServiceCourseType : BaseEntity
    {
        #region Properties
        /// <summary>
        /// ساعات گذرانده 
        /// </summary>
        public  decimal HoursCount { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// استاد مربوطه
        /// </summary>
        public  Teacher Teacher { get; set; }
        /// <summary>
        /// آی دی استاد مربوطه
        /// </summary>
        public  Guid TeacherId { get; set; }
        /// <summary>
        /// عنوان دوره ضمن خدمت
        /// </summary>
        public  Title InServiceCourseTypeTitle { get; set; }
        /// <summary>
        /// آی دی عنوان دوره ضمن خدمت
        /// </summary>
        public  Guid InServiceCourseTypeTitleId { get; set; }
        #endregion
    }
}
