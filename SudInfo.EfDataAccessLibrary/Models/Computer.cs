namespace SudInfo.EfDataAccessLibrary.Models;
public class Computer : BaseModel
{
    [Required]
    [StringLength(15)]
    public string? Ip { get; set; }
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }
    public OS OS { get; set; } = OS.Windows7;
    [Required]
    [StringLength(40)]
    public string? CPU { get; set; }
    [StringLength(40)]
    public string? GPU { get; set; }
    public int ROM { get; set; }
    public int RAM { get; set; }
    public int NumberCabinet { get; set; }
    [Required]
    [StringLength(20)]
    public string? SerialNumber { get; set; }
    [Required]
    [StringLength(20)]
    public string? InventarNumber { get; set; }
    public int YearRelease { get; set; }
    public User? User { get; set; }
    [NotMapped]
    public int Cabinet => (User == null) ? NumberCabinet : User.NumberCabinet;

    public ICollection<Periphery>? Peripheries { get; set; }
    public ICollection<Monitor>? Monitors { get; set; }
    public ICollection<Printer>? Printers { get; set; }
}

public enum OS
{
    Windows7, Windows10, Linux, WindowsXP
}