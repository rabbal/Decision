using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.TeacherInfo
{
    /// <summary>
    /// انواع پژوهش
    /// </summary>
    public enum ResearchType
    {
        /// <summary>
        /// کتاب
        /// </summary>
       [Display(Name = "کتاب")]
        Book , 
        /// <summary>
        /// مقاله
        /// </summary>
        [Display(Name = "مقاله")]
        Article
    }
}
