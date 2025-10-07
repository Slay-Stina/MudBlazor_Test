using System.Text.Json;
using TCPServer.Models;

namespace TCPServer;

// The database is now just a List<LineInfo>
public class DummyDatabase
{
    private readonly string _filePath = "lines.json";
    private List<LineInfo> _data;
    private static readonly Random _random = new();

    public DummyDatabase()
    {
        Load();
    }

    public void LogConnection(LineInfo line)
    {
        _data.Add(line);
        Save();
    }

    public List<LineInfo> GetData() => _data;

    private void Load()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            _data = JsonSerializer.Deserialize<List<LineInfo>>(json) ?? GenerateRandomData();
        }
        else
        {
            _data = GenerateRandomData();
            Save();
        }
    }

    private List<LineInfo> GenerateRandomData()
    {
        var list = new List<LineInfo>();
        int lineCount = _random.Next(2, 6); // 2-5 lines
        int selectedIdx = lineCount > 0 ? _random.Next(0, lineCount) : -1;
        for (int j = 0; j < lineCount; j++)
        {
            var line = new LineInfo
            {
                Name = $"Line_{j + 1}_{Guid.NewGuid().ToString()[..4]}",
                IpAddress = $"192.168.1.{j + 10}",
                Portnumber = 1000 + _random.Next(0, 9000),
                IsDefault = false,
                IsSelected = j == selectedIdx
            };
            list.Add(line);
        }
        return list;
    }

    private void Save()
    {
        var json = JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}
