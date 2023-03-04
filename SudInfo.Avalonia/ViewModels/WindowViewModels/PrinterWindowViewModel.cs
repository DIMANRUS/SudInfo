namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class PrinterWindowViewModel : BaseViewModel
{
    #region Services
    private readonly PrinterService _printersService;
    private readonly DialogService _dialogService;
    #endregion

    #region Properties
    [Reactive]
    public Printer Printer { get; set; } = new();
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить принтер";
    [Reactive]
    public bool IsButtonVisible { get; set; } = false;
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
            var printerResult = await _printersService.GetPrinterById(id.GetValueOrDefault());
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
            WindowType.Add => await _printersService.AddPrinter(Printer),
            _ => await _printersService.UpdatePrinter(Printer)
        };
        if (!printerResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", printerResult.Message, icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Сообщение", "Успешно!", true, icon: Icon.Success);
    }
    #endregion

    #region Private Fields
    private WindowType _windowType;
    #endregion

    #region Collections
    public static IEnumerable<PrinterType> PrinterTypes => Enum.GetValues(typeof(PrinterType)).Cast<PrinterType>();
    [Reactive]
    public IEnumerable<Computer> Computers { get; private set; }
    #endregion

    #region Constructors
    public PrinterWindowViewModel(PrinterService printersService, DialogService dialogService, ComputerService computerService)
    {
        #region Service Set
        _printersService = printersService;
        _dialogService = dialogService;
        #endregion

        #region Commands Realizations
        Initialized = ReactiveCommand.CreateFromTask(async () =>
        {
            var computersResult = await computerService.GetComputerNames();
            if (!computersResult.Success)
            {
                await dialogService.ShowMessageBox("Ошибка", "Ошибка загрузки компьютеров!", icon: Icon.Error);
                return;
            }
            Computers = computersResult.Object;
        });
        #endregion
    }
    public PrinterWindowViewModel() { }
    #endregion
}