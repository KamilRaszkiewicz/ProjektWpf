using System.Collections.ObjectModel;
using TestsGenerator.App.Services;
using TestsGenerator.Domain.Models.Questions;
using TestsGenerator.Domain.Models.Tests;
using TestsGenerator.WPF.Views.Pages;
using static TestsGenerator.WPF.ViewModels.Pages.DataViewModel;

namespace TestsGenerator.WPF.ViewModels.Pages
{
    public partial class ExamsViewModel : ObservableObject
    {

        private readonly TemplatesService _templatesService;

        [ObservableProperty]
        private Test _selectedTest;

        [ObservableProperty]
        private ObservableCollection<Test> _tests;

        public ExamsViewModel(TemplatesService templatesService)
        {
            _templatesService = templatesService;
        }

        public ObservableCollection<Test> GetTests()
        {
            var tests = new List<Test>(_templatesService.GetAllTests());
            return new ObservableCollection<Test>(_templatesService.GetAllTests());
        }

        public void InitializeViewModel()
        {
            Tests = new ObservableCollection<Test>(GetTests());
        }
    }
}
