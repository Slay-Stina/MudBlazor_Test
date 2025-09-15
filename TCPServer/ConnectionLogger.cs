using TCPServer.Models;

namespace TCPServer;

public class ConnectionLogger
{
    private readonly string _logFilePath = "connection_log.log";
    private readonly object _lock = new();

    public void Log(ConnectionLogEntry entry)
    {
        string status = entry.Success ? "Success" : "Error";
        string exception = string.IsNullOrEmpty(entry.Exception) ? "" : $" | Exception: {entry.Exception}";
        string message = string.IsNullOrEmpty(entry.Message) ? "" : entry.Message.Replace("\n", " ").Replace("\r", " ");
        string logLine = $"[{entry.Timestamp:yyyy-MM-dd HH:mm:ss}] {entry.IpAddress}:{entry.Port} - {status} - {message}{exception}";
        lock (_lock)
        {
            File.AppendAllText(_logFilePath, logLine + "\n");
        }
    }
}
