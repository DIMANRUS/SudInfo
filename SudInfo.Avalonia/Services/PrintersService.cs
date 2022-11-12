namespace SudInfo.Avalonia.Services;
public class PrintersService : IPrintersService
{
    private readonly ApplicationDBContext _applicationDbContext;
    public PrintersService(ApplicationDBContext applicationDBContext)
    {
        _applicationDbContext = applicationDBContext;
    }

    public async Task<TaskResult<Printer>> GetPrinterById(int id)
    {
        try
        {
            var printer = await _applicationDbContext.Printers.Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == id);
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
            var printers = await _applicationDbContext.Printers.Include(x => x.Employee).ToListAsync();
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
    public async Task<TaskResult> SavePrinter(Printer printer)
    {
        try
        {
            _applicationDbContext.Printers.Update(printer);
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
    public async Task<TaskResult> AddPrinter(Printer printer)
    {
        try
        {
            await _applicationDbContext.Printers.AddAsync(printer);
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