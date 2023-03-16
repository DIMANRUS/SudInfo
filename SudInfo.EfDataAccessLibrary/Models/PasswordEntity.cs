namespace SudInfo.EfDataAccessLibrary.Models;
public class PasswordEntity : BaseModel
{
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(200, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? Description { get; set; }
    [StringLength(50, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? Login { get; set; }
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(50)]
    public string? Password { get; set; }
}