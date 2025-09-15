namespace TCPServer;

class Program
{
    static void Main(string[] args)
    {
        var db = new DummyDatabase();
        var server = new Server(3077, db);
        server.Start();
    }
}
