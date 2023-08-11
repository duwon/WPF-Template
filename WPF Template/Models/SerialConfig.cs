using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.IO.Ports;

namespace WPF_Template.Models;

public partial class SerialConfig : ObservableObject
{
    public delegate void ReceivedHandler();
    public event ReceivedHandler RceivedEvent;

    private System.Collections.Concurrent.ConcurrentQueue<byte> RxBuffer = new();
    private readonly SerialPort _serialPort = new();

    [ObservableProperty]
    private ObservableCollection<string>? _comPortNames;

    [ObservableProperty]
    private string? _portName;

    [ObservableProperty]
    private int _baudRate = 115200;

    [ObservableProperty]
    private bool _isOpen = false;

    [ObservableProperty]
    private string? _rxByteHexStringBuffer;

    private byte[]? TxBuffer;

    private string? txHexString;
    public string? TxHexString
    {
        get => txHexString;
        set
        {
            SetProperty(ref txHexString, value);
        }
    }

    public SerialConfig()
    {
        _serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(DataReceivedEvent);
    }

    public string[] GetPortNames()
    {
        string[] _portNames = SerialPort.GetPortNames();
        ComPortNames = new ObservableCollection<string>(_portNames);
        return _portNames;
    }

    public void Open()
    {
        if ((PortName != null) && !_serialPort.IsOpen)
        {
            _serialPort.BaudRate = BaudRate;
            _serialPort.PortName = PortName;
            _serialPort.Open();
            IsOpen = true;
        }
    }

    public void Close()
    {
        _serialPort.Close();
        IsOpen = false;
    }

    private void DataReceivedEvent(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
    {
        try
        {
            int iRecivedSize = _serialPort.BytesToRead;

            if (iRecivedSize != 0) // 수신 데이터가 있으면
            {
                byte[] buff = new byte[iRecivedSize];

                _serialPort.Read(buff, 0, iRecivedSize);

                // RxByteHexStringBuffer += BitConverter.ToString(buff).Replace("-", " ") + " ";
                // PrintDebugMsg($"[RX] {BitConverter.ToString(buff).Replace("-", " ")}");

                for (int i = 0; i < buff.Length; i++)
                {
                    RxBuffer.Enqueue(buff[i]);
                }

                RceivedEvent?.Invoke();
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// 버퍼의 모든 데이터
    /// </summary>
    public byte[] RxData
    {
        get
        {
            return RxBuffer.ToArray();
        }
    }
    /// <summary>
    /// 버퍼에서 1바이트 읽기. 데이터 갯수 1 감소
    /// </summary>
    /// <returns></returns>
    public byte GetByte()
    {
        byte[] buff_que = new byte[1];
        RxBuffer.TryDequeue(out buff_que[0]);
        return buff_que[0];
    }
    /// <summary>
    /// 버퍼의 모든 데이터 읽기. 데이터 갯수 초기화.
    /// </summary>
    /// <returns></returns>
    public byte[] GetData()
    {
        byte[] arrBuff = RxBuffer.ToArray();
        while (RxBuffer.Count != 0)
        {
            byte[] buff_que = new byte[1];
            RxBuffer.TryDequeue(out buff_que[0]);
        }
        return arrBuff;
    }

    public bool Send()
    {
        if (!IsOpen) return false;
        if (TxBuffer == null) return false;

        _serialPort.Write(TxBuffer, 0, TxBuffer.Length);
        // TxBuffer = null;
        return true;
    }

    public bool Send(byte[] data)
    {
        if (!IsOpen) return false;
        if (data == null) return false;
        _serialPort.Write(data, 0, data.Length);
        return true;
    }

    public bool Send(string data)
    {
        if (!IsOpen) return false;
        if (data == null) return false;
        _serialPort.Write(data);
        return true;
    }
}
