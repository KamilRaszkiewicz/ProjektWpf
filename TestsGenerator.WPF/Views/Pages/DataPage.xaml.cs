using Microsoft.EntityFrameworkCore.Metadata;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TestsGenerator.App.Interfaces;
using TestsGenerator.Domain.Models.Questions;
using TestsGenerator.Domain.Models.Tests;
using TestsGenerator.WPF.ViewModels.Pages;
using TestsGenerator.WPF.Views.Windows;
using Wpf.Ui.Controls;
using System.Linq;

namespace TestsGenerator.WPF.Views.Pages
{
    public partial class DataPage : INavigableView<DataViewModel>
    {
        public const string ALL_CATGEGORIES = "Wszystkie kategorie";

        public DataViewModel ViewModel { get; }

        public MainWindow MainWindowRef { get; set; }
        public DataPage(DataViewModel viewModel)
        {
            ViewModel = viewModel;

            MainWindowRef = MainWindow.MainWindowRef;

            InitializeComponent();
            DataContext = this;

        }

        private void ApplyLVAllQuestionsFilter()
        {
            var cv = CollectionViewSource.GetDefaultView(lvAllQuestions.ItemsSource);
            var category = ViewModel.SelectedCategory;

            if (cv == null)
            {
                return;
            }

            if (category == null || category.Name == ALL_CATGEGORIES)
            {
                cv.Filter = (q) => !ViewModel.TemplateQuestions.Contains(q);
            }
            else
            {
                cv.Filter = (q) => (q as Question).Category.Id == category.Id && !ViewModel.TemplateQuestions.Contains(q);
            }
        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            ViewModel.SelectedCategory = (Category)(sender as ComboBox).SelectedItem;

            ApplyLVAllQuestionsFilter();

            CollectionViewSource.GetDefaultView(lvAllQuestions.ItemsSource)?.Refresh();
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
            ViewModel.TemplateQuestions.Clear();

            foreach(var q in template.QuestionPool)
            {
                ViewModel.TemplateQuestions.Add(q);
            }

            ApplyLVAllQuestionsFilter();

            CollectionViewSource.GetDefaultView(lvTemplateQuestions.ItemsSource)?.Refresh();
            CollectionViewSource.GetDefaultView(lvAllQuestions.ItemsSource)?.Refresh();
        }


        private void generate_templates (object sender, RoutedEventArgs e)
        {
            /*
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
            */
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

        private void Categories_ComboBox_Initialized(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;

            cb.SelectedIndex = 0;
        }


        private void BtnAddSelected_Click(object sender, RoutedEventArgs e)
        {
            var items = lvAllQuestions.SelectedItems.Cast<Question>().ToList();

            if (items.Any())
            {
                ViewModel.AddQuestionsToTemplateCommand.Execute(items);
            }

            ApplyLVAllQuestionsFilter();

            CollectionViewSource.GetDefaultView(lvTemplateQuestions.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(lvAllQuestions.ItemsSource).Refresh();
        }

        private void BtnAddAll_Click(object sender, RoutedEventArgs e)
        {
            var items = CollectionViewSource.GetDefaultView(lvAllQuestions.ItemsSource).Cast<Question>().ToList();

            ViewModel.AddQuestionsToTemplateCommand.Execute(items);

            ApplyLVAllQuestionsFilter();

            CollectionViewSource.GetDefaultView(lvTemplateQuestions.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(lvAllQuestions.ItemsSource).Refresh();
        }

        private void BtnRemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            var items = lvTemplateQuestions.SelectedItems.Cast<Question>().ToList();

            if (items.Any())
            {
                ViewModel.RemoveQuestionsFromTemplateCommand.Execute(items);
            }

            ApplyLVAllQuestionsFilter();

            CollectionViewSource.GetDefaultView(lvTemplateQuestions.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(lvAllQuestions.ItemsSource).Refresh();

        }

        private void BtnRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            var items = CollectionViewSource.GetDefaultView(lvTemplateQuestions.ItemsSource).Cast<Question>().ToList();

            ViewModel.RemoveQuestionsFromTemplateCommand.Execute(items);

            ApplyLVAllQuestionsFilter();

            CollectionViewSource.GetDefaultView(lvTemplateQuestions.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(lvAllQuestions.ItemsSource).Refresh();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            var items = CollectionViewSource.GetDefaultView(lvTemplateQuestions.ItemsSource).Cast<Question>().ToList();

            ViewModel.SaveTemplateCommand.Execute(items);
        }
    }
}

