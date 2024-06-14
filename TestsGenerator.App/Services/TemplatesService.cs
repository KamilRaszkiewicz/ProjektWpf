using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestsGenerator.App.Interfaces;
using TestsGenerator.Domain.Models.Questions;
using TestsGenerator.Domain.Models.Tests;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                .ThenInclude(x => x.Answers)
                .Include(x => x.Tests)
                .ToList();
        }

        public List<Test> GetTestTemplatesTests(TestTemplate template)
        {
            return _testsRepository
                .GetQueryable()
                .Include(x => x.QuestionsOrdinals)
                .ThenInclude(x => x.Question)
                .Include(x => x.QuestionsAnswersOrdinals)
                .ThenInclude(x => x.Answer)
                .Where(x => x.TestTemplate == template)
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

        public async Task GenerateTestsAsync(TestTemplate template, int testsToGenerate)
        {
            var numberOfTests = template.Tests.Count;
            var tests = new List<Test>();

            foreach(var versionIdentifier in Enumerable.Range(numberOfTests + 1, testsToGenerate).Select(x => IntToVersionIdentifier(x)))
            {
                tests.Add(new Test
                {
                    VersionIdentifier = versionIdentifier
                });
            }

            _templatesRepository.Attach(template);

            template.Tests.AddRange(tests);

            await _templatesRepository.UpdateAsync(template, CancellationToken.None);

            foreach(var test in tests)
            {
                test.QuestionsOrdinals = template.QuestionPool
                    .OrderBy(x => Random.Shared.Next())
                    .Select((x, i) => new TestQuestionOrdinal
                    {
                        QuestionsId = x.Id,
                        Ordinal = i,
                    }).ToList();

                test.QuestionsAnswersOrdinals = template.QuestionPool
                    .SelectMany(x => x.Answers.OrderBy(x => Random.Shared.Next()).Select((y, i) => new TestQuestionAnswerOrdinal
                    {
                        TestsId = test.Id,
                        QuestionsId = x.Id,
                        AnswersId = y.Id,
                        Ordinal = i
                    })).ToList();
            }

            await _testsRepository.UpdateAsync(tests, CancellationToken.None);
        }

        private string IntToVersionIdentifier(int value)
        {
            var result = string.Empty;

            while (value > 0)
            {
                value--;
                int remainder = value % 26;
                char letter = (char)(remainder + 'A');
                result = letter + result;
                value /= 26;
            }

            return result;
        }

        private int VersionIdentifierToInt(string base26)
        {
            int result = 0;
            int power = 1;

            for (int i = base26.Length - 1; i >= 0; i--)
            {
                char letter = base26[i];

                int value = letter - 'A' + 1;

                result += value * power;

                power *= 26;
            }

            return result;
        }
    }
}
