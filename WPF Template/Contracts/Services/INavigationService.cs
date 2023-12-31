﻿using System.Windows.Controls;

namespace WPF_Template.Contracts.Services;

public interface INavigationService
{
    event EventHandler<string> Navigated;

    bool CanGoBack { get; }

    void Initialize(Frame mainFrame);

    bool NavigateTo(string pageKey, object parameter = null, bool clearNavigation = false);

    void GoBack();

    void UnsubscribeNavigation();

    void CleanNavigation();
}
