namespace SudInfo.EfDataAccessLibrary.Models;
public class Monitor : BaseModel
{
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(50, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? Name { get; set; }
    public int ScreenSize { get; set; }
    public int ScreenResolutionWidth { get; set; }
    public int ScreenResolutionHeight { get; set; }
    [NotMapped]
    public string ScreenResolution => $"{ScreenResolutionWidth}x{ScreenResolutionHeight}";
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(20)]
    public string? SerialNumber { get; set; }
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(20)]
    public string? InventarNumber { get; set; }
    public int YearRelease { get; set; }
    public Computer? Computer { get; set; }
}