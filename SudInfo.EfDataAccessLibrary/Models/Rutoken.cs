namespace SudInfo.EfDataAccessLibrary.Models;
public class Rutoken : BaseModel
{
    [StringLength(30)]
    public string? SerialNumber { get; set; }
    public DateTime? EndDateCertificate { get; set; } = DateTime.Now.AddYears(1);
    public User? User { get; set; }
}