using System.Windows.Controls;
using WPF_Template.ViewModels.Pages;

namespace WPF_Template.Views.Pages;

public partial class SerialCommPage : Page
{
    public SerialCommPage(SerialCommViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
