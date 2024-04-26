using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGenerator.Domain.Models.Questions;
using TestsGenerator.Domain.Models.Tests;

namespace TestsGenerator.Infrastructure.Pdf
{
    internal class QuestionComponent : IComponent
    {
        private readonly TestQuestionOrdinal _question;
        private readonly List<TestQuestionAnswerOrdinal> _answers;
        private readonly QuestionComponentOptions _options;
        private readonly uint _numberOfColumns;

        public QuestionComponent(TestQuestionOrdinal question, List<TestQuestionAnswerOrdinal> answers, QuestionComponentOptions options)
        {
            _question = question;
            _answers = answers;
            _options = options;
            _numberOfColumns = (uint)Math.Min(_answers.Count(), _options.MaxAnswersPerRow);
        }

        public void Compose(IContainer container)
        {

            container
                .PaddingTop(20)
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        for (int i = 0; i < _numberOfColumns; i++)
                        {
                            columns.RelativeColumn();
                        }

                    });


                    table.Cell().ColumnSpan(_numberOfColumns).Text(text =>
                    {
                        
                        text.AlignLeft();
                        text.DefaultTextStyle(_options.QuestionsTextStyle);
                        text.Line(_question.Question.QuestionContent);
                    });

                    foreach (var answer in _answers.OrderBy(x => x.Ordinal))
                    {
                        table.Cell().ColumnSpan(1).Element(x => x.Padding(10)).Text(text =>
                        {
                            text.AlignLeft();
                            text.DefaultTextStyle(_options.AnswersTextStyle);
                            text.Line($"{answer.Ordinal % 4 + 1}. {answer.Answer.Content}");
                        });
                    }
                });
        }
    }
}
