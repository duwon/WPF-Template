using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WPF_Template.Messages;

namespace WPF_Template.ViewModels.Pages;

public partial class HomeViewModel : ObservableObject
{
    public HomeViewModel()
    {
    }

    private void PrintfDebugMessage(string message)
    {
        WeakReferenceMessenger.Default.Send(new PrintDebugMessage(message));
    }

    [RelayCommand]
    private void OnSendDebugMessage()
    {
        PrintfDebugMessage("Home 페이지에서 테스트 메시지를 보냅니다.");
    }
}
