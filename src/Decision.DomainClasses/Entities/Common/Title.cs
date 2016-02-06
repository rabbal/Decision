using System.Collections.Generic;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.DomainClasses.Entities.ApplicantInfo;

namespace Decision.DomainClasses.Entities.Common
{
    /// <summary>
    /// نشان دهنده عنوان های دروس
    /// </summary>
    public class Title : BaseEntity
    {

        #region Properties
        /// <summary>
        /// نام عنوان
        /// </summary>
        public  string Name { get; set; }
        /// <summary>
        /// نوع عنوان 
        /// </summary>
        public  TitleType Type { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// لیست اساتیدی که این عنوان را به عنوان سمت متقاضی پذیرفته اند
        /// </summary>
        public  ICollection<Applicant> Applicants  { get; set; }
        /// <summary>
        /// لیست ارزش گذارانی این عنوان را دارند
        /// </summary>
        public  ICollection<Appraiser> Appraisers  { get; set; }
      
        /// <summary>
        /// لیست سوابق تحصیلی که این عنوان را دارند
        /// </summary>
        public  ICollection<EducationalBackground> EducationalBackgrounds  { get; set; }

        /// <summary>
        /// لیست سوابق آموزشی که این عنوان را دارند
        /// </summary>
        public  ICollection<EducationalExperience> EducationalExperiences  { get; set; }
        /// <summary>
        /// لیست سوابق کاری که این عنوان را دارند
        /// </summary>
        public  ICollection<WorkExperience> WorkExperiences  { get; set; }
        #endregion

    }
}
