namespace SudInfo.EfDataAccessLibrary.Models;

public class Rutoken : BaseModel
{
    [XLColumn(Header = "Серийный номер")]
    [StringLength(30)]
    public string? SerialNumber { get; set; }

    [XLColumn(Header = "Дата окончания действия сертификата")]
    public DateTime? EndDateCertificate { get; set; } = DateTime.Now.AddYears(1);

    public User? User { get; set; }
}