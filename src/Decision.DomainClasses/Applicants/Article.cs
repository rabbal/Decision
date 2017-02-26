using System;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities.Tracking;

namespace Decision.DomainClasses.Applicants
{
    public class Article : TrackableEntity<long,User>
    {
        #region Properties

        public decimal Score { get; set; }

        public string Title { get; set; }

        public string MagazineOrSeminarName { get; set; }

        public MagazineOrSeminarType MagazineOrSeminarType { get; set; }

        public ArticleResponsibilityType ResponsibilityType { get; set; }

        public ArticleType Type { get; set; }
        
        public string Brief { get; set; }

        public DateTime? PublicationDateTime { get; set; }

        public string AttachmentFileName { get; set; }

        #endregion

        #region Navigation Properties

        public long ApplicantId { get; set; }

        public Applicant Applicant { get; set; }

        #endregion
    }
}