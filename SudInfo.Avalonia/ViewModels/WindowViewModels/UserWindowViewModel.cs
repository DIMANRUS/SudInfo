namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class UserWindowViewModel : BaseViewModel
{
    #region Services
    private readonly IUsersService _usersService;
    private readonly IDialogService _dialogService;
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
            User = userResult.Result;
        }
    }
    #endregion

    #region Private Fields
    private WindowType _windowType;
    #endregion

    #region Constructors
    public UserWindowViewModel(IUsersService usersService, IDialogService dialogService)
    {
        #region Service Set
        _usersService = usersService;
        _dialogService = dialogService;
        #endregion

        #region Commands Realizations
        SaveUser = ReactiveCommand.CreateFromTask(async () =>
        {
            try
            {
                switch (_windowType)
                {
                    case WindowType.Save:
                        await _usersService.SaveUser(User);
                        await dialogService.ShowMessageBox("Сообщение", "Пользователь сохранён!", true, icon: Icon.Success);
                        break;
                    case WindowType.Add:
                        await _usersService.SaveUser(User);
                        await dialogService.ShowMessageBox("Сообщение", "Пользователь добавлен!", icon: Icon.Success);
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
    public UserWindowViewModel() { }
    #endregion

    #region Commands
    public ICommand SaveUser { get; private init; }
    #endregion
}