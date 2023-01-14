namespace SudInfo.Avalonia.Services;
public class ComputersService : IComputersService
{
    #region Get Methods Realization
    public async Task<TaskResult<Computer>> GetComputerById(int id)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            var computer = await applicationDBContext.Computers.Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == id);
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
            using ApplicationDBContext applicationDBContext = new();
            var computers = await applicationDBContext.Computers.Include(x => x.Employee).ToListAsync();
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

    public async Task<TaskResult> AddComputer(Computer computer)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            await applicationDBContext.Computers.AddAsync(computer);
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
    public async Task<TaskResult> RemoveComputerById(int id)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            var computer = await applicationDBContext.Computers.FirstOrDefaultAsync(x => x.Id == id);
            if (computer == null)
                throw new Exception("Computer not found");
            applicationDBContext.Computers.Remove(computer);
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
    public async Task<TaskResult> SaveComputer(Computer computer)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            applicationDBContext.Computers.Update(computer);
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