using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGenerator.App.Interfaces;
using TestsGenerator.Domain.Models.Questions;
using TestsGenerator.Domain.Models.Tests;

namespace TestsGenerator.App.Services
{
    public class TemplatesService
    {
        private readonly IRepository<TestTemplate> _templatesRepository;
        private readonly IRepository<Test> _testsRepository;


        public TemplatesService(
        IRepository<TestTemplate> templatesRepository,
        IRepository<Test> testsRepository)
        {
            _templatesRepository = templatesRepository;
            _testsRepository = testsRepository;
        }

        public List<TestTemplate> GetAllTemplates()
        {
            return _templatesRepository
                .GetQueryable()
                .Include(x => x.QuestionPool)
                .Include(x => x.Tests)
                .ToList();
        }

        public List<Test> GetAllTests()
        {
            return _testsRepository
                .GetQueryable()
                .Include(x => x.VersionIdentifier)
                .Include(x => x.QuestionsOrdinals)
                .ToList();
        }

        public async Task SaveTemplate(TestTemplate template)
        {
            if (template.Id == default)  //nowy szablon
            {
                await _templatesRepository.InsertAsync(template, CancellationToken.None);
            }
            else
            {
                await _templatesRepository.UpdateAsync(template, CancellationToken.None);
            }
        }

        public async Task SaveTest(Test test)
        {
            if (test.Id == default)  //nowy szablon
            {
                await _testsRepository.InsertAsync(test, CancellationToken.None);
            }
            else
            {
                await _testsRepository.UpdateAsync(test, CancellationToken.None);
            }
        }
    }
}
