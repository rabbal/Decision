namespace Decision.DomainClasses.Common
{
    public class Setting : AuditableEntity
    {
        #region Properties
        public string Name { get; set; }
        public string Value { get; set; }

        #endregion
    }
}