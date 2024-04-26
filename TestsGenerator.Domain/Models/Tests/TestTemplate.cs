using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGenerator.Domain.Models.Questions;

namespace TestsGenerator.Domain.Models.Tests
{
    public class TestTemplate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Question> QuestionPool { get; set; }
        public List<Test> Tests { get; set; }
    }
}
