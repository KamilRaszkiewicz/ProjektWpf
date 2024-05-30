using System.Collections.ObjectModel;
using TestsGenerator.App.Interfaces;
using TestsGenerator.App.Services;
using TestsGenerator.Domain.Models.Questions;
using Wpf.Ui.Controls;

namespace TestsGenerator.WPF.ViewModels.Pages
{
    public partial class QuestionsViewModel : ObservableObject, INavigationAware
    {
        private readonly QuestionsService _questionsService;
        private bool _isInitialized = false;

        [ObservableProperty]
        private ObservableCollection<Question> _questions;

        [ObservableProperty]
        private ObservableCollection<Category> _categories;

        [ObservableProperty]
        private Question _selectedQuestion;

        [RelayCommand]
        private void AddQuestion()
        {
            //podejscie 2 - robimy zmiany w komendzie *** ZALECAM ***

            _questions.Add(new Question
            {
                QuestionAnswers = new ObservableCollection<QuestionAnswer>()
            });

        }

       

        [RelayCommand]
        private void SaveQuestion()
        {
            _questionsService.SaveQuestion(_selectedQuestion).ContinueWith(x => 
            {
                Questions = QuestionsListToObservableColleciton(_questionsService.GetAllQuestions());
            });
            
        }

        public QuestionsViewModel(QuestionsService questionsService)
        {
            _questionsService = questionsService;
        }

        public void InitializeViewModel()
        {
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

        public void OnNavigatedFrom()
        {
        }

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

    }
}
