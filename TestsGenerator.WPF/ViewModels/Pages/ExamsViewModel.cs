using System.Collections.ObjectModel;
using System.Windows.Controls;
using TestsGenerator.App.Services;
using TestsGenerator.Domain.Models.Questions;
using TestsGenerator.Domain.Models.Tests;
using TestsGenerator.WPF.Views.Pages;
using Wpf.Ui.Controls;

namespace TestsGenerator.WPF.ViewModels.Pages
{
    public partial class ExamsViewModel : ObservableObject, INavigationAware
    {
        private readonly TemplatesService _templatesService;
        private bool _isInitialized = false;

        [ObservableProperty]
        private TestTemplate _selectedTemplate;

        [ObservableProperty]
        private ObservableCollection<TestTemplate> _templates;

        [ObservableProperty]
        private ObservableCollection<Test> _tests;

        [ObservableProperty]
        private Test _selectedTest;

        [ObservableProperty]
        private TestQuestionOrdinal _selectedQuestion;
        public ExamsViewModel(TemplatesService templatesService)
        {
            _templatesService = templatesService;
        }
        public void OnNavigatedTo()
        {
            InitializeViewModel();
        }


        public void OnNavigatedFrom() { }

        public void InitializeViewModel()
        {
            Templates = new ObservableCollection<TestTemplate>(_templatesService.GetAllTemplates());
            Tests = new ObservableCollection<Test>();
        }

        [RelayCommand]
        private void ChangeTemplate(TestTemplate template)
        {
            SelectedTemplate = template;

            var tests = _templatesService.GetTestTemplatesTests(template);

            Tests.Clear();
            foreach (var t in tests)
            {
                Tests.Add(t);
            }
        }

        [RelayCommand]
        private void GenerateTests((TestTemplate template, int testsToGenerate) data)
        {
            var (template, testsToGenerate) = data;

            _templatesService.GenerateTestsAsync(template, testsToGenerate)
                .ContinueWith(x =>
                {
                    Tests.Clear();

                    foreach (var t in template.Tests)
                    {
                        Tests.Add(t);
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        [RelayCommand]
        private void ChangeTest(Test test)
        {
            SelectedTest = test;
        }
    }
}
