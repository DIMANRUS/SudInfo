
namespace SudInfo.EfDataAccessLibrary.Models;
public class Computer : BaseModel
{
    [XLColumn(Header = "IP адрес")]
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(15)]
    [RegularExpression(Const.Ip4RegularExpression, ErrorMessage = Const.NotValidIp4Message)]
    public string? Ip { get; set; }
    
    [XLColumn(Header = "Наименование")]
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(50,MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? Name { get; set; }
    
    [XLColumn(Header = "Операционная система")]
    public OS OS { get; set; } = OS.Windows7;
    
    [XLColumn(Header = "Процессор")]
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(40, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? CPU { get; set; }
    
    [XLColumn(Header = "Видеокарта")]
    [StringLength(40, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? GPU { get; set; }
    
    [XLColumn(Header = "Диск")]
    public int ROM { get; set; }
    
    [XLColumn(Header = "Оперативная память")]
    public int RAM { get; set; }
    
    [XLColumn(Ignore = true)]
    public int NumberCabinet { get; set; }
    
    [XLColumn(Header = "Серийный номер")]
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(20)]
    public string? SerialNumber { get; set; }
    
    [XLColumn(Header = "Инвентарный номер")]
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(20)]
    public string? InventarNumber { get; set; }
    
    [XLColumn(Header = "Год производства")]
    public int YearRelease { get; set; }
    
    [XLColumn(Ignore = true)]
    public User? User { get; set; }
    
    [XLColumn(Header = "Номер кабинета")]
    [NotMapped]
    public int Cabinet => User?.NumberCabinet ?? NumberCabinet;
    
    [XLColumn(Ignore = true)]
    public ICollection<Periphery>? Peripheries { get; set; }
    
    [XLColumn(Ignore = true)]
    public ICollection<Monitor>? Monitors { get; set; }
    
    [XLColumn(Ignore = true)]
    public ICollection<Printer>? Printers { get; set; }
    
    [XLColumn(Ignore = true)]
    public ICollection<AppEntity> Apps { get; set; }
}

public enum OS
{
    Windows7, Windows10, Linux, WindowsXP
}