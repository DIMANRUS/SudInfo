namespace SudInfo.EfDataAccessLibrary.Models;

public class Cartridge : BaseModel
{
    [XLColumn(Header = "Название")]
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(50, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? Name { get; set; }

    [XLColumn(Header = "Остаток (шт.)")]
    public int Remains { get; set; } = 1;

    [XLColumn(Header = "Тип")]
    public CartridgeType Type { get; set; } = CartridgeType.Новый;
}

public enum CartridgeType
{
    Новый, Заправка, Утилизация
}