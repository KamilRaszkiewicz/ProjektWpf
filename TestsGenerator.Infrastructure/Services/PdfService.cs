using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuestPDF.Drawing.Exceptions;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGenerator.App;
using TestsGenerator.App.Interfaces;
using TestsGenerator.Domain.Models.Tests;
using TestsGenerator.Infrastructure.Pdf;

namespace TestsGenerator.Infrastructure.Services
{
    internal class PdfService : IPdfService
    {
        private readonly TestPdfDocumentOptions _options;
        private readonly ILogger<PdfService> _logger;

        public PdfService(IOptions<TestPdfDocumentOptions> options, ILogger<PdfService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task<byte[]?> GeneratePdfAsync(Test test, CancellationToken? ct = null)
        {
            try
            {
                var document = new TestPdfDocument(test, _options);

                var result = await Task.Run(document.GeneratePdf, ct ?? CancellationToken.None);

                return result;
            }
            catch(Exception e)
            {
                _logger.LogException(nameof(PdfService), nameof(GeneratePdfAsync), e);
            }

            return null;
        }
    }
}
