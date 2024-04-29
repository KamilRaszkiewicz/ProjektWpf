using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGenerator.App;
using TestsGenerator.App.Interfaces;
using TestsGenerator.Domain.Models.Questions;
using TestsGenerator.Domain.Models.Tests;

namespace TestsGenerator.Infrastructure.Services
{
    public class TestsTemplatesService : ITestsTemplatesService
    {
        private readonly IRepository<TestTemplate> _testsTemplatesRepository;
        private readonly ILogger<TestsTemplatesService> _logger;

        public TestsTemplatesService(
            IRepository<TestTemplate> testsRepository, //Wstrzykujemy repozytorium od szablonów testów (pobieranie i zapisywanie template'ów)
            ILogger<TestsTemplatesService> logger
        )
        {
            _testsTemplatesRepository = testsRepository;
            _logger = logger;
        }

        public async Task<long?> CreateTestsTemplateAsync(string templatesName, List<Question> questionsPool)
        {
            try
            {
                var testTemplateExists = _testsTemplatesRepository.GetQueryable().Any(x => x.Name.ToLower() == templatesName.ToLower()); //sprawdzamy czy jest template o tej nazwie

                if(testTemplateExists)
                {
                    throw new Exception("Istnieje template o tej nazwie");
                }

                var template = new TestTemplate
                {
                    Name = templatesName,
                    CreatedAt = DateTime.Now,
                    QuestionPool = questionsPool    //od razu mozemy przypisac pytania bo czemu nie
                };

                await _testsTemplatesRepository.InsertAsync(template, CancellationToken.None);

                return template.Id; //wszystko git, zwracamy id
            }
            catch (Exception e)
            {
                _logger.LogException(nameof(TestsTemplatesService), nameof(CreateTestsTemplateAsync), e);
            }

            return null;
        }

        public TestTemplate? GetTestTemplate(long testsTemplatesId)
        {
            return _testsTemplatesRepository
                .GetQueryable()
                .Include(x => x.Tests) //Dociągamy testy wygenerowane z template'a
                .Include(x => x.QuestionPool) // dociągamy pulę pytań
                .ThenInclude(x => x.Answers) // dociagamy tez odpowiedzi w tych pytaniach
                .FirstOrDefault(x => x.Id == testsTemplatesId); // jeśli znajdzie rekord to go zwracamy
        }
    }
}
