namespace SudInfo.Avalonia.Services;

public class UserService : BaseService
{
    #region Get Methods Realization

    public static async Task<Result<User>> GetUserById(int userId)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var user = await applicationDBContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                throw new Exception("User not Found");
            return new()
            {
                Success = true,
                Object = user
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    public static async Task<IReadOnlyList<User>> GetUsers()
    {
        using SudInfoDbContext applicationDBContext = new();
        var users = await applicationDBContext.Users
            .AsNoTracking()
            .ToListAsync();
        return users;
    }
    public static async Task<IReadOnlyList<User>> GetUsersWithComputers()
    {
        using SudInfoDbContext applicationDBContext = new();
        var users = await applicationDBContext.Users
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

    public static async Task<Result> RemoveUserById(int userId)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var user = await applicationDBContext.Users.SingleOrDefaultAsync(x => x.Id == userId) ?? throw new Exception("User not found");
            applicationDBContext.Remove(user);
            await applicationDBContext.SaveChangesAsync();
            return new()
            {
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
}