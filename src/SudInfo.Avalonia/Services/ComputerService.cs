namespace SudInfo.Avalonia.Services;

public class ComputerService : BaseService<Computer>
{
    #region Ctors

    public ComputerService(SudInfoDatabaseContext context) : base(context)
    {
    }

    #endregion

    #region Get Methods

    public async Task<Result<Computer>> Get(int id)
    {
        try
        {
            var computer = await context.Computers.AsNoTracking()
                                                  .Include(x => x.User)
                                                  .FirstOrDefaultAsync(x => x.Id == id);
            return computer == null
                ? throw new Exception("Computer not Found")
                : new(computer, true);
        }
        catch (Exception ex)
        {
            return new(null, message: ex.Message);
        }
    }

    public async Task<IReadOnlyCollection<Computer>> Get()
    {
        var computers = await context.Computers.AsNoTracking()
                                               .Include(x => x.User)
                                               .ToListAsync();
        return computers;
    }

    public async Task<IReadOnlyCollection<Computer>> GetComputerNamesWithUser()
    {
        var computerNames = await context.Computers.AsNoTracking()
                                                   .Include(x => x.User)
                                                   .Select(x => new Computer()
                                                   {
                                                       Name = x.Name,
                                                       Id = x.Id,
                                                       User = x.User
                                                   })
                                                   .ToListAsync();
        return computerNames;
    }

    #endregion

    public override async Task<Result> Add(Computer computer)
    {
        try
        {
            if (computer.User != null)
            {
                computer.User = await context.Users.SingleOrDefaultAsync(x => x.Id == computer.User.Id);
            }
            await context.AddAsync(computer);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new(message: ex.Message);
        }
    }

    public async Task<Result> Remove(int id)
    {
        try
        {
            var computer = await context.Computers.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Computer not found");
            context.Remove(computer);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new(message: ex.Message);
        }
    }
}