using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.Framework.Domain.Services
{
    public interface IEditService<in TEditViewModel>
    {
        void Edit(TEditViewModel model);
        void FillEditViewModel(TEditViewModel model);

        Task EditAsync(TEditViewModel model);
        Task FillEditViewMoel(TEditViewModel model);
    }
}
