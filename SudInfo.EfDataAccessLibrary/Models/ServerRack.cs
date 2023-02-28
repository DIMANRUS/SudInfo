namespace SudInfo.EfDataAccessLibrary.Models;

[Index(nameof(Position), IsUnique = true)]
public class ServerRack : BaseModel
{
    public int Position { get; set; } = 1;
    [StringLength(20)]
    public string InventarNumber { get; set; } = null!;
    public List<Server> Servers { get; set; } = new();
}