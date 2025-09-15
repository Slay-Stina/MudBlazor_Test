using System;

namespace MudBlazor_Test_Domain.Entities;

public class Line
{
    public string Name { get; set; }
    public string IpAddress { get; set; }
    public int Portnumber { get; set; }
    public bool IsDefault { get; set; }
    public bool IsSelected { get; set; }

    public Line() { }
    public Line(string name, string ipAddress, int portnumber)
    {
        Name = name;
        IpAddress = ipAddress;
        Portnumber = portnumber;
        IsDefault = false;
        IsSelected = false;
    }

    public void SetDefault(bool isDefault) => IsDefault = isDefault;
    public void SetSelected(bool isSelected) => IsSelected = isSelected;
    public void Update(string name, string ipAddress, int portnumber)
    {
        Name = name;
        IpAddress = ipAddress;
        Portnumber = portnumber;
    }
}
