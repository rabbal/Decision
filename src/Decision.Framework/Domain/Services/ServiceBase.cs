using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Decision.Framework.Domain.Entities;
using Decision.Framework.Domain.Uow;
using Decision.Framework.GuardToolkit;
using AutoMapper;

namespace Decision.Framework.Domain.Services
{
    public abstract class ServiceBase<TEntity, TKey, TViewModel, TCreateViewModel, TEditViewModel, TListViewModel, TListRequest> : IServiceBase<TKey, TViewModel, TCreateViewModel, TEditViewModel, TListRequest, TListViewModel>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDbSet<TEntity> _set;

        #endregion

        #region Constructor

        protected ServiceBase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Check.ArgumentNotNull(unitOfWork, nameof(unitOfWork));
            Check.ArgumentNotNull(mapper, nameof(mapper));

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _set = _unitOfWork.Set<TEntity>();
        }
        #endregion

        #region Public Methods

        public void Dispose()
        {
        }

        public void Create(TCreateViewModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            _set.Add(entity);
        }

        public void FillCreateViewModel(TCreateViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(TCreateViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task FillCreateViewMoel(TCreateViewModel model)
        {
            throw new NotImplementedException();
        }

        public void Edit(TEditViewModel model)
        {
            throw new NotImplementedException();
        }

        public void FillEditViewModel(TEditViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(TEditViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task FillEditViewMoel(TEditViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<TListViewModel> FetchListAsync(TListRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<TViewModel> FetchByIdAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<TCreateViewModel> FetchForCreateAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<TEditViewModel> FetchForEditAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public TListViewModel FetchList(TListRequest request)
        {
            throw new NotImplementedException();
        }

        public TViewModel FetchById(TKey id)
        {
            throw new NotImplementedException();
        }

        public TCreateViewModel FetchForCreate(TKey id)
        {
            throw new NotImplementedException();
        }

        public TEditViewModel FetchForEdit(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(TKey id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
