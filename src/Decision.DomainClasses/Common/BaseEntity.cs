using System;

namespace Decision.DomainClasses.Common
{
    /// <summary>
    /// Represents the base Entity
    /// </summary>
    public abstract class BaseEntity : Entity
    {
        #region Properties
        /// <summary>
        /// gets or sets Identifier of this Entity
        /// </summary>
        public virtual Guid Id { get; set; }
        #endregion
    }
}
