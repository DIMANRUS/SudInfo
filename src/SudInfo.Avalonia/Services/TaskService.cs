namespace SudInfo.Avalonia.Services;

public class TaskService : BaseService<TaskEntity>
{
    #region Ctors

    public TaskService(SudInfoDatabaseContext context) : base(context)
    {
    }

    #endregion

    public async Task<IReadOnlyCollection<TaskEntity>> Get()
    {
        var tasks = await context.Tasks.AsNoTracking()
                                                     .ToListAsync();
        return tasks;
    }

    public async Task<Result> CompleteTask(int id)
    {
        try
        {
            var task = await context.Tasks.AsNoTracking()
                                          .FirstAsync(x => x.Id == id);
            context.Tasks.Remove(task);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new(message: ex.Message);
        }
    }
}