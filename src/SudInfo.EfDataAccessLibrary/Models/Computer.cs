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
    [StringLength(100, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? Name { get; set; }

    [XLColumn(Header = "Операционная система")]
    public OS OS { get; set; } = OS.Windows7;

    [XLColumn(Header = "Процессор")]
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(100, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? CPU { get; set; }

    [XLColumn(Header = "Видеокарта")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? GPU { get; set; }

    [XLColumn(Header = "Диск")]
    [RegularExpression(Const.NumberGreaterThen0, ErrorMessage = Const.NumberCannotBeGreaterThen0Message)]
    public int ROM { get; set; } = 120;

    [XLColumn(Header = "Оперативная память")]
    [RegularExpression(Const.NumberGreaterThen0, ErrorMessage = Const.NumberCannotBeGreaterThen0Message)]
    public int RAM { get; set; } = 1;

    [XLColumn(Ignore = true)]
    [RegularExpression(Const.NumberGreaterThen0, ErrorMessage = Const.NumberCannotBeGreaterThen0Message)]
    public int NumberCabinet { get; set; } = 1;

    [XLColumn(Header = "Серийный номер")]
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(50)]
    public string? SerialNumber { get; set; }

    [XLColumn(Header = "Инвентарный номер")]
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(50)]
    public string? InventarNumber { get; set; }

    [XLColumn(Header = "Год производства")]
    public int YearRelease { get; set; }

    [XLColumn(Header = "Сломан")]
    public bool IsBroken { get; set; }

    [XLColumn(Header = "Описание поломки")]
    [StringLength(200)]
    public string? BreakdownDescription { get; set; }

    [XLColumn(Header = "ВКС")]
    public bool IsVks { get; set; }

    [XLColumn(Header = "Списан")]
    public bool IsDecommissioned { get; set; }

    [XLColumn(Ignore = true)]
    public User? User { get; set; }

    [XLColumn(Header = "Номер кабинета")]
    [NotMapped]
    public int Cabinet => User?.NumberCabinet ?? NumberCabinet;

    [XLColumn(Ignore = true)]
    [NotMapped]
    public string ComputerNameWithUserSurFirstName => Name + " - " + User?.LastName + " " + User?.FirstName;

    [XLColumn(Ignore = true)]
    public ICollection<Periphery>? Peripheries { get; set; }

    [XLColumn(Ignore = true)]
    public ICollection<Monitor>? Monitors { get; set; }

    [XLColumn(Ignore = true)]
    public ICollection<Printer>? Printers { get; set; }

    [XLColumn(Ignore = true)]
    public ICollection<AppEntity>? Apps { get; set; }
}

public enum OS
{
    Windows7, Windows10, Linux, WindowsXP
}