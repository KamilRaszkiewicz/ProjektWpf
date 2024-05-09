using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGenerator.Domain.Models.Questions
{
    public class QuestionAnswer
    {
        public long QuestionsId { get; set; }
        public long AnswersId { get; set; }

        public bool IsCorrect { get; set; } = false;

        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}
