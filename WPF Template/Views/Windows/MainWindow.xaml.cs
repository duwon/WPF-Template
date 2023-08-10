using MahApps.Metro.Controls;
using System.Windows.Controls;
using WPF_Template.Contracts.Views;
using WPF_Template.ViewModels.Windows;

namespace WPF_Template.Views.Windows;

public partial class MainWindow : MetroWindow, IMainWindow
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public Frame GetNavigationFrame()
        => mainFrame;

    public void ShowWindow()
        => Show();

    public void CloseWindow()
        => Close();
}
