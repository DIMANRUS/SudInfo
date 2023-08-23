namespace SudInfo.Avalonia.Services;

public class PasswordService : BaseService<PasswordEntity>
{
    #region Ctors

    public PasswordService(SudInfoDatabaseContext context) : base(context)
    {
    }

    #endregion

    #region Get Methods

    public async Task<IReadOnlyCollection<PasswordEntity>> Get()
    {
        var passwords = await context.Passwords.AsNoTracking().ToListAsync();
        return passwords;
    }

    public async Task<Result<PasswordEntity>> Get(int id)
    {
        try
        {
            var server = await context.Passwords
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Server not Found");
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
            var passwordEntity = await context.Passwords.AsNoTracking()
                                                        .FirstAsync(x => x.Id == id);
            context.Passwords.Remove(passwordEntity);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new(message: ex.Message);
        }
    }
}