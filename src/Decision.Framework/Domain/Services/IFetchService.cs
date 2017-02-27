using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.Framework.Domain.Services
{
    public interface IFetchService<TListViewModel, in TListReqeust, in TKey, TViewModel, TCreateViewModel, TEditViewModel>
        where TKey : IEquatable<TKey>
    {
        Task<TListViewModel> FetchListAsync(TListReqeust request);
        Task<TViewModel> FetchByIdAsync(TKey id);
        Task<TCreateViewModel> FetchForCreateAsync(TKey id);
        Task<TEditViewModel> FetchForEditAsync(TKey id);

        TListViewModel FetchList(TListReqeust request);
        TViewModel FetchById(TKey id);
        TCreateViewModel FetchForCreate(TKey id);
        TEditViewModel FetchForEdit(TKey id);
    }
}
