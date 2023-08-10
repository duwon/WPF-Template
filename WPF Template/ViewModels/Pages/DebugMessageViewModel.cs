using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WPF_Template.Messages;

namespace WPF_Template.ViewModels.Pages;

public partial class DebugMessageViewModel : ObservableObject
{
    public DebugMessageViewModel()
    {
        // Register a message in some module
        WeakReferenceMessenger.Default.Register<PrintDebugMessage>(this, (r, m) =>
        {
            DebugMessageText += $"{m.Value}\r\n";
        });
    }

    [ObservableProperty]
    private string _debugMessageText;

    [RelayCommand]
    private void OnWriteDebugMessage()
    {
        DebugMessageText += "TEST\r\n";
    }
}
