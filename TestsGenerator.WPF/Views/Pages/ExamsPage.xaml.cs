using Microsoft.Web.WebView2.Core;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using TestsGenerator.Domain.Models.Questions;
using TestsGenerator.Domain.Models.Tests;
using TestsGenerator.WPF.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace TestsGenerator.WPF.Views.Pages
{
    public partial class ExamsPage : INavigableView<ExamsViewModel>
    {

       

        public ExamsViewModel ViewModel { get; }
        public ExamsPage(ExamsViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            
        }
        private void Test_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as System.Windows.Controls.ListView;
            ViewModel.SelectedTest = (Test)listView.SelectedItem;
        }
    }

}
