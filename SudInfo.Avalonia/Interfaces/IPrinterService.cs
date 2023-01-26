namespace SudInfo.Avalonia.Interfaces;
public interface IPrinterService
{
    Task<Result<List<Printer>>> GetPrinters();
    Task<Result<Printer>> GetPrinterById(int id);
    Task<Result> UpdatePrinter(Printer printer);
    Task<Result> AddPrinter(Printer printer);
    Task<Result> RemovePrinterById(int id);
}