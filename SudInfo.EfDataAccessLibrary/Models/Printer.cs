namespace SudInfo.EfDataAccessLibrary.Models;
public class Printer : BaseModel
{
    [StringLength(40)]
    public string Name { get; set; } = string.Empty;
    public PrinterType Type { get; set; } = PrinterType.Принтер;
    [StringLength(maximumLength: 12)]
    public string? Ip { get; set; }
    public int NumberCabinet { get; set; }
    public int YearRelease { get; set; }
    [StringLength(20)]
    public string SerialNumber { get; set; } = string.Empty;
    [StringLength(20)]
    public string InventarNumber { get; set; } = string.Empty;
    public Computer? Computer { get; set; }
    [NotMapped]
    public int Cabinet => (Computer == null) ? NumberCabinet : Computer.Cabinet;
}

public enum PrinterType
{
    Принтер, МФУ
}