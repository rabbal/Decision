using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.Evaluations
{
    /// <summary>
    /// انواع سوالات بر مبنای حالت پاسخ دادن
    /// </summary>
    public enum QuestionType
    {
        /// <summary>
        /// عددی باشد
        /// </summary>
        [Display(Name = "امتیازی")]
        Number, 
        /// <summary>
        /// رشته کوتاه باشد
        /// </summary>
        [Display(Name = "جواب کوتاه")]
        Input,
        /// <summary>
        /// رشته مفصل باشد
        /// </summary>
        [Display(Name = "جواب کامل")]
        TextArea,
        /// <summary>
        /// لیستی از گزینه ها با قابلیت چند انتخابی 
        /// </summary>
        [Display(Name = "چند گزینه ای چند انتخابی")]
        CheckBoxList,
        /// <summary>
        /// لیستی از گزینه ها با قابلیت تک انتخابی
        /// </summary>
        [Display(Name = "چند گزینه ای تک انتخابی")]
        RadioButtonList
    }
}
