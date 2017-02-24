namespace Decision.DomainClasses.Common
{
    public class Setting
    {
        #region Properties

        public string Name { get; set; }

        public string Value { get; set; }


        public byte[] RowVersion { get; set; }

        #endregion
    }
}