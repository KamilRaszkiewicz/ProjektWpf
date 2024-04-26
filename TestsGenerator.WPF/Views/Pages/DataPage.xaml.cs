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
        }

    }
}
