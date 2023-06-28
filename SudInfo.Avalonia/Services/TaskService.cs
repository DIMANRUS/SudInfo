namespace SudInfo.Avalonia.Services;

public class TaskService : BaseService
{
    public static async Task<IReadOnlyList<TaskEntity>> GetTasks()
    {
        using SudInfoDbContext context = new();
        var tasks = await context.Tasks
            .AsNoTracking()
            .ToListAsync();
        return tasks;
    }
    public static async Task<Result> CompleteTask(int id)
    {
        try
        {
            using SudInfoDbContext context = new();
            var task = await context.Tasks.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Task not found");
            context.Tasks.Remove(task);
            await context.SaveChangesAsync();
            return new()
            {
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