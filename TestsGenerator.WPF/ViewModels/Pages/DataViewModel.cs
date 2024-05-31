using Microsoft.Extensions.Logging;
using System.Buffers.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using TestsGenerator.App;
using TestsGenerator.App.Interfaces;
using TestsGenerator.App.Services;
using TestsGenerator.Domain.Models.Questions;
using TestsGenerator.Domain.Models.Tests;
using TestsGenerator.WPF.Models;
using Wpf.Ui.Controls;

namespace TestsGenerator.WPF.ViewModels.Pages
{
    public partial class DataViewModel : ObservableObject, INavigationAware
    {
        private readonly QuestionsService _questionsService;
        private readonly TemplatesService _templatesService;
        private bool _isInitialized = false;

        [ObservableProperty]
        private ObservableCollection<Question> _questions;

        [ObservableProperty]
        private ObservableCollection<Category> _categories;

        [ObservableProperty]
        private ObservableCollection<TestTemplate> _templates;

        [ObservableProperty]
        private Question _selectedCategory;

        [ObservableProperty]
        private Question _selectedTemplate;

        public DataViewModel(QuestionsService questionsService, TemplatesService templatesService )
        {
            _questionsService = questionsService;
            _templatesService = templatesService;
        }


        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }


        public void OnNavigatedFrom() { }


        public ObservableCollection<Question> GetQuestionsWithGivenCategory(Category category)
        {
            return new ObservableCollection<Question>(_questionsService.GetQuestionsWithGivenCategory(category));
        }
        public void InitializeViewModel()
        {
            Templates = new ObservableCollection<TestTemplate>(_templatesService.GetAllTemplates());

            Questions = QuestionsListToObservableColleciton(_questionsService.GetAllQuestions());

            Categories = new ObservableCollection<Category>(_questionsService.GetCategories());
        }
        private ObservableCollection<Question> QuestionsListToObservableColleciton(List<Question> questions)
        {
            return new ObservableCollection<Question>(questions.Select(x =>
            {
                x.QuestionAnswers = new ObservableCollection<QuestionAnswer>(x.QuestionAnswers);

                return x;
            }).ToList());
        }


        [RelayCommand]
        private void SaveTemplate()
        {
          /*  _templatesService.SaveTemplate(_selectedTemplate).ContinueWith(x =>
            {
                Templates = new ObservableCollection<TestTemplate>(_templatesService.GetAllTemplates());
            });
          */
        }


        [RelayCommand]
        private async Task AddTemplate()
        {
            var temp = new TestTemplate
            {
                Name = "Nowy Szablon",
                Tests = new List<Test>()
            };
            Templates.Add(temp);

            
        }
    }
}
