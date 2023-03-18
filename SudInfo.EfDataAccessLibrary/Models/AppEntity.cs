namespace SudInfo.EfDataAccessLibrary.Models;
public class AppEntity : BaseModel
{
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(20, ErrorMessage = Const.LengthMore2, MinimumLength = 2)]
    public string? Name { get; set; }
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(20, ErrorMessage = Const.LengthMore2, MinimumLength = 2)]
    public string? Version { get; set; }

    public ICollection<Computer> Computers { get; set; } = new List<Computer>();
}