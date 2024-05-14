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
            DataContext = this;

            InitializeComponent();
            for (int i = 1; i <= 10; i++)
            {
                listView.Items.Add("Szablon " + i.ToString());
            }
        }
        private void Add_template(object sender, RoutedEventArgs e)
        {
            listView.Items.Add("Nowy szablon");
        }

        private void Change_Template_Name(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                Debug.Print(listView.SelectedItem.ToString() + " " + template_name.Text);
                listView.SelectedItem = template_name.Text;
                listView.Items.Refresh();

            }

        }
    }

}

