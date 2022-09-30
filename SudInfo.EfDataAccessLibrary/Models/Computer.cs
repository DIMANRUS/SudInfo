namespace SudInfo.EFDataAccessLibrary.Models;
public class Computer : BaseModel
{
    [StringLength(15)]
    public string Ip { get; set; }
    public Oses OS { get; set; } = Oses.Windows7;
    public Employee? Employee { get; set; }
}

public enum Oses
{
    Windows7, Windows10, Ubuntu
}