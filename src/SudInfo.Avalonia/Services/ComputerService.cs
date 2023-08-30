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
            var computer = await context.Computers.Include(x => x.User)
                                                  .FirstAsync(x => x.Id == id);
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

    #endregion

    public override async Task<Result> Add(Computer computer)
    {
        try
        {
            if (computer.User != null)
            {
                computer.User = await context.Users.AsNoTracking()
                                                   .SingleOrDefaultAsync(x => x.Id == computer.User.Id);
            }
            context.Entry(computer).State = EntityState.Added;
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
            var computer = await context.Computers.AsNoTracking()
                                                          .SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Computer not found");
            context.Entry(computer).State = EntityState.Deleted;
            context.Remove(computer);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new(message: ex.Message);
        }
    }

    /*   public override async Task<Result> Update(Computer computer)
       {
           try
           {
               var computerFromDatabase = await context.Computers.Include(x => x.User).FirstAsync(x => x.Id == computer.Id);
               computerFromDatabase.User = computer.User;
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
       }*/
}