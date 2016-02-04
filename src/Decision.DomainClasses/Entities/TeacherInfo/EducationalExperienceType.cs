using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.TeacherInfo
{
    /// <summary>
    /// انواع سوابق آموزشی
    /// </summary>
    public enum  EducationalExperienceType
    {
        /// <summary>
        /// موضوعات مورد علاقه استاد برای تدریس
        /// </summary>
        FavoriteIssue,
        /// <summary>
        /// سوابق تدریس در مراکز علمی
        /// </summary>
        TeachingInScientificCenter,
        /// <summary>
        /// سوابق تدریس در مراکز سازمانی
        /// </summary>
        TeachingInOrganizational,
        /// <summary>
        /// اولویت های تصویب شده برای استاد به منظور آموزش دیدن
        /// </summary>
        AdoptedPrioritiesInCommittee
    }
}
