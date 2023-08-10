using System.Windows.Controls;
using WPF_Template.ViewModels.Pages;

namespace WPF_Template.Views;

public partial class Homepage : Page
{
    public Homepage(HomeViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
