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
            if (dialog.ShowDialog() == true)
            {
                string userInput = dialog.InputText;
                // Zrób coś z wprowadzonym tekstem
            }

        }


    }
}

