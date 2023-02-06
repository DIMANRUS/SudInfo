namespace SudInfo.EfDataAccessLibrary.Models;
public class Periphery : BaseModel
{
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    public PeripheryType Type { get; set; }
    [StringLength(30)]
    public string SerialNumber { get; set; } = string.Empty;
    public string? InventarNumber { get; set; } = string.Empty;
    public Computer? Computer { get; set; }
}
public enum PeripheryType
{
    Мышь, Клавиатура, Микрофон, Сканер, Колонки, Наушкники
}