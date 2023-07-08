namespace SudInfo.Avalonia.Services;

public class ComputerService : BaseService
{
    #region Get Methods

    public static async Task<Result<Computer>> GetComputerById(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var computer = await applicationDBContext.Computers
                                                     .AsNoTracking()
                                                     .Include(x => x.User)
                                                     .FirstOrDefaultAsync(x => x.Id == id);
            return computer == null
                ? throw new Exception("Computer not Found")
                : new()
                {
                    Success = true,
                    Object = computer
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
    public static async Task<IReadOnlyList<Computer>> GetComputers()
    {
        using SudInfoDbContext applicationDBContext = new();
        var computers = await applicationDBContext.Computers.AsNoTracking().Include(x => x.User).ToListAsync();
        return computers;
    }
    public static async Task<IReadOnlyList<Computer>> GetComputerNamesWithUser()
    {
        using SudInfoDbContext applicationDBContext = new();
        var computerNames = await applicationDBContext.Computers
                                                      .AsNoTracking()
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

    public static async Task<Result> AddComputer(Computer computer)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            if (computer.User != null)
            {
                computer.User = await applicationDBContext.Users.SingleOrDefaultAsync(x => x.Id == computer.User.Id);
            }
            await applicationDBContext.AddAsync(computer);
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
    public static async Task<Result> RemoveComputerById(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var computer = await applicationDBContext.Computers.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Computer not found");
            applicationDBContext.Remove(computer);
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