namespace SudInfo.EfDataAccessLibrary.Models;
public class Printer : BaseModel
{
    [StringLength(40)]
    public required string Name { get; set; }
    public PrinterType Type { get; set; } = PrinterType.Принтер;
    [StringLength(maximumLength: 12)]
    public string? Ip { get; set; }
    public int NumberCabinet { get; set; }
    public int YearRelease { get; set; }
    [StringLength(20)]
    public required string SerialNumber { get; set; }
    [StringLength(20)]
    public required string InventarNumber { get; set; }
    public Computer? Computer { get; set; }
    [NotMapped]
    public int Cabinet => (Computer == null) ? NumberCabinet : Computer.Cabinet;
}

public enum PrinterType
{
    Принтер, МФУ
}