using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGenerator.Domain.Models.Questions
{
    public class Question
    {
        public long Id { get; set; }
        public decimal MaxPoints { get; set; }
        public decimal MinPoints { get; set; }

        public string QuestionContent { get; set; }

        public bool IsSingleChoice { get; set; }

        public List<Category> Categories { get; set; }

        public List<Answer> Answers { get; set; }   //just to display
        public List<QuestionAnswer> QuestionAnswers { get; set; }   //to check wether answer is correct
    }
}
