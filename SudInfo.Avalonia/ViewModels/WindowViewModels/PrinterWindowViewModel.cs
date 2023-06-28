using SudInfo.Avalonia.Services;

namespace SudInfo.Avalonia.ViewModels.WindowViewModels;

public class PrinterWindowViewModel : BaseViewModel
{
    #region Services

    private readonly PrinterService _printersService;
    private readonly DialogService _dialogService;
    private readonly ComputerService _computerService;

    #endregion

    #region Properties

    [Reactive]
    public Printer Printer { get; set; } = new();

    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить принтер";

    [Reactive]
    public bool IsButtonVisible { get; set; }

    public bool IsComputer { get; set; }

    #endregion

    #region Public Methods

    public async void InitializationData(WindowType windowType, int? id = null)
    {
        _windowType = windowType;
        if (windowType != WindowType.View)
            IsButtonVisible = true;
        if (id != null)
        {
            if (windowType != WindowType.View)
            {
                SaveButtonText = "Сохранить принтер";
            }
            var printerResult = await PrinterService.GetPrinterById(id.GetValueOrDefault());
            if (!printerResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получени компьютера! Ошибка: {printerResult.Message}", true, icon: Icon.Error);
                return;
            }
            Printer = printerResult.Object;
        }
    }
    public async Task SavePrinter()
    {
        if (!ValidationModel(Printer))
            return;
        if (!IsComputer)
            Printer.Computer = null;
        Result printerResult = _windowType switch
        {
            WindowType.Add => await PrinterService.AddPrinter(Printer),
            _ => await _printersService.Update(Printer)
        };
        if (!printerResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", printerResult.Message, icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Сообщение", "Успешно!", true, icon: Icon.Success);
    }
    public async Task LoadComputers()
    {
        var computersResult = await ComputerService.GetComputerNames();
        Computers = computersResult;
    }

    #endregion

    #region Private Fields

    private WindowType _windowType;

    #endregion

    #region Collections
    public static IEnumerable<PrinterType> PrinterTypes => Enum.GetValues(typeof(PrinterType)).Cast<PrinterType>();

    [Reactive]
    public IEnumerable<Computer>? Computers { get; private set; }
    #endregion

    #region Constructors

    public PrinterWindowViewModel(PrinterService printersService, DialogService dialogService, ComputerService computerService)
    {
        #region Service initialization
        _printersService = printersService;
        _dialogService = dialogService;
        _computerService = computerService;
        #endregion
    }
    public PrinterWindowViewModel() { }

    #endregion
}