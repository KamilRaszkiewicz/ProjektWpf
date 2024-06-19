using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace TestsGenerator.WPF.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "Generator testów";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Pytania",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Question24 },
                TargetPageType = typeof(Views.Pages.QuestionsPage)

            },
            new NavigationViewItem()
            {
                Content = "Szablony",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Document24 },
                TargetPageType = typeof(Views.Pages.DataPage)
            },
            new NavigationViewItem()
            {
                Content = "Testy",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DocumentPdf24 },
                TargetPageType = typeof(Views.Pages.ExamsPage)
            },
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Ustawienia",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };
    }
}
