namespace SudInfo.Avalonia.Services;
public class UsersService : IUsersService
{
    #region Get Methods Realization
    public async Task<TaskResult<User>> GetUserById(int userId)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            var user = await applicationDBContext.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                throw new Exception("User not Found");
            return new()
            {
                Success = true,
                Result = user
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
    public async Task<TaskResult<List<User>>> GetUsers()
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            var users = await applicationDBContext.Users.ToListAsync();
            return new()
            {
                Success = true,
                Result = users
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
    #endregion

    public async Task<TaskResult> RemoveUserById(int userId)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            var user = await applicationDBContext.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                throw new Exception("User not found");
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
    public async Task<TaskResult> SaveUser(User user)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            applicationDBContext.Update(user);
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
    public async Task<TaskResult> AddUser(User user)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            await applicationDBContext.AddAsync(user);
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