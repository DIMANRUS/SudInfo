namespace SudInfo.EfDataAccessLibrary.Modelsl;
public class Periphery : BaseModel
{
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    [StringLength(30)]
    public string SerialNumber { get; set; } = string.Empty;
    public string? InventarNumber { get; set; } = string.Empty;
}
public enum PeripheryType
{
    Mouse, Keyboard, Microphone, Scaner, Speaker
}