using System;
using System.Collections.Generic;

using System.Xml.Linq;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.DomainClasses.Entities.TeacherInfo;
using Decision.DomainClasses.Entities.PrivateMessage;
using Decision.Utility;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Decision.DomainClasses.Entities.Users
{
    /// <summary>
    /// نشان دهنده کاربر 
    /// </summary>
    public class User : IdentityUser<Guid, UserLogin, UserRole, UserClaim>
    {
        #region Ctor
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public User()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
            EmailConfirmed = true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// نشانده دهنده قفل بودن کاربر است
        /// 
        /// </summary>
        public virtual bool IsBanned { get; set; }
        /// <summary>
        /// نام کاربر
        /// </summary>
        public virtual string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی کاربر
        /// </summary>
        public virtual string LastName { get; set; }
        /// <summary>
        /// آیا کاربر سیستمی است؟
        /// </summary>
        public virtual bool IsSystemAccount { get; set; }
        /// <summary>
        /// آخرین آی پی که ثبت شده برای کاربر
        /// </summary>
        public virtual string LastIp { get; set; }
        /// <summary>
        /// تاریخ آخرین ورود کاربر
        /// </summary>
        public virtual DateTime? LastLoginDate { get; set; }
        /// <summary>
        /// نشان دهنده این است که آیا دسترسی های کاربر تغییر کرده است ؟
        /// </summary>
        public virtual bool IsChangedPermissions { get; set; }
        /// <summary>
        /// دسترسی های مستقیم کاربر بدون وابستی به گروه های کاربری او
        /// </summary>
        public virtual string DirectPermissions { get; set; }
        /// <summary>
        ///  ساختار اکس ام ال دسترسی های مستقیم کاربر بدون وابستی به گروه های کاربری او
        /// </summary>
        public XElement XmlDirectPermissions
        {
            get { return XElement.Parse(DirectPermissions); }
            set { DirectPermissions = value.ToString(); }
        }

        /// <summary>
        /// لیست آی دی هایی که کاربر با آن به سیستم متصل است  
        /// <remarks>منظور همان آی دی های تولیدی هنگام گشودن یک تب در مرورگر در صورت استفاده از 
        /// signalr
        /// </remarks>
        /// </summary>
        public HashSet<string> ConnectionIds { get; set; }
        /// <summary>
        /// برای مسائل مربوط به همزمانی ها
        /// </summary>
        public virtual byte[] RowVersion { get; set; }

        #endregion

        #region NavigationProperties
        /// <summary>
        /// لیست لاگ های داده ،کاربر
        /// </summary>
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
        /// <summary>
        /// لیست اساتیدی که این کاربر به عنوان مدیر نگارش آنها را تایید کرده است
        /// </summary>
        public virtual ICollection<Teacher> ApprovedTeachers { get; set; }
        /// <summary>
        /// لیست گفتگوهای ارسالی کاربر
        /// </summary>

        public virtual ICollection<Conversation> SentConversations { get; set; }
        /// <summary>
        /// لیست گفتگوهای دریافتی کاربر
        /// </summary>
        public virtual ICollection<Conversation> ReceivedConversations { get; set; }
        /// <summary>
        /// لیست پیغام های ارسال شده توسط کاربر
        /// </summary>
        public virtual ICollection<Message> SentMessages { get; set; }
        /// <summary>
        /// لیست ارجاعتی که این کاربر فرستنده آنها است
        /// </summary>
        public virtual ICollection<ReferentialTeacher> SentReferentialTeachers { get; set; }
        /// <summary>
        /// لیست ارجاعاتی که این کاربر دریافت کنند آنها بوده است
        /// </summary>
        public virtual ICollection<ReferentialTeacher> ReceivedReferentialTeachers { get; set; }

        /// <summary>
        /// لیست عنوان هایی که این کاربر درج کننده آنها بوده است
        /// </summary>
        public virtual ICollection<Title> CreatedTitles { get; set; }
        /// <summary>
        /// لیست عنوان هایی که این کاربر آخرین ویرایش کننده آنها بوده است
        /// </summary>
        public virtual ICollection<Title> ModifiedTitles { get; set; }
        /// <summary>
        /// لیست ارزیاب هایی که این کاربر درج کننده آنها بوده است
        /// </summary>
        public virtual ICollection<Appraiser> CreatedAppraisers { get; set; }
        /// <summary>
        /// لیست ارزیاب هایی که این کاربر آخرین ویرایش  کننده آنها بوده است
        /// </summary>
        public virtual ICollection<Appraiser> ModifiedAppraisers { get; set; }

        /// <summary>
        /// لیست ارزیابی_استاد هایی که این کاربر درج کننده آنها بوده است
        /// </summary>
        public virtual ICollection<EntireEvaluation> CreatedEntireEvaluations { get; set; }

        /// <summary>
        /// لیست ارزیابی_استاد هایی که این کاربر آخرین ویرایش کننده آنها بوده است
        /// </summary>
        public virtual ICollection<EntireEvaluation> ModifiedEntireEvaluations { get; set; }
        /// <summary>
        /// لیست مصاحبه های  که این کاربر  درج کننده آنها بوده است
        /// </summary>
        public virtual ICollection<Interview> CreatedInterviews { get; set; }
        /// <summary>
        /// لیست مصاحبه هایی که این کاربر آخرین ویرایش کننده آنها بوده است
        /// </summary>
        public virtual ICollection<Interview> ModifiedInterviews { get; set; }
        /// <summary>
        /// لیست ارزیابی از مقالات های  که این کاربر  درج کننده آنها بوده است
        /// </summary>
        public virtual ICollection<ArticleEvaluation> CreatedArticleEvaluations { get; set; }
        /// <summary>
        /// لیست ارزیابی از مقالات های  که این کاربر  آخرین ویرایش کننده آنها بوده است
        /// </summary>
        public virtual ICollection<ArticleEvaluation> ModifiedArticleEvaluations { get; set; }
        /// <summary>
        /// لیست "پاسخ به سوالات مقداری در ارزیابی" هایی   که این کاربر  درج کننده آنها بوده است
        /// </summary>
        public virtual ICollection<ArticleEvaluationQuestion> CreatedArticleEvaluationQuestions { get; set; }
        /// <summary>
        /// لیست "پاسخ به سوالات مقداری در ارزیابی" هایی   که این کاربر  آخرین ویرایش کننده آنها بوده است
        /// </summary>
        public virtual ICollection<ArticleEvaluationQuestion> ModifiedArticleEvaluationQuestions { get; set; }
        /// <summary>
        /// لیست سوالاتی هایی   که این کاربر  درج کننده آنها بوده است
        /// </summary>
        public virtual ICollection<Question> CreatedQuestions { get; set; }
        /// <summary>
        /// لیست سوالاتی هایی   که این کاربر  آخرین ویرایش کننده آنها بوده است
        /// </summary>
        public virtual ICollection<Question> ModifiedQuestions { get; set; }
        /// <summary>
        /// لیست آدرس هایی  که این کاربر  درج کننده آنها بوده است
        /// </summary>
        public virtual ICollection<Address> CreatedAddresses { get; set; }
        ///<summary>
        /// لیست آدرس هایی  که این کاربر  آخرین ویرایش کننده آنها بوده است
        /// </summary>
        public virtual ICollection<Address> ModifiedAddresses { get; set; }
        /// <summary>
        /// لیست استاد هایی  که این کاربر  درج کننده آنها بوده است
        /// </summary>
        public virtual ICollection<Teacher> CreatedTeachers { get; set; }
        /// <summary>
        /// لیست استاد هایی  که این کاربر  آخرین ویرایش کننده آنها بوده است
        /// </summary>
        public virtual ICollection<Teacher> ModifiedTeachers { get; set; }
        /// <summary>
        /// لیست سوابق تحصیلی هایی  که این کاربر  درج کننده آنها بوده است
        /// </summary>
        public virtual ICollection<EducationalBackground> CreatedEducationalBackgrounds { get; set; }

        /// <summary>
        /// لیست سوابق تحصیلی هایی  که این کاربر  آخرین ویرایش  کننده آنها بوده است
        /// </summary>
        public virtual ICollection<EducationalBackground> ModifiedEducationalBackgrounds { get; set; }

        /// <summary>
        /// لیست موسساتی  که این کاربر  درج کننده آنها بوده است
        /// </summary>

        public virtual ICollection<Institution> CreatedInstitutions { get; set; }

        /// <summary>
        /// لیست موسساتی  که این کاربر  آخرین ویرایش کننده آنها بوده است
        /// </summary>

        public virtual ICollection<Institution> ModifiedInstitutions { get; set; }
        /// <summary>
        /// لیست مقالاتی  که این کاربر  درج کننده آنها بوده است
        /// </summary>

        public virtual ICollection<Article> CreatedArticles { get; set; }

        /// <summary>
        /// لیست مقالاتی  که این کاربر  آخرین ویرایش کننده آنها بوده است
        /// </summary>

        public virtual ICollection<Article> ModifiedArticles { get; set; }
        /// <summary>
        /// لیست سوابق پژوهشی  که این کاربر  درج کننده آنها بوده است
        /// </summary>
        public virtual ICollection<ResearchExperience> CreatedReseachExperiences { get; set; }
        /// <summary>
        /// لیست سوابق پژوهشی  که این کاربر  آخرین ویرایش کننده آنها بوده است
        /// </summary>
        public virtual ICollection<ResearchExperience> ModifiedReseachExperiences { get; set; }
        /// <summary>
        /// لیست سوابق تدریسی  که این کاربر  درج کننده آنها بوده است
        /// </summary>
        public virtual ICollection<EducationalExperience> CreatedEducationalExperiences { get; set; }

        /// <summary>
        /// لیست سوابق تدریسی  که این کاربر  آخرین ویرایش کننده آنها بوده است
        /// </summary>
        public virtual ICollection<EducationalExperience> ModifiedEducationalExperiences { get; set; }
        /// <summary>
        /// لیست مراکز کار آموزی که  این کاربر  درج کننده آنها بوده است
        /// </summary>
        public virtual ICollection<TrainingCenter> CreatedTrainingCenters { get; set; }

        /// <summary>
        /// لیست مراکز کار آموزی که  این کاربر  آخرین ویرایش  کننده آنها بوده است
        /// </summary>
        public virtual ICollection<TrainingCenter> ModifiedTrainingCenters { get; set; }
        /// <summary>
        /// لیست  دوره های کار آموزی که  این کاربر  درج کننده آنها بوده است
        /// </summary>
        public virtual ICollection<TrainingCourse> CreatedTrainingCourses { get; set; }

        /// <summary>
        /// لیست  دوره های کار آموزی که  این کاربر  آخرین ویرایش کننده آنها بوده است
        /// </summary>
        public virtual ICollection<TrainingCourse> ModifiedTrainingCourses { get; set; }
        /// <summary>
        /// لیست  سوابق کاری که  این کاربر  درج کننده آنها بوده است
        /// </summary>
        public virtual ICollection<WorkExperience> CreatedWorkExperiences { get; set; }
        /// <summary>
        /// لیست  سوابق کاری که  این کاربر  آخرین ویرایش کننده آنها بوده است
        /// </summary>
        public virtual ICollection<WorkExperience> ModifiedWorkExperiences { get; set; }
        /// <summary>
        ///TeacherInServiceCourseType لیست  
        ///  که  این کاربر  درج کننده آنها بوده است
        /// </summary>
        public virtual ICollection<TeacherInServiceCourseType> CreatedTeacherInServiceCourseTypes { get; set; }

        /// <summary>
        ///TeacherInServiceCourseType لیست  
        ///  که  این کاربر  آخرین ویرایش کننده آنها بوده است
        /// </summary>
        public virtual ICollection<TeacherInServiceCourseType> ModifiedTeacherInServiceCourseTypes { get; set; }
        #endregion
    }
}
