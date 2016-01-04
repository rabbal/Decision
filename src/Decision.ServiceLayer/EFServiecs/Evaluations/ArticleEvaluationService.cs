using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.Common.HtmlCleaner;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.ServiceLayer.Contracts.Evaluations;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.ArticleEvaluation;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.Evaluations
{
    /// <summary>
    /// کلاس ارائه دهنده سروسیس های لازم برای اعمال روی ارزیابی از مقاله استاد
    /// </summary>
    public class ArticleEvaluationService : IArticleEvaluationService
    {
        #region Fields

        private readonly IAnswerOptionService _answerOptionService;
        private readonly IQuestionService _questionService;
        private readonly IDbSet<ArticleEvaluationQuestion> _evaluationQuestions;
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<ArticleEvaluation> _ArticleEvaluations;
        #endregion

        #region Ctor

        public ArticleEvaluationService(IAnswerOptionService answerOptionService,
            IQuestionService questionService, IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _answerOptionService = answerOptionService;
            _questionService = questionService;
            _ArticleEvaluations = _unitOfWork.Set<ArticleEvaluation>();
            _mappingEngine = mappingEngine;
            _evaluationQuestions = _unitOfWork.Set<ArticleEvaluationQuestion>();
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _ArticleEvaluations.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region Create
        public async Task Create(AddArticleEvaluationBindingModel bindingModel)
        {
            var currentUserId = _userManager.GetCurrentUserId();

            var questionIds = new List<Guid>();
            if (bindingModel.CheckBoxListQuestions != null && bindingModel.CheckBoxListQuestions.Any())
                questionIds.AddRange(bindingModel.CheckBoxListQuestions.Select(a => a.Id));
            if (bindingModel.NumericQuestions != null && bindingModel.NumericQuestions.Any())
                questionIds.AddRange(bindingModel.NumericQuestions.Select(a => a.Id));
            if (bindingModel.StringQuestions != null && bindingModel.StringQuestions.Any())
                questionIds.AddRange(bindingModel.StringQuestions.Select(a => a.Id));
            if (bindingModel.RadioButtonListQuestions != null && bindingModel.RadioButtonListQuestions.Any())
                questionIds.AddRange(bindingModel.RadioButtonListQuestions.Select(a => a.Id));



            var answerOptionIds = new List<Guid>();

            if (bindingModel.CheckBoxListQuestions != null && bindingModel.CheckBoxListQuestions.Any())
                answerOptionIds.AddRange(bindingModel.CheckBoxListQuestions.Where(a => a.OptionIds != null && a.OptionIds.Any())
                    .SelectMany(a => a.OptionIds));

            if (bindingModel.RadioButtonListQuestions != null && bindingModel.RadioButtonListQuestions.Any())
                answerOptionIds.AddRange(bindingModel.RadioButtonListQuestions.Where(a => a.OptionId != null)
                    .Select(a => a.OptionId.Value));

            var actualQuestions = await _questionService.GetQuestionsByIdsAsync(questionIds);
            var actualAnswerOptions = await _answerOptionService.GetAnswerOptionsByIds(answerOptionIds);
            var newEvalution = new ArticleEvaluation
            {
                Brief = bindingModel.Brief.ToSafeHtml(),
                Content = bindingModel.Content.ToSafeHtml(),
                StrongPoint = bindingModel.StrongPoint.ToSafeHtml(),
                CreatorId = currentUserId,
                EvaluatorId = bindingModel.EvaluatorId,
                ArticleId = bindingModel.ArticleId,
                EvaluationDate = DateTime.Now

            };

            foreach (var item in actualQuestions)
            {
                newEvalution.Questions.Add(item);
            }


            if (bindingModel.NumericQuestions != null && bindingModel.NumericQuestions.Any())
            {
                foreach (var numericQuestion in bindingModel.NumericQuestions)
                {
                    newEvalution.ArticleEvaluationQuestions.Add(new ArticleEvaluationQuestion
                    {
                        ArticleEvaluationId = newEvalution.Id,
                        QuestionId = numericQuestion.Id,
                        CreatorId = currentUserId,
                        Value = numericQuestion.Value.ToString()
                    });
                }
            }

            if (bindingModel.StringQuestions != null && bindingModel.StringQuestions.Any())
            {
                foreach (var stringQuestion in bindingModel.StringQuestions)
                {
                    newEvalution.ArticleEvaluationQuestions.Add(new ArticleEvaluationQuestion
                    {
                        ArticleEvaluationId = newEvalution.Id,
                        QuestionId = stringQuestion.Id,
                        CreatorId = currentUserId,
                        Value = stringQuestion.Value.ToSafeHtml()
                    });
                }
            }

            var answerOptions = actualAnswerOptions as IList<AnswerOption> ?? actualAnswerOptions.ToList();
            if (actualAnswerOptions != null && answerOptions.Any())
            {
                foreach (var answerOption in answerOptions)
                {
                    newEvalution.AnswerOptions.Add(answerOption);
                }
            }
            _ArticleEvaluations.Add(newEvalution);
            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<ArticleEvaluationListViewModel> GetPagedList(ArticleEvaluationSearchRequest request)
        {
            var evaluations = _ArticleEvaluations.AsNoTracking().Include(a => a.Creator)
                .Include(a => a.Evaluator).Where(a=>a.ArticleId==request.ArticleId);

            var result =
                evaluations.OrderByDescending(a => a.EvaluationDate)
                    .ThenByDescending(a => a.CreateDate)
                    .Skip((request.PageIndex - 1)*10)
                    .Take(10)
                    .ProjectTo<ArticleEvaluationViewModel>(_mappingEngine);
            return new ArticleEvaluationListViewModel
            {
                Request = request,
                ArticleEvaluations = await result.ToListAsync()
            };
        }

        public long Count()
        {
            return _evaluationQuestions.LongCount();
        }
        #endregion
    }
}