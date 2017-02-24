using System;

namespace Decision.DomainClasses.Common
{
    /// <summary>
    /// Represents the base Entity
    /// </summary>
    public abstract class BaseEntity : Entity
    {
        #region Properties

        public virtual Guid Id { get; set; }
        #endregion
    }
}
