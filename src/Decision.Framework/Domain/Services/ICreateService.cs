using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.Framework.Domain.Services
{
    public interface ICheckExist<in TKey>
        where TKey:IEquatable<TKey>
    {
        Task<bool> ExistsAsync(TKey id);
        bool Exists(TKey id);
    }
    public interface ICreateService<in TCreateViewModel, in TKey>
        where TKey : IEquatable<TKey>
    {
        void Create(TCreateViewModel model);
        void FillCreateViewModel(TCreateViewModel model);

        Task CreateAsync(TCreateViewModel model);
        Task FillCreateViewMoel(TCreateViewModel model);
    }

}
