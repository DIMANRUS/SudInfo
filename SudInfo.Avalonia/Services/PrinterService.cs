namespace SudInfo.Avalonia.Services;
public class PrinterService
{
    #region Get Methods
    public async Task<Result<Printer>> GetPrinterById(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var printer = await applicationDBContext.Printers
                .AsNoTracking()
                .Include(x => x.Computer)
                .FirstOrDefaultAsync(x => x.Id == id);
            return new()
            {
                Success = true,
                Object = printer
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
    public async Task<Result<List<Printer>>> GetPrinters()
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var printers = await applicationDBContext.Printers
                .AsNoTracking()
                .Include(x => x.Computer)
                .ThenInclude(x => x.User)
                .ToListAsync();
            return new()
            {
                Success = true,
                Object = printers
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

    public async Task<Result> UpdatePrinter(Printer printer)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            applicationDBContext.Printers.Update(printer);
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
    public async Task<Result> AddPrinter(Printer printer)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            if (printer.Computer != null)
            {
                printer.Computer = await applicationDBContext.Computers.SingleOrDefaultAsync(x => x.Id == printer.Computer.Id);
            }
            await applicationDBContext.Printers.AddAsync(printer);
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
    public async Task<Result> RemovePrinterById(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var printer = await applicationDBContext.Printers.FirstOrDefaultAsync(x => x.Id == id);
            if (printer == null)
                throw new Exception("Printer not found");
            applicationDBContext.Printers.Remove(printer);
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