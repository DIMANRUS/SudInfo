namespace SudInfo.EfDataAccessLibrary.Models;
public class Server : BaseModel
{
    [StringLength(20)]
    public required string SerialNumber { get; set; }
    [StringLength(20)]
    public required string InventarNumber { get; set; }
    public int? PosiitionInServerRack { get; set; }
    public ServerRack? ServerRack { get; set; }
}