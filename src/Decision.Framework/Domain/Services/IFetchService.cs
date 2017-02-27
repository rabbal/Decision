using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.Framework.Domain.Services
{
    public interface IFetchService<TListViewModel, in TListReqeust, TViewModel, TCreateViewModel, TEditViewModel>
        {
        Task<TListViewModel> FetchListAsync(TListReqeust request);
        Task<TViewModel> FetchByIdAsync(long id);
        Task<TCreateViewModel> FetchForCreateAsync(long id);
        Task<TEditViewModel> FetchForEditAsync(long id);
        Task<bool> ExistsAsync(long id);

        TListViewModel FetchList(TListReqeust request);
        TViewModel FetchById(long id);
        TCreateViewModel FetchForCreate(long id);
        TEditViewModel FetchForEdit(long id);
        bool Exists(long id);

    }
}
