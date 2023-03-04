namespace SudInfo.EfDataAccessLibrary.Models;
public class Server : BaseModel
{
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(20, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? Name { get; set; }
    [StringLength(20, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? SerialNumber { get; set; }
    [StringLength(20, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? InventarNumber { get; set; }
    public int? PosiitionInServerRack { get; set; }
    [RegularExpression(Const.Ip4RegularExpression, ErrorMessage = Const.NotValidIp4Message)]
    [StringLength(20)]
    public string? IpAddress { get; set; }
    public int? ServerRackId { get; set; }
    public ServerRack? ServerRack { get; set; }
    public ServerOperatingSystem OperatingSystem { get; set; }
}

public enum ServerOperatingSystem
{
    WindowsServer2012, WindowsServer2003, WindowsServer2008, WindowsServer2016, WindowsServer2019, UbuntuServer, ESXi
}