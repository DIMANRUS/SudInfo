namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class UserWindowViewModel : BaseViewModel
{
    #region Services
    private readonly UserService _usersService;
    private readonly DialogService _dialogService;
    #endregion

    #region Properties
    [Reactive]
    public User User { get; set; } = new();
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить пользователя";
    #endregion

    #region Public Methods
    public async void InitializationData(WindowType windowType, int? id = null)
    {
        _windowType = windowType;
        if (id != null)
        {
            SaveButtonText = "Сохранить пользователя";
            var userResult = await _usersService.GetUserById(id.GetValueOrDefault());
            if (!userResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения пользователя! Ошибка: {userResult.Message}", true, icon: Icon.Error);
                return;
            }
            User = userResult.Object;
        }
    }
    #endregion

    #region Private Fields
    private WindowType _windowType;
    #endregion

    #region Constructors
    public UserWindowViewModel(UserService usersService, DialogService dialogService)
    {
        #region Service Set
        _usersService = usersService;
        _dialogService = dialogService;
        #endregion
    }
    public UserWindowViewModel() { }
    #endregion

    #region Public Methods
    public async Task SaveUser()
    {
        if (!ValidationModel(User))
            return;
        Result userResult = _windowType switch
        {
            WindowType.Add => await _usersService.AddUser(User),
            _ => await _usersService.UpdateUser(User)
        };
        if (!userResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", userResult.Message, icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Сообщение", "Успешно!", true, icon: Icon.Success);
    }
    #endregion
}