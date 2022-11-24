namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class ComputerWindowViewModel : BaseViewModel
{
    #region Services
    private readonly IComputersService _computersService;
    private readonly IDialogService _dialogService;
    #endregion

    #region Commands
    public ICommand SaveComputer { get; set; }
    #endregion

    #region Collections
    public IEnumerable<OS> OsesList => Enum.GetValues(typeof(OS)).Cast<OS>();
    public IEnumerable<Employee> Employees { get; set; }
    #endregion

    #region Properties
    [Reactive]
    public Computer Computer { get; set; } = new();
    public bool IsEmployee { get; set; }
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить компьютер";
    #endregion

    #region Constructors
    public ComputerWindowViewModel(IComputersService computersService, IEmployeeService employeeService, IDialogService dialogService, IValidationService validationService)
    {
        #region Service Set
        _computersService = computersService;
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
                await dialogService.ShowMessageBox("Ошибка", "Ошибка загрузки Сотрудников! Компьютер можно добавить, но без сотрудника.", icon: Icon.Error);
            }
        });

        #region Commands Initialization
        SaveComputer = ReactiveCommand.CreateFromTask(async () =>
        {
            try
            {
                if (!validationService.ValidationIp4(Computer.Ip) || Computer.CPU.Length < 5 || Computer.RAM == 0 || Computer.ROM == 0 || Computer.SerialNumber.Length < 2 || Computer.InventarNumber.Length < 5)
                {
                    await dialogService.ShowMessageBox(title: "Ошибка", "Проверьте правильность заполнения полей!", icon: Icon.Error);
                    return;
                }
                if (!IsEmployee)
                    Computer.Employee = null;
                switch (_windowType)
                {
                    case WindowType.Save:
                        await computersService.SaveComputer(Computer);
                        await dialogService.ShowMessageBox("Сообщение", "Компьютер сохранён!", true, icon: Icon.Success);
                        break;
                    case WindowType.Add:
                        await computersService.AddComputer(Computer);
                        await dialogService.ShowMessageBox("Сообщение", "Компьютер добавлен!", icon: Icon.Success);
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

    public ComputerWindowViewModel()
    {
    }
    #endregion

    #region Private Fields
    private WindowType _windowType;
    private int _computerId;
    #endregion

    #region Public Methods
    public async void InitializationData(WindowType windowType, int? id = null)
    {
        _windowType = windowType;
        if (id != null)
        {
            SaveButtonText = "Сохранить компьютер";
            _computerId = id.GetValueOrDefault();
            var computerResult = await _computersService.GetComputerById(id.GetValueOrDefault());
            if (!computerResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получени компьютера! Ошибка: {computerResult.Message}", true, icon: Icon.Error);
                return;
            }
            Computer = computerResult.Result;
        }
    }
    #endregion
}