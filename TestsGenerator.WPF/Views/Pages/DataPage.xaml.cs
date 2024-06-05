using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using TestsGenerator.App.Interfaces;
using TestsGenerator.Domain.Models.Questions;
using TestsGenerator.Domain.Models.Tests;
using TestsGenerator.WPF.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace TestsGenerator.WPF.Views.Pages
{
    public partial class DataPage : INavigableView<DataViewModel>
    {
        public DataViewModel ViewModel { get; }

        private readonly IPdfService _pdfService;

        public DataPage(DataViewModel viewModel, IPdfService pdfService)
        {
            ViewModel = viewModel;
            _pdfService = pdfService;
            InitializeComponent();
            DataContext = this;
        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = Categories_ComboBox.SelectedItem as Category;

            if (item == null)
            {
                return;
            }
            else if(item.Name== "Wszystkie pytania")
            {
                ViewModel.Questions = ViewModel.GetQuestions();
            }
            else
            {
                ViewModel.Questions = ViewModel.GetQuestionsWithGivenCategory(item);
            }

        }
        private void Add_template(object sender, RoutedEventArgs e)
        {
            ViewModel.AddTemplateCommand.Execute(this);
        }
        private void Template_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var listView = sender as System.Windows.Controls.ListView;
            var template = (TestTemplate)listView.SelectedItem;
            ViewModel.SelectedTemplate = template;
            foreach(var question in ViewModel.Questions)
            {
                foreach (var q in template.QuestionPool)
                {
                    if (question.questionName == q.QuestionContent)
                    {      
                        question.isInPool = true; break;
                    }
                    else
                    {
                        question.isInPool = false;
                    }
                }
            }

        }


        private void generate_templates (object sender, RoutedEventArgs e)
        {
            TextInputDialog dialog = new TextInputDialog();
            int testCount = int.Parse(valueTextBox.Text);
            var template = ViewModel.SelectedTemplate;
            if (template == null || testCount==0 || template.QuestionPool.Count<=5)
            {
                Error error = new Error();
                error.ShowDialog();
            }
            else if (dialog.ShowDialog() == true)
            {
                Test test = new Test();
                test.VersionIdentifier = dialog.InputText;
                List<TestQuestionOrdinal> testList = new List<TestQuestionOrdinal>();
               for (int i = 0;i<testCount; i++)
                {
                    var randomQuestions = GetRandomQuestions(template.QuestionPool, 5);
                    
                    foreach(var question  in randomQuestions )
                    {
                        TestQuestionOrdinal testQuestion = new TestQuestionOrdinal();
                        testQuestion.Question = question;
                        testList.Add(testQuestion);
                    }
                }
               test.QuestionsOrdinals = testList;
               ViewModel.AddTestsCommand.Execute(test);
            }
            else
            {
                return;
            }

        }

        public static List<T> GetRandomQuestions<T>(List<T> list, int count)
        {
            if (count > list.Count)
            {
                throw new ArgumentException("Count cannot be greater than the number of elements in the list.");
            }

            var random = new Random();
            return list.OrderBy(x => random.Next()).Take(count).ToList();
        }
    }
}

