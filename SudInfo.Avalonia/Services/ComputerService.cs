namespace SudInfo.Avalonia.Services;
public class ComputerService : BaseService
{
    #region Get Methods Realization
    public async Task<Result<Computer>> GetComputerById(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var computer = await applicationDBContext.Computers.AsNoTracking().Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (computer == null)
                throw new Exception("Computer not Found");
            return new()
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
    public async Task<Result<List<Computer>>> GetComputers()
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var computers = await applicationDBContext.Computers.AsNoTracking().Include(x => x.User).ToListAsync();
            return new()
            {
                Success = true,
                Object = computers
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
    public async Task<Result<List<Computer>>> GetComputerNames()
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var computerNames = await applicationDBContext.Computers.AsNoTracking().Select(x => new Computer() { Name = x.Name, Id = x.Id }).ToListAsync();
            return new()
            {
                Object = computerNames,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Message = ex.Message,
                Success = false
            };
        }
    }
    #endregion

    public async Task<Result> AddComputer(Computer computer)
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
    public async Task<Result> RemoveComputerById(int id)
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