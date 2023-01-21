namespace SudInfo.Avalonia.Services;
public class PrintersService : IPrintersService
{
    #region Get Methods
    public async Task<TaskResult<Printer>> GetPrinterById(int id)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            var printer = await applicationDBContext.Printers.Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == id);
            return new()
            {
                Success = true,
                Result = printer
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
    public async Task<TaskResult<List<Printer>>> GetPrinters()
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            var printers = await applicationDBContext.Printers.Include(x => x.Employee).ToListAsync();
            return new()
            {
                Success = true,
                Result = printers
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
    public async Task<TaskResult> SavePrinter(Printer printer)
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
    public async Task<TaskResult> AddPrinter(Printer printer)
    {
        try
        {
            using ApplicationDBContext applicationDBContext = new();
            if (printer.Employee != null)
            {
                printer.Employee = await applicationDBContext.Users.SingleOrDefaultAsync(x => x.Id == printer.Employee.Id);
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
    public async Task<TaskResult> RemovePrinterById(int id)
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