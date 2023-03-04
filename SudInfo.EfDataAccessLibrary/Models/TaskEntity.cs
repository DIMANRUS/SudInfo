namespace SudInfo.EfDataAccessLibrary.Models;
public class TaskEntity : BaseModel
{
    [Required(ErrorMessage = Const.FieldRequired)]
    [StringLength(200, MinimumLength = 2, ErrorMessage = Const.LengthMore2)]
    public string? Description { get; set; }
    public DateTime ReminderTime { get; set; } = DateTime.Now;
}