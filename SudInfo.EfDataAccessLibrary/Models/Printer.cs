namespace SudInfo.EfDataAccessLibrary.Models;
public class Printer : BaseModel
{
    [Required]
    [StringLength(40)]
    public string? Name { get; set; }
    public PrinterType Type { get; set; } = PrinterType.Принтер;
    [StringLength(maximumLength: 12)]
    public string? Ip { get; set; }
    public int NumberCabinet { get; set; }
    public int YearRelease { get; set; }
    [Required]
    [StringLength(20)]
    public string? SerialNumber { get; set; }
    [Required]
    [StringLength(20)]
    public string? InventarNumber { get; set; }
    public Computer? Computer { get; set; }
    [NotMapped]
    public int Cabinet => (Computer == null) ? NumberCabinet : Computer.Cabinet;
}

public enum PrinterType
{
    Принтер, МФУ
}