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

        public Category Category { get; set; }

        public IList<Answer> Answers { get; set; }   //just to display  //IList coby ObservableCollection pasowalo
        public IList<QuestionAnswer> QuestionAnswers { get; set; }   //to check wether answer is correct //IList coby ObservableCollection pasowalo
    }
}
