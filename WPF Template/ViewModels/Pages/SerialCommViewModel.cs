using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;
using WPF_Template.Messages;
using WPF_Template.Models;

namespace WPF_Template.ViewModels.Pages;

public partial class SerialCommViewModel : ObservableObject
{
    public SerialCommViewModel()
    {
        OnGetPortNames();
        Serial.RceivedEvent += new SerialConfig.ReceivedHandler(DataReceivedEvent);
    }

    private void PrintfDebugMessage(string message)
    {
        WeakReferenceMessenger.Default.Send(new PrintDebugMessage(message));
    }

    private void DataReceivedEvent()
    {
        SerialRxHexString = BitConverter.ToString(Serial.GetData()).Replace("-", " ");
        PrintfDebugMessage($"[RX] {SerialRxHexString}");
    }

    [ObservableProperty]
    private SerialConfig _serial = new();

    [ObservableProperty]
    private string _serialTxString;
    [ObservableProperty]
    private string _serialRxHexString;

    [RelayCommand]
    private void OnSerialConnect()
    {
        if (Serial.IsOpen)
        {
            Serial.Close();
        }
        else
        {
            Serial.Open();
        }
    }

    [RelayCommand]
    private void OnGetPortNames()
    {
        Serial.GetPortNames();
    }

    [RelayCommand]
    private void OnSendData()
    {
        if (Serial.Send(SerialTxString))
            PrintfDebugMessage($"[TX] {BitConverter.ToString(Encoding.UTF8.GetBytes(SerialTxString)).Replace("-", " ")}");
    }
}
