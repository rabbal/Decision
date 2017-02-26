

using System.ComponentModel;

namespace Decision.ViewModels.Common
{
    public class ListRequestBase
    {
        public ListRequestBase()
        {
            PageSize = 10;
            PageIndex = 1;
            SortDirection = SortDirectionMode.Desc;
        }

        [DisplayName("جستجو")]
        public string Term { get; set; }
        public string SortDirection { get; set; }
        public int Total { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string CurrentSort { get; set; }
    }
}
