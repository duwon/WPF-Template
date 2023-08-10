using System.Windows.Controls;
using WPF_Template.ViewModels.Pages;

namespace WPF_Template.Views.Pages;

public partial class HomePage : Page
{
    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
