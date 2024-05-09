using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TestsGenerator.Domain.Models.Questions;
using TestsGenerator.WPF.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace TestsGenerator.WPF.Views.Pages
{
    /// <summary>
    /// Interaction logic for QuestionsPage.xaml
    /// </summary>
    public partial class QuestionsPage : INavigableView<QuestionsViewModel>
    {
        public QuestionsViewModel ViewModel { get; }

        public QuestionsPage(QuestionsViewModel viewModel)
        {
            ViewModel = viewModel;


            InitializeComponent();
            DataContext = this;
        }
        private void Question_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as System.Windows.Controls.ListView;
            ViewModel.SelectedQuestion = (Question)listView.SelectedItem;
        } 

        private void Add_Answer(object sender, RoutedEventArgs e)
        {
            if(ViewModel.SelectedQuestion == null)
            {
                return;
            }

            //Podejscie 1 - robimy zmiany bezposrednio tu

            ViewModel.SelectedQuestion.QuestionAnswers.Add(new QuestionAnswer
            {
                Answer = new Answer()
            });
        }

        private void Delete_Answer(object sender, RoutedEventArgs e)
        {
            var item = Answers_Grid.SelectedItem as Answer;

            if (item == null)
            {
                return;
            }

            //Podejscie 1 - robimy zmiany bezposrednio tu

            var qa = ViewModel.SelectedQuestion.QuestionAnswers.First(x => x.Answer == item);
            ViewModel.SelectedQuestion.QuestionAnswers.Remove(qa);
        }
    }
}
