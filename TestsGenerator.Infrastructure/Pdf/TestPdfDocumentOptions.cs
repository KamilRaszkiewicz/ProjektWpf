using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGenerator.Infrastructure.Pdf
{
    internal class TestPdfDocumentOptions
    {
        public TextStyle HeaderTextStyle { get; set; } = TextStyle.Default;
        public TextStyle VersionTextStyle { get; set; } = TextStyle.Default;
        public QuestionComponentOptions QuestionComponentOptions { get; set; } = new();
    }

    internal class QuestionComponentOptions
    {
        public TextStyle QuestionsTextStyle { get; set; } = TextStyle.Default;
        public TextStyle AnswersTextStyle { get; set; } = TextStyle.Default;

        public int MaxAnswersPerRow { get; set; } = 4;
    }
}
