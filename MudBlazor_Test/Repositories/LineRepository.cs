using MudBlazor_Test.TCPConnector;
using MudBlazor_Test_Domain.Entities;
using MudBlazor_Test_Domain.Repositories;
using System.Text.Json;

namespace MudBlazor_Test.Repositories;

public class LineRepository : ILineRepository
{
    private readonly string _host;
    private readonly int _port;

    public LineRepository(string host, int port)
    {
        _host = host;
        _port = port;
    }

    public async Task<IEnumerable<Line>> GetAllAsync()
    {
        using var conn = new TcpConnection(_host, _port);
        conn.Send("GET_ALL_LINES");
        var response = conn.Receive();
        var wrapper = JsonSerializer.Deserialize<LineListWrapper>(response);
        return wrapper?.Lines ?? new List<Line>();
    }

    public async Task<Line?> GetByNameAsync(string name)
    {
        using var conn = new TcpConnection(_host, _port);
        conn.Send($"GET_LINE_BY_NAME:{name}");
        var response = conn.Receive();
        return JsonSerializer.Deserialize<Line>(response);
    }

    public async Task AddAsync(Line line)
    {
        using var conn = new TcpConnection(_host, _port);
        var json = JsonSerializer.Serialize(line);
        conn.Send($"ADD_LINE:{json}");
        conn.Receive();
    }

    public async Task UpdateAsync(Line line)
    {
        using var conn = new TcpConnection(_host, _port);
        var json = JsonSerializer.Serialize(line);
        conn.Send($"UPDATE_LINE:{json}");
        conn.Receive();
    }

    public async Task DeleteAsync(Line line)
    {
        using var conn = new TcpConnection(_host, _port);
        var json = JsonSerializer.Serialize(line);
        conn.Send($"DELETE_LINE:{json}");
        conn.Receive();
    }

    public async Task SetDefaultAsync(Line line)
    {
        using var conn = new TcpConnection(_host, _port);
        var json = JsonSerializer.Serialize(line);
        conn.Send($"SET_DEFAULT:{json}");
        conn.Receive();
    }

    public async Task SetSelectedAsync(Line line)
    {
        using var conn = new TcpConnection(_host, _port);
        var json = JsonSerializer.Serialize(line);
        conn.Send($"SET_SELECTED:{json}");
        conn.Receive();
    }

    private class LineListWrapper
    {
        public List<Line> Lines { get; set; } = new();
    }
}