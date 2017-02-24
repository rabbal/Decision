using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.ViewModel.Home
{
   public class LastActivityViewModel
    {
        /// <summary>
        /// نام کاربری کاربر ثبت کننده تغییرات
        /// </summary>
       public string UserName { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
       public string Description { get; set; }
        /// <summary>
        /// زمان
        /// </summary>
       public DateTime OperateDate { get; set; }
    }
}
