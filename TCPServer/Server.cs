using System.Net;
using System.Net.Sockets;
using TCPServer;

namespace TCPServer;

public class Server
{
    private readonly int _port;
    private readonly DummyDatabase _db;

    public Server(int port, DummyDatabase db)
    {
        _port = port;
        _db = db;
    }

    public void Start()
    {
        IPEndPoint ep = new IPEndPoint(IPAddress.Any, _port);
        TcpListener server = new TcpListener(ep);
        server.Start();
        Console.WriteLine($"TCP server started on port: {_port}. Waiting for connections...");

        while (true)
        {
            var client = server.AcceptTcpClient();
            Task.Run(() => new ClientHandler(client, _db).Handle());
        }
    }
}
