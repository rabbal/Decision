using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Decision.ViewModel.Common
{
    public enum PageSize
    {
        [Display(Name = "10")]
        Count10 = 10,
        [Display(Name = "20")]
        Count20 = 20,
        [Display(Name = "30")]
        Count30 = 30,
        [Display(Name = "40")]
        Count40 = 40,
        [Display(Name = "50")]
        Count50 = 50,
        [Display(Name = "همه")]
        All = 1
    }
}
