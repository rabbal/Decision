using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.Framework.Domain.Services
{
    public interface ICreateService<in TCreateViewModel>
    {
        void Create(TCreateViewModel model);
        void FillCreateViewModel(TCreateViewModel model);

        Task CreateAsync(TCreateViewModel model);
        Task FillCreateViewMoel(TCreateViewModel model);
    }

}
