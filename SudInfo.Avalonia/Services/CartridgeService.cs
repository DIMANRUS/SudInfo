namespace SudInfo.Avalonia.Services;

public class CartridgeService : BaseService
{
    #region Get Methods
    public async Task<Result<Cartridge>> GetCartridge(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var cartridge = await applicationDBContext.Cartridges.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return cartridge == null
                ? throw new Exception("Cartridge not Found")
                : new()
                {
                    Success = true,
                    Object = cartridge
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
    public async Task<Result<IReadOnlyList<Cartridge>>> GetCartridges()
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var cartridges = await applicationDBContext.Cartridges.AsNoTracking().ToListAsync();
            return new()
            {
                Success = true,
                Object = cartridges
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
    #endregion
    public async Task<Result> Add(string cartridgeName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(cartridgeName))
                throw new("Cartdige name not valid");
            using SudInfoDbContext applicationDBContext = new();
            await applicationDBContext.Cartridges.AddAsync(new()
            {
                Name = cartridgeName
            });
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
                Message = ex.Message
            };
        }
    }
    public async Task<Result> Remove(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var cartridge = await applicationDBContext.Cartridges.FirstOrDefaultAsync(x => x.Id == id);
            applicationDBContext.Cartridges.Remove(cartridge);
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
                Message = ex.Message
            };
        }
    }
    public async Task<Result> Update(params Cartridge[] cartridges)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            applicationDBContext.Cartridges.UpdateRange(cartridges);
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
                Message = ex.Message
            };
        }
    }
}