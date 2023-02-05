namespace SudInfo.EfDataAccessLibrary.Models.BaseModels;
public abstract class BaseModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}