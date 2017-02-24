namespace Decision.Common.EntityFrameworkToolkit
{
    public class SqlServerInfo
    {
        public string ProductVersion { get; set; }
        public string PatchLevel { get; set; }
        public string ProductEdition { get; set; }
        public string ClrVersion { get; set; }
        public string DefaultCollation { get; set; }
        public string Instance { get; set; }
        public int Lcid { get; set; }
        public string ServerName { get; set; }
    }
}