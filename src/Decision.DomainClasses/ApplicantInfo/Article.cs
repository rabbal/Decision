﻿using System;
using Decision.DomainClasses.Common;

namespace Decision.DomainClasses.ApplicantInfo
{

    public class Article : BaseEntity
    {
        #region Ctor
   
        public Article()
        {
            Attachment = BitConverter.GetBytes(0);
        }
        #endregion

        #region Properties

        public double Score { get; set; }
     
        public string Title { get; set; }

        public string MagazineOrSeminarName { get; set; }

        public MagazineOrSeminarType MagazineOrSeminarType { get; set; }

        public ArticleResponsibilityType ResponsibilityType { get; set; }
    
        public ArticleType Type { get; set; }

        public string ArticleType { get; set; }

        public string Brief { get; set; }

        public DateTime? PublicatedOn { get; set; }

        public byte[] Attachment { get; set; }
        #endregion

        #region NavigationProperties

        public Guid ApplicantId { get; set; }

        public Applicant Applicant { get; set; }
        #endregion
    }
}