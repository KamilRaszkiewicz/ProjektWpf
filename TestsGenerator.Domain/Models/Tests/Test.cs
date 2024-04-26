using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGenerator.Domain.Models.Tests
{
    public class Test
    {
        public long Id { get; set; }
        public long TestTemplatesId { get; set; }

        public string VersionIdentifier { get; set; }

        public TestTemplate TestTemplate { get; set; }

        public List<TestQuestionOrdinal> QuestionsOrdinals { get; set; }
        public List<TestQuestionAnswerOrdinal> QuestionsAnswersOrdinals { get; set; }

    }
}
