using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using WPF_Template.Contracts.Services;
using WPF_Template.Properties;
using WPF_Template.ViewModels.Pages;

namespace WPF_Template.ViewModels.Windows;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IWindowManagerService _windowManagerService;

    public MainWindowViewModel(INavigationService navigationService, IWindowManagerService windowManagerService)
    {
        _navigationService = navigationService;
        _windowManagerService = windowManagerService;
    }

    /// <summary>
    /// 좌측 메뉴 상단
    /// </summary>
    public ObservableCollection<HamburgerMenuItem> MenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
    {
        new HamburgerMenuGlyphItem() { Label = Resources.MainHomePage, Glyph = "\uE80F", TargetPageType = typeof(HomeViewModel) },
        new HamburgerMenuGlyphItem() { Label = Resources.MainHomePage, Glyph = "\uE8B1", TargetPageType = typeof(SerialCommViewModel) },
    };

    /// <summary>
    /// 좌측 메뉴 바닥
    /// </summary>
    public ObservableCollection<HamburgerMenuItem> OptionMenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
    {
        // new HamburgerMenuGlyphItem() { Label = Resources.MainDebugMessagePage, Glyph = "\uE8A2", TargetPageType = typeof(DebugMessageViewModel) },
        new HamburgerMenuGlyphItem() { Label = Resources.MainSettingsPage, Glyph = "\uE713", TargetPageType = typeof(SettingsViewModel) },
    };

    #region ObservableProperty
    [ObservableProperty]
    private HamburgerMenuItem _selectedMenuItem;

    [ObservableProperty]
    private HamburgerMenuItem _selectedOptionsMenuItem;
    #endregion

    #region RelayCommand

    [RelayCommand]
    private void OnLoaded()
    {
        _navigationService.Navigated += OnNavigated;
    }

    [RelayCommand]
    private void OnUnloaded()
    {
        _navigationService.Navigated -= OnNavigated;
    }

    [RelayCommand]
    private void OnGoBack()
        => _navigationService.GoBack();

    [RelayCommand]
    private void OnMenuItemInvoked()
        => NavigateTo(SelectedMenuItem.TargetPageType);

    [RelayCommand]
    private void OnOptionsMenuItemInvoked()
        => NavigateTo(SelectedOptionsMenuItem.TargetPageType);

    [RelayCommand]
    private void NewDebugWindow()
    {
        if (_windowManagerService.GetWindow(typeof(DebugMessageViewModel).FullName) == null)
        {
            _windowManagerService.OpenInNewWindow(typeof(DebugMessageViewModel).FullName, "DEBUG");
        }
    }

    #endregion

    #region INavigationService
    private void NavigateTo(Type targetViewModel)
    {
        if (targetViewModel != null)
        {
            _navigationService.NavigateTo(targetViewModel.FullName);
        }
    }

    private void OnNavigated(object sender, string viewModelName)
    {
        var item = MenuItems
                    .OfType<HamburgerMenuItem>()
                    .FirstOrDefault(i => viewModelName == i.TargetPageType?.FullName);
        if (item != null)
        {
            SelectedMenuItem = item;
        }
        else
        {
            SelectedOptionsMenuItem = OptionMenuItems
                    .OfType<HamburgerMenuItem>()
                    .FirstOrDefault(i => viewModelName == i.TargetPageType?.FullName);
        }

        GoBackCommand.NotifyCanExecuteChanged();
    }
    #endregion
}
