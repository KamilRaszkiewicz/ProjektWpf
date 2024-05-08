using Microsoft.Web.WebView2.Core;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using TestsGenerator.WPF.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace TestsGenerator.WPF.Views.Pages
{
    public partial class ExamsPage : INavigableView<ExamsViewModel>
    {

        private ObservableCollection<TestSheet> TestCollection = new ObservableCollection<TestSheet>();

        public ExamsViewModel ViewModel { get; }
        public ExamsPage(ExamsViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            
        }

    }
    public class TestSheet
    {
        public string Title { get; set; }
        public List<Questions> Questions { get; set; }
    }

    public class Questions
    {
        public string Content { get; set; }
        public List<Answerss> Answers { get; set; }
    }

    public class Answerss
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}
