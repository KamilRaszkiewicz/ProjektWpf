using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGenerator.Domain.Models.Questions;

namespace TestsGenerator.Domain.Models.Tests
{
    public class TestQuestionOrdinal
    {
        public long TestsId { get; set; }
        public long QuestionsId { get; set; }

        public int Ordinal { get; set; }

        public Test Test { get; set; }
        public Question Question { get; set; }
    }
}
