using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.Common
{
    /// <summary>
    /// 
    /// </summary>
    public enum  TitleCategory:int
    {
        /// <summary>
        /// علمی
        /// </summary>
        [Display(Name = "علمی")]
        Scientific=1,
        /// <summary>
        /// کارگاهی
        /// </summary>
       [Display(Name = "کارگاهی")]
        WorkShop=2,
        /// <summary>
        /// ضمن خدمت
        /// </summary>
        [Display(Name = "ضمن خدمت")]
        InService=3
    }
}
