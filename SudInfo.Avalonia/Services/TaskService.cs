namespace SudInfo.Avalonia.Services;
public class TaskService
{
    public async Task<Result<IReadOnlyList<TaskEntity>>> GetTasks()
    {
        try
        {
            using SudInfoDbContext context = new();
            var tasksResult = await context.Tasks.AsNoTracking().ToListAsync();
            return new()
            {
                Object = tasksResult,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Message = ex.Message
            };
        }
    }
}