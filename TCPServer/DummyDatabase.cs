using System.Text.Json;
using TCPServer.Models;

namespace TCPServer;

public class DummyDatabaseModel
{
    public LineInfo DefaultLine { get; set; } = new();
    public List<LineInfo> Lines { get; set; } = new();
}

public class DummyDatabase
{
    private readonly string _filePath = "lines.json";
    private DummyDatabaseModel _data;
    private static readonly Random _random = new();

    public DummyDatabase()
    {
        Load();
    }

    public void LogConnection(LineInfo line)
    {
        _data.Lines.Add(line);
        Save();
    }

    public DummyDatabaseModel GetData() => _data;

    private void Load()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            _data = JsonSerializer.Deserialize<DummyDatabaseModel>(json) ?? GenerateRandomData();
        }
        else
        {
            _data = GenerateRandomData();
            Save();
        }
    }

    private DummyDatabaseModel GenerateRandomData()
    {
        var model = new DummyDatabaseModel();
        int lineCount = _random.Next(2, 6); // 2-5 lines
        int defaultIdx = lineCount > 0 ? _random.Next(0, lineCount) : -1;
        int selectedIdx = lineCount > 0 ? _random.Next(0, lineCount) : -1;
        for (int j = 0; j < lineCount; j++)
        {
            var line = new LineInfo
            {
                Name = $"Line_{j + 1}_{Guid.NewGuid().ToString()[..4]}",
                IpAddress = $"192.168.1.{j + 10}",
                Portnumber = 1000 + _random.Next(0, 9000),
                IsDefault = j == defaultIdx,
                IsSelected = j == selectedIdx
            };
            model.Lines.Add(line);
            if (j == defaultIdx)
                model.DefaultLine = line;
        }
        return model;
    }

    private void Save()
    {
        var json = JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}
