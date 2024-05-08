using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace TestsGenerator.WPF.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "WPF UI - TestsGenerator.WPF";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Pytania",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.DashboardPage)
            },
            new NavigationViewItem()
            {
                Content = "Szablony",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Save24 },
                TargetPageType = typeof(Views.Pages.DataPage)
            },
            new NavigationViewItem()
            {
                Content = "Testy",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Fingerprint24 },
                TargetPageType = typeof(Views.Pages.ExamsPage)
            },
            new NavigationViewItem()
            {
                Content = "Arkusze",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DocumentPdf24 },
                TargetPageType = typeof(Views.Pages.PdfPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Settings",
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
