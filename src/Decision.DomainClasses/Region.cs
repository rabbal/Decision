using System.Collections.Generic;
using Decision.Framework.Domain.Entities;

namespace Decision.DomainClasses
{
    public class Region : Entity
    {
        #region Properties
        public string Title { get; set; }
        public RegionType Type { get; set; }
        #endregion

        #region Navigation Properties

        public Region Parent { get; set; }
        public long? ParentId { get; set; }
        public ICollection<Region> Children { get; set; }
        #endregion
    }
}
