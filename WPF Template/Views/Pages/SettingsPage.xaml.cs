using System.Windows.Controls;
using WPF_Template.ViewModels.Pages;

namespace WPF_Template.Views;

public partial class SettingsPage : Page
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
