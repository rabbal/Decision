using System;
using Decision.DomainClasses.Users;

namespace Decision.DomainClasses.Common
{

    
    public abstract class Entity
    {
        #region Properties
        public virtual DateTime CreatedOn { get; set; }

        public virtual DateTime ModifiedOn { get; set; }

        public virtual string CreatorIp { get; set; }

        public virtual string ModifierIp { get; set; }
   
        public virtual bool ModifyLocked { get; set; }

        public virtual bool IsDeleted { get; set; }
       
        public virtual string ModifierAgent { get; set; }
        
        public virtual string CreatorAgent { get; set; }
        
        public virtual int Version { get; set; }
        
        public virtual AuditAction Action { get; set; }
        
        public virtual byte[] RowVersion { get; set; }
        #endregion

        #region NavigationProperties
        
        public virtual User ModifiedBy { get; set; }
        
        public virtual Guid ModifiedById { get; set; }
        
        public virtual User CreatedBy { get; set; }
        
        public virtual Guid CreatedById { get; set; }
        #endregion
    }
}
