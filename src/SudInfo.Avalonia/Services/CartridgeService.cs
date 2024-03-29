using DocumentFormat.OpenXml.Vml.Office;

namespace SudInfo.Avalonia.Services;

public class CartridgeService : BaseService<Cartridge>
{
    #region Ctors

    public CartridgeService(SudInfoDatabaseContext context) : base(context)
    {
    }

    #endregion

    #region Get Methods

    public async Task<Result<Cartridge>> Get(int id)
    {
        try
        {
            var cartridge = await context.Cartridges.FirstAsync(x => x.Id == id);
            return new(cartridge, true);
        }
        catch (Exception ex)
        {
            return new(null, message: ex.Message);
        }
    }

    public async Task<IReadOnlyCollection<Cartridge>> Get()
    {
        var cartridges = await context.Cartridges.AsNoTracking()
                                                 .ToListAsync();
        return cartridges;
    }

    #endregion

    public async Task<Result> Remove(int id)
    {
        try
        {
            var cartridge = await context.Cartridges.AsNoTracking()
                                                    .FirstAsync(x => x.Id == id);
            context.Entry(cartridge).State = EntityState.Deleted;
            context.Cartridges.Remove(cartridge);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new(message: ex.Message);
        }
    }
}