using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

using MahApps.Metro.Controls;

using WPF_Template.Contracts.Services;
using WPF_Template.Contracts.ViewModels;
using WPF_Template.Contracts.Views;

namespace WPF_Template.Services;

public class WindowManagerService : IWindowManagerService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPageService _pageService;

    public Window MainWindow
        => Application.Current.MainWindow;

    public WindowManagerService(IServiceProvider serviceProvider, IPageService pageService)
    {
        _serviceProvider = serviceProvider;
        _pageService = pageService;
    }

    public void OpenInNewWindow(string key, object parameter = null)
    {
        var window = GetWindow(key);
        if (window != null)
        {
            window.Activate();
        }
        else
        {
            window = new MetroWindow()
            {
                Title = "WPF_Template",
                Style = Application.Current.FindResource("CustomMetroWindow") as Style
            };
            var frame = new Frame()
            {
                Focusable = false,
                NavigationUIVisibility = NavigationUIVisibility.Hidden
            };

            window.Content = frame;
            var page = _pageService.GetPage(key);
            window.Closed += OnWindowClosed;
            window.Show();
            frame.Navigated += OnNavigated;
            var navigated = frame.Navigate(page, parameter);
        }
    }

    public bool? OpenInDialog(string key, object parameter = null)
    {
        var shellWindow = _serviceProvider.GetService(typeof(IShellDialogWindow)) as Window;
        var frame = ((IShellDialogWindow)shellWindow).GetDialogFrame();
        frame.Navigated += OnNavigated;
        shellWindow.Closed += OnWindowClosed;
        var page = _pageService.GetPage(key);
        var navigated = frame.Navigate(page, parameter);
        return shellWindow.ShowDialog();
    }

    public Window GetWindow(string key)
    {
        foreach (Window window in Application.Current.Windows)
        {
            var dataContext = window.GetDataContext();
            if (dataContext?.GetType().FullName == key)
            {
                return window;
            }
        }

        return null;
    }

    /// <summary>
    /// 현재 열려 있는 해당 Window 종료
    /// </summary>
    /// <param name="key"></param>
    /// <returns>true:해당 윈도우 종료, false:활성화된 Window 없음으로 종료 안됨</returns>
    public bool CloseWindow(string key)
    {
        foreach (Window window in Application.Current.Windows)
        { 
            var dataContext = window.GetDataContext();
            if (dataContext?.GetType().FullName == key)
            { 
                window.Close();
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 현재 활성화된 모든 Window 종료
    /// </summary>
    /// <param name="key"></param>
    public void CloseWindow()
    {
        foreach (Window window in Application.Current.Windows)
        {
            window.Close();
        }
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        if (sender is Frame frame)
        {
            var dataContext = frame.GetDataContext();
            if (dataContext is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedTo(e.ExtraData);
            }
        }
    }

    private void OnWindowClosed(object sender, EventArgs e)
    {
        if (sender is Window window)
        {
            if (window.Content is Frame frame)
            {
                frame.Navigated -= OnNavigated;
            }

            window.Closed -= OnWindowClosed;
        }
    }
}
