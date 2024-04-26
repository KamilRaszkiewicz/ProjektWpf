using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestsGenerator.App.Interfaces;
using TestsGenerator.Domain.Models.Questions;
using TestsGenerator.Domain.Models.Tests;
using TestsGenerator.WPF.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace TestsGenerator.WPF.Views.Pages
{
    /// <summary>
    /// Interaction logic for PdfPage.xaml
    /// </summary>
    public partial class PdfPage : INavigableView<PdfViewModel>
    {
        public PdfViewModel ViewModel { get; }

        private readonly IPdfService _pdfService;

        public PdfPage(PdfViewModel viewModel, IPdfService pdfService)
        {
            ViewModel = viewModel;
            _pdfService = pdfService;
            DataContext = this;

            InitializeComponent();
        }

        private string GetRandomString(int minLength, int maxLength)
        {
            byte[] randomStringBuffer = new byte[maxLength];
            int length = Random.Shared.Next(minLength, maxLength);

            Random.Shared.NextBytes(randomStringBuffer);

            return Convert.ToBase64String(randomStringBuffer.Take(length).ToArray());
        }

        private async void webView_Initialized(object sender, EventArgs e)
        {
            var questions = Enumerable.Range(1, 20).Select(x => new Question
            {
                Id = x,
                QuestionContent = $"Pytanie {x}: {GetRandomString(64, 128)}",
                Answers = Enumerable.Range(1, 4).Select(y => new Answer
                {
                    Id = y,
                    Content = GetRandomString(4, 32)
                }).ToList()
            });

            var test = new Test
            {
                Id = 1,
                VersionIdentifier = "A",

                TestTemplate = new TestTemplate
                {
                    Name = "Nazwa testu"
                },

                QuestionsOrdinals = questions.Select((x, i) => new TestQuestionOrdinal
                {
                    TestsId = 1,
                    QuestionsId = 1,
                    Question = x,
                    Ordinal = i,
                }).ToList(),

                QuestionsAnswersOrdinals = questions.SelectMany(x => x.Answers.Select((y, i) => new TestQuestionAnswerOrdinal
                {
                    TestsId = 1,
                    QuestionsId = x.Id,
                    AnswersId = y.Id,
                    Answer = y,
                    Ordinal = i,
                })).ToList()
            };

            await webView.EnsureCoreWebView2Async(null);

            var pdf = await _pdfService.GeneratePdfAsync(test);

            await Application.Current.Dispatcher.BeginInvoke(() =>
            {
                webView.CoreWebView2.Navigate($"data:application/pdf;base64,{Convert.ToBase64String(pdf)}");
            });

        }
    }
}
