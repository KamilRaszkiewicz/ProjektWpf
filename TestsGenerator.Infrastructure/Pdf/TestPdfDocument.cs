using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGenerator.Domain.Models.Tests;

namespace TestsGenerator.Infrastructure.Pdf
{
    internal class TestPdfDocument : IDocument
    {
        private readonly Test _test;
        private readonly TestPdfDocumentOptions _options;

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public TestPdfDocument(Test test, TestPdfDocumentOptions options)
        {
            _test = test;
            _options = options;
        }

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(60);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);

                    page
                        .Header()
                        .ShowOnce()
                        .PaddingBottom(10)
                        .BorderBottom(1)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(1);
                            });

                            table.Cell().Row(1).Column(1).Text(_test.TestTemplate.Name).Bold().FontSize(18);

                            table.Cell().Row(1).Column(2).Text($"Wersja {_test.VersionIdentifier}").Bold();
                        });

                    page.Content().Column(column =>
                    {
                        foreach (var question in _test.QuestionsOrdinals.OrderBy(x => x.Ordinal))
                        {
                            column.Item().Component(
                                new QuestionComponent(
                                    question,
                                    _test.QuestionsAnswersOrdinals.Where(x => x.QuestionsId == question.QuestionsId).ToList(),
                                    _options.QuestionComponentOptions
                                )
                            );
                        }
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
        }
    }
}
