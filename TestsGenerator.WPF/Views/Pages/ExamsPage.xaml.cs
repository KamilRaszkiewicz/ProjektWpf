using Microsoft.Web.WebView2.Core;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using TestsGenerator.App.Interfaces;
using TestsGenerator.Domain.Models.Questions;
using TestsGenerator.Domain.Models.Tests;
using TestsGenerator.WPF.ViewModels.Pages;
using Wpf.Ui.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace TestsGenerator.WPF.Views.Pages
{
    public partial class ExamsPage : INavigableView<ExamsViewModel>
    {
        private readonly IPdfService _pdfService;

        public ExamsViewModel ViewModel { get; }
        public ExamsPage(ExamsViewModel viewModel, IPdfService pdfService)
        {
            _pdfService = pdfService;
            ViewModel = viewModel;
            InitializeComponent();
            DataContext = this;

        }
        private void Test_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as System.Windows.Controls.ListView;
            ViewModel.SelectedTest = (Test)listView.SelectedItem;
            ViewModel.SelectedQuestion = null;
        }

        private void Question_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedQuestion = (Question)Questions.SelectedItem;

            ViewModel.SelectQuestionCommand.Execute((Question)Questions.SelectedItem);
        }

        private void CBTemplates_Initialized(object sender, EventArgs e)
        {

        }

        private void CBTemplates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (sender as ComboBox).SelectedItem;

            if (item != null)
            {
                ViewModel.ChangeTemplateCommand.Execute(item);
            }
        }

        private async void CBTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var test = (sender as ComboBox).SelectedItem as Test;

            if(test != null)
            {
                ViewModel.ChangeTestCommand.Execute(test);


                var pdf = await _pdfService.GeneratePdfAsync(test, CancellationToken.None);

                await System.Windows.Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    webView.CoreWebView2.Navigate($"data:application/pdf;base64,{Convert.ToBase64String(pdf)}");
                });

            }
        }

        private void CBTests_Initialized(object sender, EventArgs e)
        {

        }

        private void GenerateTestsBtn_Click(object sender, RoutedEventArgs e)
        {
            var template = (TestTemplate)CBTemplates.SelectedItem;
            var testsToGenerate = (int)slider.Value;

            this.ViewModel.GenerateTestsCommand.Execute((template, testsToGenerate));
        }

        private async void webView_Initialized(object sender, EventArgs e)
        {
            await webView.EnsureCoreWebView2Async(null);
        }
    }

}
