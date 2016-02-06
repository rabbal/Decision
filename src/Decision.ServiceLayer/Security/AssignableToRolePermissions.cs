using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace Decision.ServiceLayer.Security
{
    public static class AssignableToRolePermissions
    {
        #region Fields

        private static Lazy<IEnumerable<PermissionModel>> _permissionsLazy =
            new Lazy<IEnumerable<PermissionModel>>(GetPermision, LazyThreadSafetyMode.ExecutionAndPublication);

        private static Lazy<IEnumerable<string>> _permissionNamesLazy = new Lazy<IEnumerable<string>>(
            GetPermisionNames, LazyThreadSafetyMode.ExecutionAndPublication);
        #endregion

        #region permissionNames

        public const string CanViewApplicantList = "CanViewApplicantList";
        public const string CanEditApplicant = "CanEditApplicant";
        public const string CanCreateApplicant = "CanCreateApplicant";
        public const string CanReferApplicant = "CanReferApplicant";
        public const string CanCancelReferApplicant = "CanCancelReferApplicant";
        public const string CanDeleteApplicant = "CanDeleteApplicant";
        public const string CanApproveApplicant = "CanApproveApplicant";
        public const string CanViewApplicantDetails = "CanViewApplicantDetials";
        public const string CanManageArticleEvaluation = "CanManageArticleEvaluation";
        public const string CanUsePrivateMessage = "CanUsePrivateMessage";
        public const string CanManageEntireEvaluation = "CanManageEntireEvaluation";
        public const string CanManageAddoptedPriority = "CanManageAddoptedPriority";
        public const string CanManageAddress = "CanManageAddress";
        public const string CanManageAppraiser = "CanManageAppraiser";
        public const string CanManageEducationalBackground = "CanManageEducationalBackground";
        public const string CanManageFavoriteIssue = "CanManageFavoriteIssued";
        public const string CanManageInstitution = "CanManageInstitution";
        public const string CanManageInterview = "CanManageInterview";
        public const string CanManageApplicantInServiceCourseType = "CanManageApplicantInServiceCourseType";
        public const string CanManageArticle = "CanManageArticle";
        public const string CanManageOrganizationalTeaching = "CanManageOrganizationalTeaching";
        public const string CanManageQuestion = "CanManageQuestion";
        public const string CanManageResearchExperience = "CanManageResearchExperience";

        public const string CanManageScientificTeaching = "CanManageScientificTeaching";
        public const string CanManageTitle = "CanManageTitle";
        public const string CanManageTrainingCenter = "CanManageTrainingCenter";
        public const string CanManageTrainingCourse = "CanManageTrainingCourse";
        public const string CanManageUser = "CanManageUser";
        public const string CanManageWorkExperience = "CanManageWorkExperience";
        
        public const string CanAccessReports = nameof(CanAccessReports);
        public const string CanAccessToSystemMaintenance = nameof(CanAccessToSystemMaintenance);


        #endregion //permissions

        #region Categories

        public const string CanManageCategory = "CanManageCategory";
        public const string CanCreateCategory = "CanCreateCategory";
        public const string CanEditCategory = "CanEditCategory";
        public const string CanViewCategory = "CanViewCategory";
        public const string CanDeleteCategroy = "CanDeleteCategory";
        #endregion

        #region Permissions
        public static readonly PermissionModel CanViewApplicantDetailsPermission = new PermissionModel { Name = CanViewApplicantDetails, Category = CanViewCategory, Description = "میتوانند  فرم جزئیات اساتید را مشاهده کنند" };
        public static readonly PermissionModel CanViewApplicantListPermission = new PermissionModel { Name = CanViewApplicantList, Category = CanViewCategory, Description = "میتوانند  لیست اساتید را مشاهده کنند" };
        public static readonly PermissionModel CanManageArticleEvaluationPermission = new PermissionModel { Name = CanManageArticleEvaluation, Category = CanManageCategory, Description = "میتوانند ارزیابی های به عمل آمده از مقالات را مدیریت کنند" };
        public static readonly PermissionModel CanUsePrivateMessagePermission = new PermissionModel { Name = CanUsePrivateMessage, Category = CanManageCategory, Description = "میتوانند از سیستم پیغام خصوصی استفاده کنند" };
        public static readonly PermissionModel CanEditApplicantPermission = new PermissionModel { Name = CanEditApplicant, Category = CanEditCategory, Description = "میتوانند اساتید را ویرایش کنند" };
        public static readonly PermissionModel CanCreateApplicantPermission = new PermissionModel { Name = CanCreateApplicant, Category = CanCreateCategory, Description = "میتوانند متقاضی درج کنند" };
        public static readonly PermissionModel CanReferApplicantPermission = new PermissionModel { Name = CanReferApplicant, Category = CanEditCategory, Description = "میتوانند اساتید را ارجاع دهند" };
        public static readonly PermissionModel CanCancelReferApplicantPermission = new PermissionModel { Name = CanCancelReferApplicant, Category = CanEditCategory, Description = "میتوانند متقاضی ارجاع داده شده را لغو ارجاع کنند" };
        public static readonly PermissionModel CanDeleteApplicantPermission = new PermissionModel { Name = CanDeleteApplicant, Category = CanDeleteCategroy, Description = "میتواند اساتید را حذف کنند" };
        public static readonly PermissionModel CanApproveApplicantPermission = new PermissionModel { Name = CanApproveApplicant, Category = CanEditCategory, Description = "میتوانند اساتید درج شده را تأیید کنند" };
        public static readonly PermissionModel CanManageEntireEvaluationPermission = new PermissionModel { Name = CanManageEntireEvaluation, Category = CanManageCategory, Description = "میتواند ارزیابی های به عمل آمده از اساتید را مدیریت کند" };
        public static readonly PermissionModel CanManageAdoptedPriorityPermission = new PermissionModel { Name = CanManageAddoptedPriority, Category = CanManageCategory, Description = "میتواند الویت های تصویب شده اساتید را مدیریت کند" };
        
        public static readonly PermissionModel CanManageAddressPermission = new PermissionModel { Name = CanManageAddress, Category = CanManageCategory, Description = "می توانند آدرس را مدیریت کنند" };
        public static readonly PermissionModel CanManageAppraiserPermission = new PermissionModel { Name = CanManageAppraiser, Category = CanManageCategory, Description = "می توانند ارزیاب را مدیریت کنند" };
        public static readonly PermissionModel CanManageEducationalBackgroundPermission = new PermissionModel { Name = CanManageEducationalBackground, Category = CanManageCategory, Description = "می توانند سوابق تحصیلی را مدیریت کنند" };
        public static readonly PermissionModel CanManageFavoriteIssuePermission = new PermissionModel { Name = CanManageFavoriteIssue, Category = CanManageCategory, Description = "می توانند موضوعات مورد علاقه را مدیریت کنند" };
        public static readonly PermissionModel CanManageInstitutionPermission = new PermissionModel { Name = CanManageInstitution, Category = CanManageCategory, Description = "می توانند موسسه آموزشی را مدیریت کنند" };
        public static readonly PermissionModel CanManageInterviewPermission = new PermissionModel { Name = CanManageInterview, Category = CanManageCategory, Description = "می توانند مصحاحبه ها را مدیریت کنند" };
        public static readonly PermissionModel CanManageApplicantInServiceCourseTypePermission = new PermissionModel { Name = CanManageApplicantInServiceCourseType, Category = CanManageCategory, Description = "می توانند دوره های ضمن خدمت را مدیریت کنند" };
        public static readonly PermissionModel CanManageArticlePermission = new PermissionModel { Name = CanManageArticle, Category = CanManageCategory, Description = "می توانند مقالات را مدیریت کنند" };
        public static readonly PermissionModel CanManageOrganizationalTeachingPermission = new PermissionModel { Name = CanManageOrganizationalTeaching, Category = CanManageCategory, Description = "می توانند تدریس های سازمانی را مدیریت کنند" };
        public static readonly PermissionModel CanManageQuestionPermission = new PermissionModel { Name = CanManageQuestion, Category = CanManageCategory, Description = "می توانند سوالات را مدیریت کنند" };
        public static readonly PermissionModel CanManageResearchExperiencePermission = new PermissionModel { Name = CanManageResearchExperience, Category = CanManageCategory, Description = "می توانند سوابق پژوهشی را مدیریت کنند" };

        public static readonly PermissionModel CanAccessToSystemMaintenancePermission = new PermissionModel { Name = CanAccessToSystemMaintenance, Category = CanManageCategory, Description = "می توانند به بخش نگهداری سیستم دسترسی داشته باشند" };

        public static readonly PermissionModel CanAccessReportsPermission = new PermissionModel
        {
            Name = CanAccessReports,
            Category = CanManageCategory,
            Description = "می توانند به بخش گزارشات دسترسی داشته باشند"
        };
        public static readonly PermissionModel CanManageScientificTeachingPermission = new PermissionModel { Name = CanManageScientificTeaching, Category = CanManageCategory, Description = "می توانند سوابق تدریس در مراکز علمی را مدیریت کنند" };
        public static readonly PermissionModel CanManageTitlePermission = new PermissionModel { Name = CanManageTitle, Category = CanManageCategory, Description = "می توانند عناوین را مدیریت کنند" };
        public static readonly PermissionModel CanManageTrainingCenterPermission = new PermissionModel { Name = CanManageTrainingCenter, Category = CanManageCategory, Description = "می توانند مراکز آموزشی را مدیریت کنند" };
        public static readonly PermissionModel CanManageTrainingCoursePermission = new PermissionModel { Name = CanManageTrainingCourse, Category = CanManageCategory, Description = "می توانند دروه های کارآموزی را مدیریت کنند" };
        public static readonly PermissionModel CanManageUserPermission = new PermissionModel { Name = CanManageUser, Category = CanManageCategory, Description = "می توانند کاربران را مدیریت کنند" };
        public static readonly PermissionModel CanManageWorkExperiencePermission = new PermissionModel { Name = CanManageWorkExperience, Category = CanManageCategory, Description = "می توانند سوابق کاری را مدیریت کنند" };

        #endregion

        #region Properties
        public static IEnumerable<PermissionModel> Permissions => _permissionsLazy.Value;

        public static IEnumerable<string> PermissionNames => _permissionNamesLazy.Value;

        #endregion

        #region GetAllPermisions
        private static IEnumerable<PermissionModel> GetPermision()
        {
            return new List<PermissionModel>
            {
                CanManageAddressPermission,
                CanManageAppraiserPermission,
                CanManageEducationalBackgroundPermission,
                CanManageFavoriteIssuePermission,
                CanManageInstitutionPermission,
                CanManageInterviewPermission,
                CanManageApplicantInServiceCourseTypePermission,
                CanManageArticlePermission,
                CanManageOrganizationalTeachingPermission,
                CanManageQuestionPermission,
                CanManageResearchExperiencePermission,
                CanManageScientificTeachingPermission,
                CanManageTitlePermission,
                CanManageTrainingCenterPermission,
                CanManageTrainingCoursePermission,
                CanManageUserPermission,
                CanManageWorkExperiencePermission,
               
                CanManageAdoptedPriorityPermission,
                CanManageEntireEvaluationPermission,
                CanViewApplicantDetailsPermission ,
                CanViewApplicantListPermission ,
                CanManageArticleEvaluationPermission,
                CanUsePrivateMessagePermission ,
                CanEditApplicantPermission ,
                CanCreateApplicantPermission,
                CanReferApplicantPermission ,
                CanCancelReferApplicantPermission ,
                CanDeleteApplicantPermission ,
                CanApproveApplicantPermission ,
                CanAccessToSystemMaintenancePermission,
               CanAccessReportsPermission
            };
        }

        private static IEnumerable<string> GetPermisionNames()
        {
            return new List<string>()
            {
                CanViewApplicantList,
                CanEditApplicant,
                CanCreateApplicant,
                CanReferApplicant,
                CanCancelReferApplicant,
                CanDeleteApplicant,
                CanApproveApplicant,
                CanViewApplicantDetails,
                CanManageArticleEvaluation,
                CanUsePrivateMessage,
                CanManageEntireEvaluation,
                CanManageAddoptedPriority,
                CanManageAddress,
                CanManageAppraiser,
                CanManageEducationalBackground,
                CanManageFavoriteIssue,
                CanManageInstitution,
                CanManageInterview,
                CanManageApplicantInServiceCourseType,
                CanManageArticle,
                CanManageOrganizationalTeaching,
                CanManageQuestion,
                CanManageResearchExperience,
                CanManageScientificTeaching,
                CanManageTitle,
                CanManageTrainingCenter,
                CanManageTrainingCourse,
                CanManageUser,
                CanManageWorkExperience,
                 CanAccessReports,
                CanAccessToSystemMaintenance,
            };
        }
        #endregion

        #region GetAsSelectedList
        public static IEnumerable<SelectListItem> GetAsSelectListItems()
        {
            return Permissions.Select(a => new SelectListItem { Text = a.Description, Value = a.Name }).ToList();
        }
        #endregion

        public static IEnumerable<string> GetBaseSettingPermissions()
        {
            return new List<string>
            {
                CanManageTrainingCenter,
                CanManageTrainingCourse,
                CanManageTitle,
                CanManageAppraiser,
                CanManageQuestion,
                CanManageInstitution

            };
        }
    }
}
