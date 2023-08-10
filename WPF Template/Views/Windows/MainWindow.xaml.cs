﻿using System.Windows.Controls;

using MahApps.Metro.Controls;

using WPF_Template.Contracts.Views;
using WPF_Template.ViewModels.Windows;

namespace WPF_Template.Views;

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
