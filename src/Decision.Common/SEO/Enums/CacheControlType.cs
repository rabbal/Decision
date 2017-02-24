using System.ComponentModel;

namespace Decision.Common.SEO.Enums
{
    public enum CacheControlType
    {
        [Description("public")]
        Public,
        [Description("private")]
        Private,
        [Description("no-cache")]
        Nocache,
        [Description("no-store")]
        Nostore
    }
}