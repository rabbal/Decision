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
using Decision.ViewModel.Question;
using EntityFramework.Extensions;

namespace Decision.ServiceLayer.EFServiecs.Evaluations
{
    /// <summary>
    /// کلاس ارائه دهنده سروسیس های لازم برای اعمال روی سوال
    /// </summary>
    public class QuestionService : IQuestionService
    {
        #region Fields

        private readonly IAnswerOptionService _answerOptionService;
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<Question> _questions;
        #endregion

        #region Ctor

        public QuestionService(IUnitOfWork unitOfWork, IAnswerOptionService answerOptionService, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _questions = _unitOfWork.Set<Question>();
            _mappingEngine = mappingEngine;
            _answerOptionService = answerOptionService;
        }
        #endregion

        #region GetForEdit
        public async Task<EditQuestionViewModel> GetForEditAsync(Guid id)
        {
            var viewModel = await _questions.AsNoTracking().ProjectTo<EditQuestionViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
            return viewModel;

        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _questions.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditQuestionViewModel viewModel)
        {
            var question = await _questions.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, question);
        }
        #endregion

        #region Create
        public async Task<QuestionViewModel> CreateAsync(AddQuestionViewModel viewModel)
        {
            var question = _mappingEngine.Map<Question>(viewModel);

            foreach (var option in viewModel.Options)
            {
                question.AnswerOptions.Add(_mappingEngine.Map<AnswerOption>(option));
            }
            _questions.Add(question);
            await _unitOfWork.SaveChangesAsync();
            return await GetQuestionViewModel(question.Id);
        }
        #endregion

        #region GetPagedList
        public async Task<QuestionListViewModel> GetPagedListAsync(QuestionSearchRequest request)
        {
            var questions = _questions.AsNoTracking().Include(a => a.CreatedBy)
                .Include(a => a.ModifiedBy).OrderByDescending(a => a.CreatedOn).AsQueryable();


            var query = await questions.ProjectTo<QuestionViewModel>(_mappingEngine)
                .Skip((request.PageIndex - 1) * 10)
                .Take(10).ToListAsync();

            return new QuestionListViewModel { SearchRequest = request, Questions = query };
        }

        public Task<bool> IsInDb(Guid id)
        {
            return _questions.AnyAsync(a => a.Id == id);
        }

        #endregion

        public Task<QuestionViewModel> GetQuestionViewModel(Guid id)
        {
            return _questions.AsNoTracking()
                 .Include(a => a.CreatedBy)
                 .Include(a => a.ModifiedBy).ProjectTo<QuestionViewModel>(_mappingEngine)
                 .FirstOrDefaultAsync(a => a.Id == id);
        }


        public Task DisableAsync(Guid guid)
        {
            return _questions.Where(a => a.Id == guid).UpdateAsync(a => new Question { IsDeleted = true });
        }

        public Task EnableAsync(Guid guid)
        {
            return _questions.Where(a => a.Id == guid).UpdateAsync(a => new Question { IsDeleted = false });
        }

        public async Task<IEnumerable<Question>> GetQuestionsByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _questions.Where(a => ids.Contains(a.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Question>> GelAllActive()
        {
            return await _questions.AsNoTracking().Where(a => !a.IsDeleted).Include(a => a.AnswerOptions).ToListAsync();
        }
    }
}