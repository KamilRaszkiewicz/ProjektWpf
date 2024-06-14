using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGenerator.Domain.Models.Questions;

namespace TestsGenerator.Domain.Models.Tests
{
    public class TestQuestionAnswerOrdinal
    {
        public long TestsId { get; set; }
        public long QuestionsId { get; set; }
        public long AnswersId { get; set; }

        public int Ordinal { get; set; }

        public Test Test { get; set; }
        public Answer Answer { get; set; }
        public Question Question { get; set; }
    }
}
