using System.Windows.Controls;

namespace WPF_Template.Contracts.Views;

public interface IMainWindow
{
    Frame GetNavigationFrame();

    void ShowWindow();

    void CloseWindow();

    event EventHandler CloseEvent;
}
