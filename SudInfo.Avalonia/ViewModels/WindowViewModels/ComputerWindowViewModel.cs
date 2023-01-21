namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class ComputerWindowViewModel : BaseViewModel
{
    #region Services
    private readonly IComputersService _computersService;
    private readonly IDialogService _dialogService;
    private readonly IValidationService _validationService;
    #endregion

    #region Collections
    public static IEnumerable<OS> OsesList => Enum.GetValues(typeof(OS)).Cast<OS>();
    [Reactive]
    public IEnumerable<User> Users { get; set; }
    #endregion

    #region Properties
    [Reactive]
    public Computer Computer { get; set; } = new();
    public bool IsEmployee { get; set; }
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить компьютер";
    #endregion

    #region Constructors
    public ComputerWindowViewModel(IComputersService computersService, IUsersService usersService, IDialogService dialogService, IValidationService validationService)
    {
        #region Service Set
        _computersService = computersService;
        _dialogService = dialogService;
        _validationService = validationService;
        #endregion

        Initialized = ReactiveCommand.CreateFromTask(async () =>
        {
            var usersResult = await usersService.GetUsers();
            if (!usersResult.Success)
            {
                await dialogService.ShowMessageBox("Ошибка", "Ошибка загрузки Сотрудников! Компьютер можно добавить, но без сотрудника.", icon: Icon.Error);
                return;
            }
            Users = usersResult.Result;
        });
    }

    public ComputerWindowViewModel()
    {
    }
    #endregion

    #region Private Fields
    private WindowType _windowType;
    #endregion

    #region Public Methods
    public async Task SaveComputer()
    {
        if (!_validationService.ValidationIp4(Computer.Ip) || Computer.CPU.Length < 5 || Computer.RAM == 0 || Computer.ROM == 0 || Computer.SerialNumber.Length < 2 || Computer.InventarNumber.Length < 5)
        {
            await _dialogService.ShowMessageBox(title: "Ошибка", "Проверьте правильность заполнения полей!", icon: Icon.Error);
            return;
        }
        if (!IsEmployee)
            Computer.Employee = null;
        TaskResult computerResult = _windowType switch
        {
            WindowType.Add => await _computersService.AddComputer(Computer),
            _ => await _computersService.SaveComputer(Computer)
        };
        if (!computerResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", computerResult.Message, icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Сообщение", "Успешно!", true, icon: Icon.Success);
    }
    public async void InitializationData(WindowType windowType, int? id = null)
    {
        _windowType = windowType;
        if (id != null)
        {
            SaveButtonText = "Сохранить компьютер";
            var computerResult = await _computersService.GetComputerById(id.GetValueOrDefault());
            if (!computerResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения компьютера! Ошибка: {computerResult.Message}", true, icon: Icon.Error);
                return;
            }
            Computer = computerResult.Result;
        }
    }
    #endregion
}