namespace SudInfo.Avalonia.Interfaces;
public interface IPrintersService
{
    Task<TaskResult<List<Printer>>> GetPrinters();
    Task<TaskResult<Printer>> GetPrinterById(int id);
    Task<TaskResult> SavePrinter(Printer printer);
    Task<TaskResult> AddPrinter(Printer printer);
    Task<TaskResult> RemovePrinterById(int id);
}