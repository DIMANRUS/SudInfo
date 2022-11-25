namespace SudInfo.EfDataAccessLibrary.Models;
public class Employee : BaseModel
{
    [StringLength(20)]
    public string FirstName { get; set; }
    [StringLength(20)]
    public string LastName { get; set; }
    [StringLength(20)]
    public string? MiddleName { get; set; }
    public byte NumberCabinet { get; set; }
    [StringLength(11)]
    public string PersonalPhone { get; set; }
    [StringLength(11)]
    public string WorkPhone { get; set; }
    [StringLength(3)]
    public string PhoneLocal { get; set; }
}