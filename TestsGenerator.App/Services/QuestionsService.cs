using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGenerator.App.Interfaces;
using TestsGenerator.Domain.Models.Questions;

namespace TestsGenerator.App.Services
{
    public class QuestionsService
    {
        private readonly IRepository<Question> _questionsRepository;
        private readonly IRepository<Category> _categoriesRepository;

        public QuestionsService(
            IRepository<Question> questionsRepository,
            IRepository<Category> categoriesRepostiory)
        {
            _questionsRepository = questionsRepository;
            _categoriesRepository = categoriesRepostiory;
        }

        public List<Question> GetAllQuestions()
        {
            return _questionsRepository
                .GetQueryable()
                .Include(x => x.Category)
                .Include(x => x.QuestionAnswers)
                .ThenInclude(x => x.Answer)
                .ToList();
        }

        public async Task SaveQuestion(Question question)
        {
            if(question.Id == default)  //nowe pytanie
            {
                await _questionsRepository.InsertAsync(question, CancellationToken.None);
            }
            else
            {
                await _questionsRepository.UpdateAsync(question, CancellationToken.None);
            }    
        }

        public List<Category> GetCategories()
        {
            return _categoriesRepository
                .GetQueryable()
                .ToList();
        }
    }
}
