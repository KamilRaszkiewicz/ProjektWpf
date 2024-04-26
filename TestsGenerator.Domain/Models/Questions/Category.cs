using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGenerator.Domain.Models.Questions
{
    public class Category
    {
        public long Id { get; set; }
        public bool Name { get; set; }

        public List<Question> Questions { get; set; }
    }
}
