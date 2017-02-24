using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Decision.ViewModel.Common
{
    public abstract class BaseIsDelete
    {
        /// <summary>
        /// آیا رکورد به صورت منطقی حذف شده است
        /// </summary>
        [DisplayName("حذف منطقی")]
        public bool IsDeleted { get; set; }
    }
}
