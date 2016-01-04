using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.Common;

namespace Decision.DomainClasses.Entities.TeacherInfo
{
    /// <summary>
    /// نشان دهنده مرکز کار آموزی
    /// </summary>
    public class TrainingCenter : BaseEntity
    {
       

        #region Properties
        /// <summary>
        /// نام مرکز کار آموزی
        /// </summary>
        public  string CenterName { get; set; }
        /// <summary>
        /// شماره تلفن 1
        /// </summary>
        public  string PhoneNumber1 { get; set; }
        /// <summary>
        /// شماره تلفن 2
        /// </summary>
        public  string PhoneNumber2 { get; set; }
        /// <summary>
        /// نشانی به همراه جزییات
        /// </summary>
        public  string Location { get; set; }
        /// <summary>
        /// توضیحات در صورت نیاز
        /// </summary>
        public  string Description { get; set; }
        /// <summary>
        /// شهر
        /// </summary>
        public  string City { get; set; }
        /// <summary>
        /// استان
        /// </summary>
        public  string State { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// دوره های کار آموزی
        /// </summary>
        public  ICollection<TrainingCourse> Courses  { get; set; }
     
      
        #endregion
    }
}
