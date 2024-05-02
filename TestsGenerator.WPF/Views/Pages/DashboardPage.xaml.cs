using Microsoft.Web.WebView2.Core;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using TestsGenerator.WPF.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace TestsGenerator.WPF.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }
        private ObservableCollection<Answers> dataCollection = new ObservableCollection<Answers>();


        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            Answers_Grid.ItemsSource = dataCollection;
            Change_Width();
        }

        private async Task Change_Width()
        {
            await Task.Delay(1000);
            Answers_Grid.Columns[0].Width = new DataGridLength(730);

        }
        private void Add_Answer(object sender, RoutedEventArgs e)
        {
            dataCollection.Add(new Answers());
            Answers_Grid.Columns[0].Width = new DataGridLength(730);
        }

        private void Delete_Answer(object sender, RoutedEventArgs e)
        {
            if (Answers_Grid.SelectedItem != null)
            {
                Answers selectedData = (Answers)Answers_Grid.SelectedItem;
                dataCollection.Remove(selectedData);
            }
        }
    }

    public class Answers
    {
        public string Odpowiedź { get; set; }
        public bool Prawidłowa { get; set; }
        
        
    }
}
