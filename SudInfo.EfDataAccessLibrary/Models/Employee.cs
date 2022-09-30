namespace SudInfo.EFDataAccessLibrary.Models;
public class Employee : BaseModel
{
    [StringLength(20)]
    public string FirstName { get; set; }
    [StringLength(20)]
    public string LastName { get; set; }
    [StringLength(20)]
    public string? MiddleName { get; set; }
    public byte NumberCabinet { get; set; }
    [StringLength(10)]
    public string Phone { get; set; }
    public byte PhoneLocal { get; set; }
}