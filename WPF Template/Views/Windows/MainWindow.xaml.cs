using MahApps.Metro.Controls;
using System.Windows.Controls;
using WPF_Template.Contracts.Views;
using WPF_Template.ViewModels.Windows;

namespace WPF_Template.Views.Windows;

public partial class MainWindow : MetroWindow, IMainWindow
{

    public event EventHandler CloseEvent;

    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public Frame GetNavigationFrame()
        => mainFrame;

    public void ShowWindow()
    {
        Show();
        Closed += MainWindow_Closed;
    }

    private void MainWindow_Closed(object sender, EventArgs e)
    {
        CloseEvent?.Invoke(this, EventArgs.Empty);
    }

    public void CloseWindow()
        => Close();
}
