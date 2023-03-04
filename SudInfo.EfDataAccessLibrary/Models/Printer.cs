namespace SudInfo.EfDataAccessLibrary.Models;
public class Printer : BaseModel
{
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(40)]
    public string? Name { get; set; }
    public PrinterType Type { get; set; } = PrinterType.Принтер;
    [StringLength(12)]
    [RegularExpression(Const.Ip4RegularExpression, ErrorMessage = Const.NotValidIp4Message)]
    public string? Ip { get; set; }
    public int NumberCabinet { get; set; }
    public int YearRelease { get; set; }
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(20)]
    public string? SerialNumber { get; set; }
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(20)]
    public string? InventarNumber { get; set; }
    public Computer? Computer { get; set; }
    [NotMapped]
    public int Cabinet => (Computer == null) ? NumberCabinet : Computer.Cabinet;
}

public enum PrinterType
{
    Принтер, МФУ, КМА
}