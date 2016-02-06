using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.ApplicantInfo
{
    /// <summary>
    /// انواع پژوهش
    /// </summary>
    public enum PresenterType
    {
        /// <summary>
        /// عمومی
        /// </summary>
       [Display(Name = "عمومی")]
        General , 
        /// <summary>
        /// علمی
        /// </summary>
        [Display(Name = "علمی")]
        Academic
    }
}
