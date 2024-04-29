using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGenerator.Domain.Models.Questions;
using TestsGenerator.Domain.Models.Tests;

namespace TestsGenerator.App.Interfaces
{
    public interface ITestsTemplatesService
    {
        public TestTemplate? GetTestTemplate(long testsId);
        public Task<long?> CreateTestsTemplateAsync(string templatesName, List<Question> questionsPool);
    }
}
