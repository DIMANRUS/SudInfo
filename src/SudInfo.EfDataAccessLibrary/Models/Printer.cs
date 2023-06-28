﻿namespace SudInfo.EfDataAccessLibrary.Models;
public class Printer : BaseModel
{
    [XLColumn(Header = "Наименование")]
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(100)]
    public string? Name { get; set; }
    
    [XLColumn(Header = "Тип")]
    public PrinterType Type { get; set; } = PrinterType.Принтер;
    
    [XLColumn(Header = "IP Адрес")]
    [StringLength(12)]
    [RegularExpression(Const.Ip4RegularExpression, ErrorMessage = Const.NotValidIp4Message)]
    public string? Ip { get; set; }
    
    [XLColumn(Ignore = true)]
    public int NumberCabinet { get; set; }
    
    [XLColumn(Header = "Год производства")]
    [RegularExpression(Const.NumberGreaterThen0, ErrorMessage = Const.NumberCannotBeGreaterThen0Message)]
    public int YearRelease { get; set; }
    
    [XLColumn(Header = "Серийный номер")]
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(50)]
    public string? SerialNumber { get; set; }
    
    [XLColumn(Header = "Инвентарный номер")]
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(50)]
    public string? InventarNumber { get; set; }
    
    public Computer? Computer { get; set; }
    
    [XLColumn(Header = "Номер кабинета")]
    [NotMapped]
    public int Cabinet => (Computer == null) ? NumberCabinet : Computer.Cabinet;
}

public enum PrinterType
{
    Принтер, МФУ, КМА
}