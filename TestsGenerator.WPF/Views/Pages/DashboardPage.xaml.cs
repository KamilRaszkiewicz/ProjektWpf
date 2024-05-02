using Microsoft.Web.WebView2.Core;
using System.Windows.Controls;
using TestsGenerator.WPF.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace TestsGenerator.WPF.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void Add_Answer(object sender, RoutedEventArgs e)
        {
            Answers_Grid.Items.Add(new {}) ;
        }
    }
}
