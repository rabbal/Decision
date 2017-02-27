

using System.ComponentModel;
using Decision.Framework.Domain.Entities;

namespace Decision.ViewModels
{
    public abstract class ListRequestBase
    {
        public SortOrder SortOrder { get; set; }
        public long TotalCount { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = nameof(IEntity.Id);
    }
}
