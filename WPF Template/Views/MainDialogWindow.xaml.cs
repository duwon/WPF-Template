using System.Windows.Controls;

using MahApps.Metro.Controls;

using WPF_Template.Contracts.Views;
using WPF_Template.ViewModels;

namespace WPF_Template.Views;

public partial class MainDialogWindow : MetroWindow, IShellDialogWindow
{
    public MainDialogWindow(MainDialogViewModel viewModel)
    {
        InitializeComponent();
        viewModel.SetResult = OnSetResult;
        DataContext = viewModel;
    }

    public Frame GetDialogFrame()
        => dialogFrame;

    private void OnSetResult(bool? result)
    {
        DialogResult = result;
        Close();
    }
}
