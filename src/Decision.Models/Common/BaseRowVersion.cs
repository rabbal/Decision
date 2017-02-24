using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Decision.ViewModel.Common
{
    public abstract class BaseRowVersion
    {
        public byte[] RowVersion { get; set; }
    }
}
