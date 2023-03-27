namespace SudInfo.EfDataAccessLibrary.Models;
public class Contact : BaseModel
{
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(50, ErrorMessage = Const.LengthMore2, MinimumLength = 2)]
    public string? Name { get; set; }

    [RegularExpression("[7-8][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]", ErrorMessage = Const.NotValidPhoneMessage)]
    [StringLength(11)]
    public string? Phone { get; set; }

    [StringLength(50, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? Email { get; set; }

    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(200, ErrorMessage = Const.LengthMore2, MinimumLength = 2)]
    public string? Description { get; set; }
}