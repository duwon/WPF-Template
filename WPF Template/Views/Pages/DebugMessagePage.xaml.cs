using System.Windows.Controls;
using WPF_Template.ViewModels.Pages;

namespace WPF_Template.Views.Pages;

public partial class DebugMessagePage : Page
{
    public DebugMessagePage(DebugMessageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
