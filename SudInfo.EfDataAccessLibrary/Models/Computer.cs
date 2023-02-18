namespace SudInfo.EfDataAccessLibrary.Models;
public class Computer : BaseModel
{
    [StringLength(15)]
    public required string Ip { get; set; }
    [StringLength(50)]
    public required string Name { get; set; }
    public OS OS { get; set; } = OS.Windows7;
    [StringLength(40)]
    public string CPU { get; set; }
    [StringLength(40)]
    public string? GPU { get; set; }
    public int ROM { get; set; }
    public byte RAM { get; set; }
    public int NumberCabinet { get; set; }
    [StringLength(20)]
    public required string SerialNumber { get; set; }
    [StringLength(20)]
    public required string InventarNumber { get; set; }
    public int YearRelease { get; set; }
    public User? User { get; set; }
    [NotMapped]
    public int Cabinet => (User == null) ? NumberCabinet : User.NumberCabinet;

    public List<Periphery> Peripheries { get; set; } = new();
    public List<Monitor> Monitors { get; set; } = new();
    public List<Printer> Printers { get; set; } = new();
}

public enum OS
{
    Windows7, Windows10, Linux, WindowsXP
}