using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.Evaluations;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.AnswerOption;
using EntityFramework.Extensions;

namespace Decision.ServiceLayer.EFServiecs.Evaluations
{
    /// <summary>
    /// کلاس ارائه دهنده سروسیس های لازم برای اعمال روی گرینه های سوال های چند گزینه ای
    /// </summary>
    public class AnswerOptionService : IAnswerOptionService
    {
        #region Fields
        
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<AnswerOption> _answerOptions;
        #endregion

        #region Ctor

        public AnswerOptionService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _answerOptions = _unitOfWork.Set<AnswerOption>();
            
            _mappingEngine = mappingEngine;
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _answerOptions.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditAnswerOptionViewModel viewModel)
        {
            var answerOption = await _answerOptions.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, answerOption);
        }
        #endregion

        #region Create
        public async Task<AnswerOptionViewModel> Create(AddAnswerOptionViewModel viewModel)
        {
            var answerOption = _mappingEngine.Map<AnswerOption>(viewModel);
            _answerOptions.Add(answerOption);
            await _unitOfWork.SaveChangesAsync();
            return await GetAnswerOptionViewModel(answerOption.Id);
        }

        public Task<bool> IsInDb(Guid id)
        {
            return _answerOptions.AnyAsync(a => a.Id == id);
        }

        #endregion

        public Task<AnswerOptionViewModel> GetAnswerOptionViewModel(Guid id)
        {
            return _answerOptions.AsNoTracking()
                 .ProjectTo<AnswerOptionViewModel>(_mappingEngine)
                 .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<AnswerOption>> GetAnswerOptionsByIds(IEnumerable<Guid> ids)
        {
            return await _answerOptions.Where(a => ids.Contains(a.Id)).ToListAsync();
        }
    }
}