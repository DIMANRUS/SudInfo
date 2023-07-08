namespace SudInfo.Avalonia.ViewModels.WindowViewModels;

public class ComputerWindowViewModel : BaseViewModel
{
    #region Services

    private readonly ComputerService _computerService;
    private readonly DialogService _dialogService;
    private readonly UserService _usersService;

    #endregion

    #region Collections
    public static IEnumerable<OS> OsesList => Enum.GetValues(typeof(OS)).Cast<OS>();

    [Reactive]
    public IEnumerable<User>? Users { get; set; }

    #endregion

    #region Properties

    [Reactive]
    public Computer Computer { get; set; } = new();

    [Reactive]
    public bool IsUser { get; set; }

    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить компьютер";

    [Reactive]
    public bool ButtonIsVisible { get; private set; }
    #endregion

    #region Constructors

    public ComputerWindowViewModel(
        ComputerService computerService,
        UserService usersService,
        DialogService dialogService)
    {
        #region Service Initialization
        _computerService = computerService;
        _dialogService = dialogService;
        _usersService = usersService;
        #endregion
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
        if (!ValidationModel(Computer))
            return;
        if (!IsUser)
            Computer.User = null;
        Result computerResult = _windowType switch
        {
            WindowType.Add => await ComputerService.AddComputer(Computer),
            _ => await _computerService.Update(Computer)
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
            var computerResult = await ComputerService.GetComputerById(id.GetValueOrDefault());
            if (!computerResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения компьютера! Ошибка: {computerResult.Message}", true, icon: Icon.Error);
                return;
            }
            IsUser = computerResult.Object.User != null;
            Computer = computerResult.Object;
        }
    }

    public async Task LoadUsers()
    {
        var usersResult = await UserService.GetUsers();
        Users = usersResult;
    }

    #endregion
}