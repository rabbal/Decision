using System;

namespace Decision.DomainClasses.Common
{
    public interface IHasGuidKey
    {
        #region Properties
        Guid Id { get; set; }

        #endregion
    }
}
