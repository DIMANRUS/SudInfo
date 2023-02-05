namespace SudInfo.EfDataAccessLibrary.Models;
public class User : BaseModel
{
    [StringLength(20)]
    public string FirstName { get; set; } = string.Empty;
    [StringLength(20)]
    public string LastName { get; set; } = string.Empty;
    [StringLength(20)]
    public string? MiddleName { get; set; }
    [NotMapped]
    public string FIO => LastName + Constants.EmptyWithSpaceString + FirstName + Constants.EmptyWithSpaceString + MiddleName;
    public int NumberCabinet { get; set; }
    [StringLength(11)]
    public string? PersonalPhone { get; set; }
    [StringLength(11)]
    public string? WorkPhone { get; set; }
    [StringLength(3)]
    public string? PhoneLocal { get; set; }

    public List<Computer> Computers { get; set; } = new();
    public List<Monitor> Monitors { get; set; } = new();
}