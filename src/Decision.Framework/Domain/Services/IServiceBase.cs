using System;
using Decision.Framework.Domain.Entities;

namespace Decision.Framework.Domain.Services
{
    public interface IServiceBase<TViewModel, TCreateViewModel, TEditViewModel, in TListRequest, TListViewModel> :
        IDisposable, ICreateService<TCreateViewModel>,
        IEditService<TEditViewModel>,
        IFetchService<TListViewModel, TListRequest, TViewModel, TCreateViewModel, TEditViewModel>,
        IDeleteService
        where TEditViewModel : IEntity
    {
    }
}