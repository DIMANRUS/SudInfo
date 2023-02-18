namespace SudInfo.EfDataAccessLibrary.Models;
public class ServerRack : BaseModel
{
    public int Position { get; set; }
    [StringLength(20)]
    public required string InventarNumber { get; set; }

    public List<Server> Servers { get; set; } = new();
    public List<Monitor> Monitors { get; set; } = new();
}