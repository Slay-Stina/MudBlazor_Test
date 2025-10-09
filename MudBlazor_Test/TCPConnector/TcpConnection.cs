using System.Net.Sockets;
using System.Text;

namespace MudBlazor_Test.TCPConnector;

public class TcpConnection : IDisposable
{
    private readonly TcpClient _client;
    private readonly NetworkStream _stream;
    private readonly Encoding _encoding = Encoding.Unicode;
    private readonly int _bytesize = 1024 * 1024;

    public bool IsConnected => _client.Connected;

    public TcpConnection(string host, int port)
    {
        _client = new TcpClient();
        try
        {
            _client.Connect(host, port);
            _stream = _client.GetStream();
        }
        catch
        {
            _client.Dispose();
            throw;
        }
    }

    public void Send(string message)
    {
        var bytes = _encoding.GetBytes(message);
        _stream.Write(bytes, 0, bytes.Length);
    }

    public string Receive()
    {
        var responseBytes = new byte[_bytesize];
        int bytesRead = _stream.Read(responseBytes, 0, responseBytes.Length);
        return _encoding.GetString(responseBytes, 0, bytesRead);
    }

    public void Dispose()
    {
        _stream?.Dispose();
        _client?.Dispose();
    }
}
