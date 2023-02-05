namespace SudInfo.Avalonia.Services;
public class PeripheryService : IPeripheryService
{
    #region Get Methods Realization
    public async Task<Result<Periphery>> GetPeripheryById(int id)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            var periphery = await applicationDBContext.Peripheries.Include(x => x.Computer).ThenInclude(x=>x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (periphery == null)
                throw new Exception("Пепреферия не найдена");
            return new()
            {
                Success = true,
                Object = periphery
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
    public async Task<Result<List<Periphery>>> GetPeripheryList()
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            var peripheries = await applicationDBContext.Peripheries.Include(x => x.Computer).ThenInclude(x => x.User).ToListAsync();
            return new()
            {
                Success = true,
                Object = peripheries
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

    public async Task<Result> AddPeriphery(Periphery periphery)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            if (periphery.Computer != null)
            {
                periphery.Computer = await applicationDBContext.Computers.SingleOrDefaultAsync(x => x.Id == periphery.Computer.Id);
            }
            await applicationDBContext.Peripheries.AddAsync(periphery);
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
    public async Task<Result> RemovePeripheryById(int id)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            var periphery = await applicationDBContext.Peripheries.FirstOrDefaultAsync(x => x.Id == id);
            if (periphery == null)
                throw new Exception("Переферия не найдена");
            applicationDBContext.Peripheries.Remove(periphery);
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
    public async Task<Result> UpdatePeriphery(Periphery periphery)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            applicationDBContext.Peripheries.Update(periphery);
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