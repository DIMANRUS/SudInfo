namespace SudInfo.EfDataAccessLibrary.Models;
public class Monitor : BaseModel
{
    [StringLength(50)]
    public required string Name { get; set; }
    public int ScreenSize { get; set; }
    public int ScreenResolutionWidth { get; set; }
    public int ScreenResolutionHeight { get; set; }
    [NotMapped]
    public string ScreenResolution => $"{ScreenResolutionWidth}x{ScreenResolutionHeight}";
    [StringLength(20)]
    public required string SerialNumber { get; set; }
    [StringLength(20)]
    public required string InventarNumber { get; set; }
    public int YearRelease { get; set; }
    public Computer? Computer { get; set; }
}