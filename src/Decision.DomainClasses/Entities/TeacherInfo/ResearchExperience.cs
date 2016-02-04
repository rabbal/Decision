using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.Common;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.TeacherInfo
{
    /// <summary>
    /// نشان دهنده سابقه پژوهشی استاد
    /// </summary>
    public class ResearchExperience : BaseEntity
    {
        #region Properties
        /// <summary>
        /// عنوای پژوهش
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// نوع پژوهش
        /// </summary>
        public ResearchType ResearchType { get; set; }
        /// <summary>
        ///  چاپ شده توسط  
        /// </summary>
        public string PublishedIn { get; set; }
        /// <summary>
        ///  سال انتشار  
        /// </summary>
        public DateTime PublishDate { get; set; }
        /// <summary>
        /// توضیحات اضافی
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// استاد انجام دهنده پژوهش
        /// </summary>
        public Teacher Teacher { get; set; }
        /// <summary>
        /// آی دی استاد انجام دهنده پژوهش
        /// </summary>
        public Guid TeacherId { get; set; }
        #endregion
    }
}
