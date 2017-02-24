using System;
using System.Xml.Linq;
using Decision.DomainClasses.Users;
namespace Decision.DomainClasses.Common
{
    public class AuditLog
    {
        #region Constructors
        public AuditLog()
        {
            OperatedOn = DateTime.Now;
        }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public AuditAction Action { get; set; }
        public string Description { get; set; }
        public DateTime OperatedOn { get; set; }
        public string Entity { get; set; }
        public string XmlOldValue { get; set; }
        public XElement XmlOldValueWrapper
        {
            get { return XElement.Parse(XmlOldValue); }
            set { XmlOldValue = value.ToString(); }
        }
        public string XmlNewValue { get; set; }
        public XElement XmlNewValueWrapper
        {
            get { return XElement.Parse(XmlNewValue); }
            set { XmlNewValue = value.ToString(); }
        }
        public string EntityId { get; set; }
        public string Agent { get; set; }
        public string OperantIp { get; set; }
        #endregion

        #region NavigationProperties
        public User Operant { get; set; }
        public Guid OperantId { get; set; }
        #endregion
    }
}
