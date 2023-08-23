namespace SudInfo.Avalonia.Services;

public class AppService : BaseService<AppEntity>
{
    #region Ctors

    public AppService(SudInfoDatabaseContext context) : base(context)
    {
    }

    #endregion

    #region Get Methods

    public async Task<IReadOnlyCollection<AppEntity>> Get()
    {
        var apps = await context.Apps.AsNoTracking()
                                     .Include(x => x.Computers)
                                     .ThenInclude(x => x.User)
                                     .ToListAsync();
        return apps;
    }

    public async Task<Result<AppEntity>> Get(int id)
    {
        try
        {
            var server = await context.Apps
                .AsNoTracking()
                .Include(x => x.Computers)
                .SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("App not Found");
            return new(server, true);
        }
        catch (Exception ex)
        {
            return new(null, message: ex.Message);
        }
    }

    #endregion

    public async Task<Result> Remove(int id)
    {
        try
        {
            var entity = await context.Apps.AsNoTracking()
                                           .Include(x => x.Computers)
                                           .FirstAsync(x => x.Id == id);
            entity.Computers!.Clear();
            context.Apps.Remove(entity);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new Result(message: ex.Message);
        }
    }

    public async Task<Result> Update(AppEntity entity)
    {
        try
        {
            var app = await context.Apps.Include(x => x.Computers)
                                        .FirstAsync(x => x.Id == entity.Id);
            app.Name = entity.Name;
            app.Version = entity.Version;
            if (entity.Computers!.Count == 0)
            {
                app.Computers = null;
            }
            else
            {
                app.Computers = new List<Computer>();
                foreach (var computer in entity.Computers)
                {
                    app.Computers.Add(await context.Computers.FindAsync(computer.Id));
                }
            }
            context.Update(app);
            await context.SaveChangesAsync();
            return new Result(true);
        }
        catch (Exception ex)
        {
            return new Result(message: ex.Message);
        }
    }

    public override async Task<Result> Add(AppEntity entity)
    {
        try
        {
            var computers = entity.Computers;
            entity.Computers = new List<Computer>();
            foreach (var computer in computers)
            {
                entity.Computers.Add(await context.Computers.FindAsync(computer.Id));
            }
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return new Result(true);
        }
        catch (Exception ex)
        {
            return new Result(message: ex.Message);
        }
    }
}