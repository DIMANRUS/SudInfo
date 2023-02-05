namespace SudInfo.EfDataAccessLibrary.Models;
public class Monitor : BaseModel
{
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    public int ScreenSize { get; set; }
    public int ScreenResolutionWidth { get; set; }
    public int ScreenResolutionHeight { get; set; }
    [NotMapped]
    public string ScreenResolution => $"{ScreenResolutionWidth}x{ScreenResolutionHeight}";
    [StringLength(20)]
    public string SerialNumber { get; set; } = string.Empty;
    [StringLength(20)]
    public string InventarNumber { get; set; } = string.Empty;
    private int NumberCabinet { get; set; }
    public int YearRelease { get; set; }
    public User? User { get; set; }
    public bool IsDecommissioned { get; set; }

    [NotMapped]
    public int Cabinet => (User == null) ? NumberCabinet : User.NumberCabinet;
}