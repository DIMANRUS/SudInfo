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
    public async Task<Result> CompleteTask(int id)
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

    public async Task<Result> AddTask(TaskEntity task)
    {
        try
        {
            using SudInfoDbContext context = new();
            await context.Tasks.AddAsync(task);
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