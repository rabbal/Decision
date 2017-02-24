using Decision.Common.Domain;
using Decision.Common.Domain.Tracking;
using Decision.DomainClasses.Identity;

namespace Decision.DomainClasses
{
    public class Setting : TrackableEntity, ISystemDefaultEntry
    {
        #region Properties

        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsSystemEntry { get; set; }

        #endregion

        #region Navigation Properties

        public User User { get; set; }
        public long? UserId { get; set; }

        #endregion
    }
}