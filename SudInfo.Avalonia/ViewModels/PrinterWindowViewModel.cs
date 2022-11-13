namespace SudInfo.Avalonia.ViewModels;
public class PrinterWindowViewModel : BaseModel
{
    #region Services
    private readonly IPrintersService _printersService;
    private readonly IDialogService _dialogService;
    #endregion

    #region Properties
    [Reactive]
    public Printer Printer { get; set; } = new();
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить принтер";
    public bool IsEmployee { get; set; }
    #endregion

    #region Public Methods
    public async void InitializationData(WindowType windowType, int? id = null)
    {
        _windowType = windowType;
        if (id != null)
        {
            SaveButtonText = "Сохранить принтер";
            _printerId = id.GetValueOrDefault();
            var printerResult = await _printersService.GetPrinterById(id.GetValueOrDefault());
            if (!printerResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получени компьютера! Ошибка: {printerResult.Message}", true, icon: Icon.Error);
                return;
            }
            Printer = printerResult.Result;
        }
    }
    #endregion

    #region Private Fields
    private WindowType _windowType;
    private int _printerId;
    #endregion

    #region Collections
    public IEnumerable<PrinterType> PrinterTypes => Enum.GetValues(typeof(PrinterType)).Cast<PrinterType>();
    public IEnumerable<Employee> Employees { get; private set; }
    #endregion

    #region Constructors
    public PrinterWindowViewModel(IPrintersService printersService, IDialogService dialogService, IEmployeeService employeeService, IValidationService validationService)
    {
        #region Service Set
        _printersService = printersService;
        _dialogService = dialogService;
        #endregion

        Task.Run(async () =>
        {
            try
            {
                Employees = await employeeService.GetEmployees();
            }
            catch
            {
                await dialogService.ShowMessageBox("Ошибка", "Ошибка загрузки Сотрудников! Принтер можно добавить, но без сотрудника.", icon: Icon.Error);
            }
        });

        #region Commands Realizations
        SavePrinter = ReactiveCommand.CreateFromTask(async () =>
        {
            try
            {
                if (Printer.Name.Length < 5 || !validationService.ValidationIp4(Printer.Ip))
                {
                    await dialogService.ShowMessageBox(title: "Ошибка", "Проверьте правильность заполнения полей!", icon: Icon.Error);
                    return;
                }
                if (!IsEmployee)
                    Printer.Employee = null;
                switch (_windowType)
                {
                    case WindowType.Save:
                        await printersService.SavePrinter(Printer);
                        await dialogService.ShowMessageBox("Сообщение", "Принтер сохранён!", true, icon: Icon.Success);
                        break;
                    case WindowType.Add:
                        await printersService.AddPrinter(Printer);
                        await dialogService.ShowMessageBox("Сообщение", "Принтер добавлен!", icon: Icon.Success);
                        break;
                }
            }
            catch (Exception ex)
            {
                await dialogService.ShowMessageBox("Ошибка", $"Ошибка: {ex.Message}", icon: Icon.Error);
            }
        });
        #endregion
    }

    public PrinterWindowViewModel()
    {
    }
    #endregion

    #region Commands
    public ICommand SavePrinter { get; private init; }
    #endregion
}