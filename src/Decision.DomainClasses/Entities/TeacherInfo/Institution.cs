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
    /// نشان دهنده موسسه آموزشی
    /// </summary>
    public class Institution : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام موسسه
        /// </summary>
        public  string Name { get; set; }
        /// <summary>
        /// توضیحاتی در مورد موسسه
        /// </summary>
        public  string Description { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// لیست سوابق تحصیلی مربوط به این موسسه
        /// </summary>
        public  ICollection<EducationalBackground> EducationalBackgrounds{ get; set; }
        #endregion
    }
}
