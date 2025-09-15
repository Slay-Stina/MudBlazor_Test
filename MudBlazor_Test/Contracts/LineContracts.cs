namespace MudBlazor_Test.Contracts;

public class LineRequest
{
    public string Name { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public int PortNumber { get; set; }
    public bool IsDefault { get; set; }
    public bool IsSelected { get; set; }
}

public class LineResponse
{
    public string Name { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public int PortNumber { get; set; }
    public bool IsDefault { get; set; }
    public bool IsSelected { get; set; }
}