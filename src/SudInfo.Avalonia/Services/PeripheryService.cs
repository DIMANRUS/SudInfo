namespace SudInfo.Avalonia.Services;

public class PeripheryService : BaseService<Periphery>
{
    #region Ctors

    public PeripheryService(SudInfoDatabaseContext context) : base(context) { }

    #endregion

    #region Get Methods

    public async Task<Result<Periphery>> Get(int id)
    {
        try
        {
            var periphery = await context.Peripheries.Include(x => x.Computer)
                                                     .ThenInclude(x => x.User)
                                                     .FirstOrDefaultAsync(x => x.Id == id);
            return periphery == null
                ? throw new Exception("Пепреферия не найдена")
                : new(periphery, true);
        }
        catch (Exception ex)
        {
            return new(null, message: ex.Message);
        }
    }

    public async Task<IReadOnlyCollection<Periphery>> Get()
    {

        var peripheries = await context.Peripheries.Include(x => x.Computer)
                                                   .ThenInclude(x => x.User)
                                                   .ToListAsync();
        return peripheries;
    }

    #endregion

    public override async Task<Result> Add(Periphery periphery)
    {
        try
        {
            if (periphery.Computer != null)
            {
                periphery.Computer = await context.Computers.SingleOrDefaultAsync(x => x.Id == periphery.Computer.Id);
            }
            await context.Peripheries.AddAsync(periphery);
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
            var periphery = await context.Peripheries.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Переферия не найдена");
            context.Peripheries.Remove(periphery);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new(message: ex.Message);
        }
    }
}