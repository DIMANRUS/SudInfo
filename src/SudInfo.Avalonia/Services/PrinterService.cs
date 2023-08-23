namespace SudInfo.Avalonia.Services;

public class PrinterService : BaseService<Printer>
{
    #region Ctors

    public PrinterService(SudInfoDatabaseContext context) : base(context)
    {
    }

    #endregion

    #region Get Methods

    public async Task<Result<Printer>> Get(int id)
    {
        try
        {
            var printer = await context.Printers.AsNoTracking()
                                                .Include(x => x.Computer)
                                                .FirstOrDefaultAsync(x => x.Id == id);
            return printer == null
                ? throw new Exception("Принтер не найден.")
                : new(printer, true);
        }
        catch (Exception ex)
        {
            return new(null, message: ex.Message);
        }
    }

    public async Task<IReadOnlyCollection<Printer>> Get()
    {
        var printers = await context.Printers.AsNoTracking()
                                             .Include(x => x.Computer)
                                             .ThenInclude(x => x.User)
                                             .ToListAsync();
        return printers;
    }

    #endregion

    public override async Task<Result> Add(Printer printer)
    {
        try
        {
            if (printer.Computer != null)
            {
                printer.Computer = await context.Computers.AsNoTracking()
                                                          .FirstAsync(x => x.Id == printer.Computer.Id);
            }
            await context.Printers.AddAsync(printer);
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
            var printer = await context.Printers.AsNoTracking()
                                                .FirstAsync(x => x.Id == id) ?? throw new Exception("Printer not found");
            context.Printers.Remove(printer);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new(message: ex.Message);
        }
    }
}