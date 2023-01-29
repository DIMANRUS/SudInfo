namespace SudInfo.EfDataAccessLibrary.Models;
public class Periphery : BaseModel
{
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    [StringLength(30)]
    public string SerialNumber { get; set; } = string.Empty;
    public string? InventarNumber { get; set; } = string.Empty;
    public User? User { get; set; }
    public bool IsDecommissioned { get; set; }
}
public enum PeripheryType
{
    Мышь, Клавиатура, Микрофон, Сканер, Колонки, Наушкники
}