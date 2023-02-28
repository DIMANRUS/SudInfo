namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class ComputerWindowViewModel : BaseViewModel
{
    #region Services
    private readonly ComputerService _computerService;
    private readonly DialogService _dialogService;
    private readonly ValidationService _validationService;
    #endregion

    #region Collections
    public static IEnumerable<OS> OsesList => Enum.GetValues(typeof(OS)).Cast<OS>();
    [Reactive]
    public IEnumerable<User> Users { get; set; }
    #endregion

    #region Properties
    [Reactive]
    public Computer Computer { get; set; } = new();
    public bool IsUser { get; set; }
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить компьютер";
    [Reactive]
    public bool ButtonIsVisible { get; private set; } = false;
    #endregion

    #region Constructors
    public ComputerWindowViewModel(ComputerService computerService, UserService usersService, DialogService dialogService, ValidationService validationService)
    {
        #region Service Set
        _computerService = computerService;
        _dialogService = dialogService;
        _validationService = validationService;
        #endregion

        Initialized = ReactiveCommand.CreateFromTask(async () =>
        {
            var usersResult = await usersService.GetUsers();
            if (!usersResult.Success)
            {
                await dialogService.ShowMessageBox("Ошибка", "Ошибка загрузки!", icon: Icon.Error);
                return;
            }
            Users = usersResult.Object;
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
        if (!ValidationService.ValidationIp4(Computer.Ip) || Computer.CPU.Length < 5 || Computer.RAM == 0 || Computer.ROM == 0 || Computer.SerialNumber.Length < 2 || Computer.InventarNumber.Length < 5)
        {
            await _dialogService.ShowMessageBox(title: "Ошибка", "Проверьте правильность заполнения полей!", icon: Icon.Error);
            return;
        }
        if (!IsUser)
            Computer.User = null;
        Result computerResult = _windowType switch
        {
            WindowType.Add => await _computerService.AddComputer(Computer),
            _ => await _computerService.UpdateComputer(Computer)
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
        if (windowType != WindowType.View)
        {
            ButtonIsVisible = true;
        }
        if (id != null)
        {
            if (windowType != WindowType.View)
            {
                ButtonIsVisible = true;
                SaveButtonText = "Сохранить компьютер";
            }
            var computerResult = await _computerService.GetComputerById(id.GetValueOrDefault());
            if (!computerResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения компьютера! Ошибка: {computerResult.Message}", true, icon: Icon.Error);
                return;
            }
            Computer = computerResult.Object;
        }
    }
    #endregion
}