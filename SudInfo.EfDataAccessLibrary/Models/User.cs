namespace SudInfo.EfDataAccessLibrary.Models;
public class User : BaseModel
{
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(20,MinimumLength =2, ErrorMessage = Const.LengthMore2)]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(20, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? LastName { get; set; }
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(20, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? MiddleName { get; set; }
    [NotMapped]
    public string FIO => LastName + EfDataAccessLibrary.Const.EmptyWithSpaceString + FirstName + EfDataAccessLibrary.Const.EmptyWithSpaceString + MiddleName;
    public int NumberCabinet { get; set; }
    [StringLength(11)]
    public string? PersonalPhone { get; set; }
    [StringLength(11)]
    public string? WorkPhone { get; set; }
    [StringLength(3)]
    public string? PhoneLocal { get; set; }

    public List<Computer> Computers { get; set; } = new();
}