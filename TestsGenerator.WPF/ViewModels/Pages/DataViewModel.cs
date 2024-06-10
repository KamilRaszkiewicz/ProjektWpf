using Microsoft.Extensions.Logging;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
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
        private ObservableCollection<TemplatesWithQuestions> _questions;

        [ObservableProperty]
        private ObservableCollection<Question> _questionsNotChanged;

        [ObservableProperty]
        private ObservableCollection<Category> _categories;

        [ObservableProperty]
        private ObservableCollection<TestTemplate> _templates;

        [ObservableProperty]
        private Category _selectedCategory;

        [ObservableProperty]
        private TestTemplate _selectedTemplate;

        public DataViewModel(QuestionsService questionsService, TemplatesService templatesService )
        {
            _questionsService = questionsService;
            _templatesService = templatesService;
        }
        public class TemplatesWithQuestions()
        {
            public string questionName { get; set; }
            public bool isInPool { get; set; }
            public long id { get; set; }
            public long categoryId { get; set; }
            public string categoryName { get; set; }

        }
        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }


        public void OnNavigatedFrom() { }

        public ObservableCollection<TemplatesWithQuestions> GetQuestions()
        {
            var questions = new List<Question>(_questionsService.GetAllQuestions());
            return new ObservableCollection<TemplatesWithQuestions>(questions.Select(x => new TemplatesWithQuestions { id = x.Id, questionName = x.QuestionContent, categoryName = x.Category.Name, }).ToList());
        }
        public ObservableCollection<TemplatesWithQuestions> GetQuestionsWithGivenCategory(Category category)
        {
            var questions = new List<Question>(_questionsService.GetQuestionsWithGivenCategory(category));

            return new ObservableCollection<TemplatesWithQuestions>(questions.Select(x => new TemplatesWithQuestions {id=x.Id, questionName = x.QuestionContent, categoryName = x.Category.Name,  }).ToList());
        }
        public void InitializeViewModel()
        {
            Templates = new ObservableCollection<TestTemplate>(_templatesService.GetAllTemplates());

            QuestionsNotChanged = new ObservableCollection<Question>(_questionsService.GetAllQuestions());
            Questions = QuestionsListToObservableColleciton(_questionsService.GetAllQuestions());
            var categories = new ObservableCollection<Category>(_questionsService.GetCategories());

            Category all = new Category();
            all.Name = "Wszystkie pytania";
            categories.Insert(0,all);
            Categories = categories;

            // Categories = new ObservableCollection<Category>(_questionsService.GetCategories());
        }


        private ObservableCollection<TemplatesWithQuestions> QuestionsListToObservableColleciton(List<Question> questions)
        {
            return new ObservableCollection<TemplatesWithQuestions>(questions.Select(x => new TemplatesWithQuestions {id = x.Id, questionName = x.QuestionContent, categoryName = x.Category.Name, categoryId = x.Category.Id}).ToList());
        }


        [RelayCommand]
        private void SaveTemplate()
        {
            foreach (var question in Questions)
            {
                if (question.isInPool)
                {
                    Question question1 = QuestionsNotChanged.Where(p=>p.QuestionContent==question.questionName).FirstOrDefault();
                    _selectedTemplate.QuestionPool.Add(question1);
                }
            }

          _templatesService.SaveTemplate(_selectedTemplate).ContinueWith(x =>
            {
                Templates = new ObservableCollection<TestTemplate>(_templatesService.GetAllTemplates());
            });
         
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
           await _templatesService.SaveTemplate(temp).ContinueWith(x =>
            {
                Templates = new ObservableCollection<TestTemplate>(_templatesService.GetAllTemplates());
            });

            
        }

        [RelayCommand]
        private async Task AddTests(Test tests)
        {
            await _templatesService.SaveTest(tests);
        }
    }
}
