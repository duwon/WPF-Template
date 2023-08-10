using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WPF_Template.ViewModels;

public class MainDialogViewModel : ObservableObject
{
    private ICommand _closeCommand;

    public ICommand CloseCommand => _closeCommand ?? (_closeCommand = new RelayCommand(OnClose));

    public Action<bool?> SetResult { get; set; }

    public MainDialogViewModel()
    {
    }

    private void OnClose()
    {
        bool result = true;
        SetResult(result);
    }
}
