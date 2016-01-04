using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Users;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.PrivateMessage
{
    /// <summary>
    /// نشان دهنده فایل ضمیمه مربوط به پیغام خصوصی
    /// </summary>
    public class MessageAttachment
    {

        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        public MessageAttachment()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }
        #endregion

        #region Properties
        /// <summary>
        /// آی دی موجودیت 
        /// </summary>
        public  Guid Id { get; set; }
        /// <summary>
        /// نام نمایشی زیبا
        /// </summary>
        public  string FriendlyName { get; set; }
        /// <summary>
        /// محتوای ضمیمه
        /// </summary>
        public  byte[] Data { get; set; }
        /// <summary>
        /// Mime Type Of Attachment
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// فرمت فایل
        /// </summary>
        public string Extension { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// آی دی پیغام مربوط به این فایل
        /// </summary>
        public  Message Message { get; set; }
        /// <summary>
        ///  پیغام مربوط به این فایل
        /// </summary>
        public  Guid MessageId { get; set; }

        #endregion
    }
}
