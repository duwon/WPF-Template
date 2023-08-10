using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Options;

using WPF_Template.Contracts.Services;
using WPF_Template.Contracts.ViewModels;
using WPF_Template.Models;

namespace WPF_Template.ViewModels.Pages;

public partial class SettingsViewModel : ObservableObject, INavigationAware
{
    private readonly AppConfig _appConfig;
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly ISystemService _systemService;
    private readonly IApplicationInfoService _applicationInfoService;
    private readonly IWindowManagerService _windowManagerService;

    public SettingsViewModel(IOptions<AppConfig> appConfig, IThemeSelectorService themeSelectorService, ISystemService systemService, IApplicationInfoService applicationInfoService, IWindowManagerService windowManagerService)
    {
        _appConfig = appConfig.Value;
        _themeSelectorService = themeSelectorService;
        _systemService = systemService;
        _applicationInfoService = applicationInfoService;
        _windowManagerService = windowManagerService;

        ConfigurationsFolder = _appConfig.ConfigurationsFolder;
    }

    #region ObservableProperty

    [ObservableProperty]
    private AppTheme _theme;

    [ObservableProperty]
    private string _versionDescription;

    [ObservableProperty]
    private string _configurationsFolder;

    private bool isDebugMessageWindow;
    public bool IsDebugMessageWindow
    {
        get => isDebugMessageWindow;
        set
        {
            if (value == isDebugMessageWindow) return;
            if (!isDebugMessageWindow)
            {
                _windowManagerService.OpenInNewWindow(typeof(DebugMessageViewModel).FullName);
            }
            else
            {
                _windowManagerService.CloseWindow(typeof(DebugMessageViewModel).FullName);
            }
            SetProperty(ref isDebugMessageWindow, value);
        }
    }

    #endregion

    #region RelayCommand

    [RelayCommand]
    private void OnSetTheme(string themeName)
    {
        var theme = (AppTheme)Enum.Parse(typeof(AppTheme), themeName);
        _themeSelectorService.SetTheme(theme);
    }

    #endregion

    #region INavigationService

    public void OnNavigatedTo(object parameter)
    {
        VersionDescription = $"{Properties.Resources.AppDisplayName} - {_applicationInfoService.GetVersion()}";
        Theme = _themeSelectorService.GetCurrentTheme();
    }

    public void OnNavigatedFrom()
    {
    }

    #endregion
}
