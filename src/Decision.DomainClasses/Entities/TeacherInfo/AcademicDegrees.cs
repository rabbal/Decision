using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.TeacherInfo
{
    /// <summary>
    /// مقاطع تحصیلات دانشگاهی
    /// </summary>
    public enum  AcademicDegrees
    {
        /// <summary>
        ///  لیسانس 
        /// </summary>
        [Display(Name = "لیسانس")]
        BS,
        /// <summary>
        /// فوق لیسانس
        /// </summary>
        [Display(Name = "فوق لیسانس")]
        MS,
        /// <summary>
        /// دکتری
        /// </summary>
       [Display(Name = "دکتری")]
        PhD,
        /// <summary>
        /// فوق دکتری
        /// </summary>
        [Display(Name = "فوق دکتری")]
        PostDoc,
        /// <summary>
        /// معادل دکتری به لحاظ ایثارگری
        /// </summary>
        [Display(Name = "معادل دکترا به لحاظ ایثارگری")]
        EPhD


    }
}
