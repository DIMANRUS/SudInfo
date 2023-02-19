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
    [NotMapped]
    public string Icon => Type switch
    {
        PeripheryType.Мышь => "avares://SudInfo.Avalonia/Assets/Images/mouse.png",
        PeripheryType.Наушкники => "avares://SudInfo.Avalonia/Assets/Images/headphones.png",
        PeripheryType.Микрофон => "avares://SudInfo.Avalonia/Assets/Images/microphone.png",
        PeripheryType.Колонки => "avares://SudInfo.Avalonia/Assets/Images/sound.png",
        _ => "avares://SudInfo.Avalonia/Assets/Images/keyboard.png"
    };
}
public enum PeripheryType
{
    Мышь, Клавиатура, Микрофон, Сканер, Колонки, Наушкники
}