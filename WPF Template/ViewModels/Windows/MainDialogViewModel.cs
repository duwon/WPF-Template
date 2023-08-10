using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WPF_Template.ViewModels.Windows;

public partial class MainDialogViewModel : ObservableObject
{
    public Action<bool?> SetResult { get; set; }

    public MainDialogViewModel()
    {
    }

    [RelayCommand]
    private void OnClose()
    {
        bool result = true;
        SetResult(result);
    }
}
