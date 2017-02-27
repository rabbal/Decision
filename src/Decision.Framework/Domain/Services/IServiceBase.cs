using System;

namespace Decision.Framework.Domain.Services
{
    public interface IServiceBase<in TKey, TViewModel, TCreateViewModel, TEditViewModel, in TListRequest, TListViewModel> :
        IDisposable, ICreateService<TCreateViewModel, TKey>,
        IEditService<TEditViewModel, TKey>,
        IFetchService<TListViewModel, TListRequest, TKey, TViewModel, TCreateViewModel, TEditViewModel>,
        ICheckExist<TKey>
        where TKey : IEquatable<TKey>

    {
    }
}