using System;
using System.Xml.Linq;
using Decision.DomainClasses.Entities.Users;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.Common
{
    /// <summary>
    /// Represent The Operation's log
    /// </summary>
    public class AuditLog
    {
        #region Ctor
        /// <summary>
        /// Create One Instance Of <see cref="AuditLog"/>
        /// </summary>
        public AuditLog()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
            OperatedOn = DateTime.Now;
        }
        #endregion

        #region Properties
        /// <summary>
        /// sets or gets identifier of AuditLog
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// gets or sets Type of  Modification(create,softDelet,Delete,update)
        /// </summary>
        public AuditAction Action { get; set; }
        /// <summary>
        /// sets or gets description of Log
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// sets or gets when log is operated
        /// </summary>
        public DateTime OperatedOn { get; set; }
        /// <summary>
        /// sets or gets Type Of Entity 
        /// </summary>
        public string Entity { get; set; }
        /// <summary>
        /// gets or sets  Old value of  Properties before modification
        /// </summary>
        public string XmlOldValue { get; set; }
        /// <summary>
        /// gets or sets XML Base OldValue of Properties (NotMapped)
        /// </summary>
        public XElement XmlOldValueWrapper
        {
            get { return XElement.Parse(XmlOldValue); }
            set { XmlOldValue = value.ToString(); }
        }
        /// <summary>
        /// gets or sets new value of  Properties after modification
        /// </summary>
        public string XmlNewValue { get; set; }
        /// <summary>
        /// gets or sets XML Base NewValue of Properties (NotMapped)
        /// </summary>
        public XElement XmlNewValueWrapper
        {
            get { return XElement.Parse(XmlNewValue); }
            set { XmlNewValue = value.ToString(); }
        }
        /// <summary>
        /// gets or sets Identifier Of Entity
        /// </summary>
        public string EntityId { get; set; }
        /// <summary>
        /// gets or sets user agent information
        /// </summary>
        public string Agent { get; set; }
        /// <summary>
        /// gets or sets user's ip address
        /// </summary>
        public string OperantIp { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// sets or gets log's creator
        /// </summary>
        public User Operant { get; set; }
        /// <summary>
        /// sets or gets identifier of log's creator
        /// </summary>
        public Guid OperantId { get; set; }
        #endregion
    }
}
