namespace SudInfo.Avalonia.Services;
public class ComputersService : IComputersService
{
    #region Private Variables
    private readonly ApplicationDBContext _applicationDbContext;
    #endregion
    
    #region Constructors
    public ComputersService(ApplicationDBContext applicationDBContext)
    {
        _applicationDbContext = applicationDBContext;
    } 
    #endregion

    public async Task<TaskResult> AddComputer(Computer computer)
    {
        try
        {
            await _applicationDbContext.Computers.AddAsync(computer);
            await _applicationDbContext.SaveChangesAsync();
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

    #region Get Computer/s Realization
    public async Task<TaskResult<Computer>> GetComputerById(int id)
    {
        try
        {
            var computer = await _applicationDbContext.Computers.Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == id);
            if (computer == null)
                throw new Exception("Computer not Found");
            return new()
            {
                Success = true,
                Result = computer
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

    public async Task<TaskResult<List<Computer>>> GetComputers()
    {
        try
        {
            var computers = await _applicationDbContext.Computers.Include(x => x.Employee).ToListAsync();
            return new()
            {
                Success = true,
                Result = computers
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

    public async Task<TaskResult> RemoveComputerById(int id)
    {
        try
        {
            var computer = await _applicationDbContext.Computers.FirstOrDefaultAsync(x => x.Id == id);
            if (computer == null)
                throw new Exception("Computer not found");
            _applicationDbContext.Computers.Remove(computer);
            await _applicationDbContext.SaveChangesAsync();
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

    public async Task<TaskResult> SaveComputer(Computer computer)
    {
        try
        {
            _applicationDbContext.Computers.Update(computer);
            await _applicationDbContext.SaveChangesAsync();
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