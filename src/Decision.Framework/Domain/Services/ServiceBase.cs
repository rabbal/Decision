using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Decision.Framework.Domain.Entities;
using Decision.Framework.Domain.Uow;
using Decision.Framework.GuardToolkit;
using AutoMapper;

namespace Decision.Framework.Domain.Services
{
    public abstract class ServiceBase<TEntity, TViewModel, TCreateViewModel, TEditViewModel, TListViewModel,
        TListRequest> : IServiceBase<TViewModel, TCreateViewModel, TEditViewModel, TListRequest, TListViewModel>
        where TEntity : class, IEntity, new()
        where TEditViewModel : IEntity
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDbSet<TEntity> _entities;

        #endregion

        #region Constructor

        protected ServiceBase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Check.ArgumentNotNull(unitOfWork, nameof(unitOfWork));
            Check.ArgumentNotNull(mapper, nameof(mapper));

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _entities = _unitOfWork.Set<TEntity>();
        }

        #endregion

        #region Public Methods

        public virtual void Dispose()
        {
        }

        public virtual void Create(TCreateViewModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            _entities.Add(entity);

            _unitOfWork.SaveChanges();
        }

        public virtual void FillCreateViewModel(TCreateViewModel model)
        {
            throw new NotImplementedException();
        }

        public virtual Task CreateAsync(TCreateViewModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            _entities.Add(entity);

            return _unitOfWork.SaveChangesAsync();
        }

        public virtual Task FillCreateViewMoel(TCreateViewModel model)
        {
            throw new NotImplementedException();
        }

        public virtual void Edit(TEditViewModel model)
        {
            var entity = _entities.Find(model.Id);
            if (entity == null)
                throw new EntityNotFoundException($"Couldn't Find Entity {model.Id} For Edit");

            _mapper.Map(model, entity);
            _unitOfWork.SaveChanges();
        }

        public virtual void FillEditViewModel(TEditViewModel model)
        {
            throw new NotImplementedException();
        }

        public virtual async Task EditAsync(TEditViewModel model)
        {
            var entity = await _entities.FirstOrDefaultAsync(a => a.Id == model.Id);
            if (entity == null)
                throw new EntityNotFoundException($"Couldn't Find Entity {model.Id} For Edit");

            _mapper.Map(model, entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public virtual Task FillEditViewMoel(TEditViewModel model)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TListViewModel> FetchListAsync(TListRequest request)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TViewModel> FetchByIdAsync(long id)
        {
            var entity = await _entities.Where(a => a.Id == id)
                .ProjectToFirstOrDefaultAsync<TViewModel>(_mapper.ConfigurationProvider);

            if (entity == null)
                throw new EntityNotFoundException($"Couldn't Find Entity {id} When FetchByIdAsync");
            return entity;
        }

        public virtual Task<TCreateViewModel> FetchForCreateAsync(long id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TEditViewModel> FetchForEditAsync(long id)
        {
            var entity = await _entities.Where(a => a.Id == id)
                .ProjectToFirstOrDefaultAsync<TEditViewModel>(_mapper.ConfigurationProvider);

            if (entity == null)
                throw new EntityNotFoundException($"Couldn't Find Entity {id} When FetchForEditAsync");

            return entity;
        }

        public virtual TListViewModel FetchList(TListRequest request)
        {
            throw new NotImplementedException();
        }

        public TViewModel FetchById(long id)
        {
            var entity =
                _entities.Where(a => a.Id == id).ProjectToFirstOrDefault<TViewModel>(_mapper.ConfigurationProvider);

            if (entity == null)
                throw new EntityNotFoundException($"Couldn't Find Entity {id} When FetchById");

            return entity;
        }

        public virtual TCreateViewModel FetchForCreate(long id)
        {
            throw new NotImplementedException();
        }

        public virtual TEditViewModel FetchForEdit(long id)
        {
            var entity =
                _entities.Where(a => a.Id == id).ProjectToFirstOrDefault<TEditViewModel>(_mapper.ConfigurationProvider);

            if (entity == null)
                throw new EntityNotFoundException($"Couldn't Find Entity {id} When FetchForEdit");

            return entity;
        }

        public Task<bool> ExistsAsync(long id)
        {
            return _entities.AnyAsync(a => a.Id == id);
        }

        public bool Exists(long id)
        {
            return _entities.Any(a => a.Id == id);
        }


        public void Delete(Entity model)
        {
            var entity = _mapper.Map<TEntity>(model);

            _entities.Remove(entity);
            _unitOfWork.SaveChanges();
        }

        public Task DeleteAsync(Entity model)
        {
            var entity = _mapper.Map<TEntity>(model);

            _entities.Remove(entity);
            return _unitOfWork.SaveChangesAsync();
        }

        #endregion
    }
}
