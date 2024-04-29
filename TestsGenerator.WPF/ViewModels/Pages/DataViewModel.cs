using Microsoft.Extensions.Logging;
using System.Buffers.Text;
using System.Linq;
using System.Text.Json;
using System.Windows.Documents;
using System.Windows.Media;
using TestsGenerator.App;
using TestsGenerator.App.Interfaces;
using TestsGenerator.Domain.Models.Questions;
using TestsGenerator.Domain.Models.Tests;
using TestsGenerator.WPF.Models;
using Wpf.Ui.Controls;

namespace TestsGenerator.WPF.ViewModels.Pages
{
    public partial class DataViewModel : ObservableObject, INavigationAware
    {


        [ObservableProperty]
        private IEnumerable<DataColor> _colors;

        private bool _isInitialized = false;
        private readonly ITestsTemplatesService _testsTemplateService;
        private readonly ILogger<DataViewModel> _logger;

        public DataViewModel(
            ITestsTemplatesService testsTemplateService, // Wstrzykujemy serwis od templejtów testów
            ILogger<DataViewModel> logger
        )
        {
            _testsTemplateService = testsTemplateService;
            _logger = logger;
        }


        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        public void OnNavigatedFrom() { }

        private void InitializeViewModel()
        {
            //odpalamy na osobnym tasku żeby nie blokowoć UI
            Task.Run(async () => await CreateTestTemplate())    //Tworzenie template'a
                .ContinueWith(t =>
                {
                    var id = t.Result;

                    if (id != null)
                    {
                        var template = _testsTemplateService.GetTestTemplate(id.Value); //pobieranie i wypisywanie

                        _logger.LogInformation($"Pa jaki fajny template: {JsonSerializer.Serialize(template)}");
                    }
                });

            var random = new Random();
            var colorCollection = new List<DataColor>();

            for (int i = 0; i < 8192; i++)
                colorCollection.Add(
                    new DataColor
                    {
                        Color = new SolidColorBrush(
                            Color.FromArgb(
                                (byte)200,
                                (byte)random.Next(0, 250),
                                (byte)random.Next(0, 250),
                                (byte)random.Next(0, 250)
                            )
                        )
                    }
                );

            Colors = colorCollection;

            _isInitialized = true;
        }

        private async Task<long?> CreateTestTemplate()
        {
            try
            {
                //a dodam se tutaj test - trza będzie podpiąc pod kontrolki
                long? id = await _testsTemplateService.CreateTestsTemplateAsync($"Jakis Tam Test {Random.Shared.NextInt64()}", [
                    new Question
                    {
                        QuestionContent = "Treść pytania 1",
                        Answers = [
                            new Answer
                            {
                                Content = "aaaaa"
                            },
                            new Answer
                            {
                                Content = "bbbb"
                            },
                            new Answer
                            {
                                Content = "cccc"
                            }
                        ],
                        MinPoints = 0,
                        MaxPoints = 2,
                    },
                    new Question
                    {
                        QuestionContent = "Treść pytania 2",
                        Answers = [
                            new Answer
                            {
                                Content = "1111"
                            },
                            new Answer
                            {
                                Content = "2222"
                            },
                            new Answer
                            {
                                Content = "3333"
                            }
                        ],
                        MinPoints = 0,
                        MaxPoints = 2,
                    }
                ]);

                if(id == null)
                {
                    throw new Exception("cosik nie tak");
                }

                _logger.LogInformation($"Udało się dodać szablon testu o id {id}");

                return id;
            }
            catch( Exception ex)
            {
                _logger.LogException(nameof(DataViewModel), nameof(CreateTestTemplate), ex);
            }

            return null;
        }
    }
}
