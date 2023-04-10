namespace SudInfo.EfDataAccessLibrary.Models;
public class Monitor : BaseModel
{
    [XLColumn(Header = "Наименование")]
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(50, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? Name { get; set; }
    
    [XLColumn(Header = "Размер экрана")]
    public int ScreenSize { get; set; }
    
    [XLColumn(Ignore = true)]
    public int ScreenResolutionWidth { get; set; }
    
    [XLColumn(Ignore = true)]
    public int ScreenResolutionHeight { get; set; }
    
    [XLColumn(Header = "Разрешение экрана")]
    [NotMapped]
    public string ScreenResolution => $"{ScreenResolutionWidth}x{ScreenResolutionHeight}";
    
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
    public Computer? Computer { get; set; }
}