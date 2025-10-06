using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using TCPServer.Models;

namespace TCPServer;

public class ClientHandler
{
    private readonly TcpClient _client;
    private readonly DummyDatabase _db;
    private static readonly ConnectionLogger _logger = new();

    public ClientHandler(TcpClient client, DummyDatabase db)
    {
        _client = client;
        _db = db;
    }

    public void Handle()
    {
        const int bufferSize = 4096;
        var stream = _client.GetStream();
        var buffer = new byte[bufferSize];
        int bytesRead;
        var remoteEndPoint = _client.Client.RemoteEndPoint?.ToString() ?? "unknown";
        var ip = _client.Client.RemoteEndPoint is System.Net.IPEndPoint ipEp ? ipEp.Address.ToString() : "unknown";
        var port = _client.Client.RemoteEndPoint is System.Net.IPEndPoint ipEp2 ? ipEp2.Port : 0;

        try
        {
            StringBuilder messageBuilder = new StringBuilder();
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string part = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                messageBuilder.Append(part);
                if (bytesRead < bufferSize) break;
            }

            string message = messageBuilder.ToString();
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine($"Message received from {remoteEndPoint}: {message}");

                string reply = HandleCommand(message);
                byte[] replyBytes = Encoding.Unicode.GetBytes(reply);
                stream.Write(replyBytes, 0, replyBytes.Length);
            }
        }
        catch (Exception ex)
        {
            _logger.Log(new ConnectionLogEntry
            {
                IpAddress = ip,
                Port = port,
                RemoteEndPoint = remoteEndPoint,
                Message = null,
                Timestamp = DateTime.UtcNow,
                Success = false,
                Exception = ex.ToString()
            });
            Console.WriteLine($"Client connection error: {ex.Message}");
        }
        finally
        {
            _client.Close();
        }
    }

    private string HandleCommand(string message)
    {
        try
        {
            var data = _db.GetData();
            if (message.StartsWith("GET_ALL_LINES"))
            {
                return JsonSerializer.Serialize(data);
            }
            else if (message.StartsWith("GET_LINE_BY_NAME:"))
            {
                var name = message[17..];
                var line = data.Lines.FirstOrDefault(l => l.Name == name);
                return line != null ? JsonSerializer.Serialize(line) : "";
            }
            else if (message.StartsWith("ADD_LINE:"))
            {
                var json = message[9..];
                var line = JsonSerializer.Deserialize<LineInfo>(json);
                if (line != null)
                {
                    data.Lines.Add(line);
                    return "OK";
                }
                return "FAIL";
            }
            else if (message.StartsWith("UPDATE_LINE:"))
            {
                var json = message[12..];
                var updatedLine = JsonSerializer.Deserialize<LineInfo>(json);
                if (updatedLine != null)
                {
                    var idx = data.Lines.FindIndex(l => l.Name == updatedLine.Name);
                    if (idx >= 0)
                    {
                        data.Lines[idx] = updatedLine;
                        return "OK";
                    }
                }
                return "FAIL";
            }
            else if (message.StartsWith("DELETE_LINE:"))
            {
                var json = message[12..];
                var line = JsonSerializer.Deserialize<LineInfo>(json);
                if (line != null)
                {
                    var idx = data.Lines.FindIndex(l => l.Name == line.Name);
                    if (idx >= 0)
                    {
                        data.Lines.RemoveAt(idx);
                        return "OK";
                    }
                }
                return "FAIL";
            }
            else if (message.StartsWith("SET_DEFAULT:"))
            {
                var json = message[12..];
                var line = JsonSerializer.Deserialize<LineInfo>(json);
                if (line != null)
                {
                    foreach (var l in data.Lines)
                        l.IsDefault = false;
                    var lineToSet = data.Lines.FirstOrDefault(l => l.Name == line.Name);
                    if (lineToSet != null)
                    {
                        lineToSet.IsDefault = true;
                        return "OK";
                    }
                }
                return "FAIL";
            }
            else if (message.StartsWith("SET_SELECTED:"))
            {
                var json = message[13..];
                var line = JsonSerializer.Deserialize<LineInfo>(json);
                if (line != null)
                {
                    foreach (var l in data.Lines)
                        l.IsSelected = false;
                    var lineToSet = data.Lines.FirstOrDefault(l => l.Name == line.Name);
                    if (lineToSet != null)
                    {
                        lineToSet.IsSelected = true;
                        return "OK";
                    }
                }
                return "FAIL";
            }
            else
            {
                return "UNKNOWN_COMMAND";
            }
        }
        catch (Exception ex)
        {
            return $"ERROR: {ex.Message}";
        }
    }
}
