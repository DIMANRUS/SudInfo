namespace SudInfo.Avalonia.ViewModels.WindowViewModels;

public class RutokenWindowViewModel : BaseViewModel
{
    #region Services

    private readonly RutokenService _rutokenService;
    private readonly DialogService _dialogService;
    private readonly UserService _usersService;

    #endregion

    #region Collections

    [Reactive]
    public IReadOnlyCollection<User>? Users { get; set; }

    #endregion

    #region Properties
    [Reactive]
    public Rutoken Rutoken { get; set; } = new();
    public bool IsUser { get; set; }
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить рутокен";
    #endregion

    #region Constructors

    public RutokenWindowViewModel(
        RutokenService rutokenSerrvice,
        UserService usersService,
        DialogService dialogService)
    {
        #region Service Initialization
        _rutokenService = rutokenSerrvice;
        _dialogService = dialogService;
        _usersService = usersService;
        #endregion
    }

    public RutokenWindowViewModel() { }

    #endregion

    #region Private Fields
    private WindowType _windowType;
    #endregion

    #region Public Methods

    public async Task SaveRutoken()
    {
        if (!IsUser)
            Rutoken.User = null;
        Result rutokenResult = _windowType switch
        {
            WindowType.Add => await RutokenService.AddRutoken(Rutoken),
            _ => await _rutokenService.Update(Rutoken)
        };
        if (!rutokenResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", rutokenResult.Message, icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Сообщение", "Успешно!", true, icon: Icon.Success);
    }
    public async void InitializationData(WindowType windowType, int? id = null)
    {
        _windowType = windowType;
        if (id != null)
        {
            SaveButtonText = "Сохранить рутокен";
            var rutokenResult = await RutokenService.GetRutokenById(id.GetValueOrDefault());
            if (!rutokenResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения рутокена! Ошибка: {rutokenResult.Message}", true, icon: Icon.Error);
                return;
            }
            Rutoken = rutokenResult.Object;
        }
    }
    public async Task LoadUsers()
    {
        var usersResult = await UserService.GetUsers();
        Users = usersResult;
    }

    #endregion
}