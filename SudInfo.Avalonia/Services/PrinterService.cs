namespace SudInfo.Avalonia.Services;
public class PrinterService : IPrinterService
{
    #region Get Methods
    public async Task<Result<Printer>> GetPrinterById(int id)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            var printer = await applicationDBContext.Printers.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
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
            using ApplicationDBContext applicationDBContext = new();
            var printers = await applicationDBContext.Printers.Include(x => x.User).ToListAsync();
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
            using ApplicationDBContext applicationDBContext = new();
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
            using ApplicationDBContext applicationDBContext = new();
            if (printer.User != null)
            {
                printer.User = await applicationDBContext.Users.SingleOrDefaultAsync(x => x.Id == printer.User.Id);
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
            using ApplicationDBContext applicationDBContext = new();
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