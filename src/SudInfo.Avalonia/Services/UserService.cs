namespace SudInfo.Avalonia.Services;

public class UserService : BaseService<User>
{
    #region Ctors

    public UserService(SudInfoDatabaseContext context) : base(context)
    {
    }

    #endregion

    #region Get Methods

    public async Task<Result<User>> Get(int id)
    {
        try
        {
            var user = await context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
            return user == null ? throw new Exception("User not Found") : new(user, true);
        }
        catch (Exception ex)
        {
            return new(null, message: ex.Message);
        }
    }

    public async Task<IReadOnlyCollection<User>> Get()
    {
        var users = await context.Users
            .AsNoTracking()
            .Include(x => x.Computers)
            .ThenInclude(x => x.Monitors)
            .Include(x => x.Computers)
            .ThenInclude(x => x.Printers)
            .Include(x => x.Computers)
            .ThenInclude(x => x.Peripheries)
            .ToListAsync();
        return users;
    }

    #endregion

    public async Task<Result> Remove(int id)
    {
        try
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("User not found");
            context.Remove(user);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new(message: ex.Message);
        }
    }
}