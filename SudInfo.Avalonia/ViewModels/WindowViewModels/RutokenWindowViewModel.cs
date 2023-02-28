namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class RutokenWindowViewModel : BaseViewModel
{
    #region Services
    private readonly RutokenService _rutokenService;
    private readonly DialogService _dialogService;
    #endregion

    #region Collections
    [Reactive]
    public IEnumerable<User> Users { get; set; }
    #endregion

    #region Properties
    [Reactive]
    public Rutoken Rutoken { get; set; } = new();
    public bool IsUser { get; set; }
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить рутокен";
    #endregion

    #region Constructors
    public RutokenWindowViewModel(RutokenService rutokenSerrvice, UserService usersService, DialogService dialogService)
    {
        #region Service Set
        _rutokenService = rutokenSerrvice;
        _dialogService = dialogService;
        #endregion

        Initialized = ReactiveCommand.CreateFromTask(async () =>
        {
            var usersResult = await usersService.GetUsers();
            if (!usersResult.Success)
            {
                await dialogService.ShowMessageBox("Ошибка", "Ошибка загрузки Сотрудников! Рутокен можно добавить, но без сотрудника.", icon: Icon.Error);
                return;
            }
            Users = usersResult.Object;
        });
    }

    public RutokenWindowViewModel()
    {
    }
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
            WindowType.Add => await _rutokenService.AddRutoken(Rutoken),
            _ => await _rutokenService.UpdateRutoken(Rutoken)
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
            var rutokenResult = await _rutokenService.GetRutokenById(id.GetValueOrDefault());
            if (!rutokenResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения рутокена! Ошибка: {rutokenResult.Message}", true, icon: Icon.Error);
                return;
            }
            Rutoken = rutokenResult.Object;
        }
    }
    #endregion
}