namespace SudInfo.Avalonia.Services;
public class PasswordService : BaseService
{
    public async Task<Result<IReadOnlyList<PasswordEntity>>> GetPasswords()
    {
        try
        {
            using SudInfoDbContext context = new();
            var result = await context.Passwords.AsNoTracking().ToListAsync();
            return new()
            {
                Object = result,
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

    public async Task<Result> RemovePassword(int id)
    {
        try
        {
            using SudInfoDbContext context = new();
            var passwordEntity = await context.Passwords.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Password not found");
            context.Passwords.Remove(passwordEntity);
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
    public async Task<Result<PasswordEntity>> GetPasswordEntity(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var server = await applicationDBContext.Passwords
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Server not Found");
            return new()
            {
                Success = true,
                Object = server
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