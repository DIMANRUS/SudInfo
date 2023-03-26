namespace SudInfo.EfDataAccessLibrary.Models;

[Index(nameof(Name), IsUnique = true)]
public class Cartridge : BaseModel
{
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(50, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? Name { get; set; }
    public int Remains { get; set; }
}