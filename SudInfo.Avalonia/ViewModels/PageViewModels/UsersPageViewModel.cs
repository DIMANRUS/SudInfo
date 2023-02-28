namespace SudInfo.Avalonia.ViewModels.PageViewModels;
public class UsersPageViewModel : BaseRoutableViewModel
{
    #region Services
    private readonly UserService _usersService;
    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;
    #endregion

    #region Collections
    [Reactive]
    public ObservableCollection<User> Users { get; set; }
    private IEnumerable<User> UsersFromDataBase { get; set; }
    #endregion

    #region Public properties
    [Reactive]
    public string SearchText { get; set; } = string.Empty;
    #endregion

    #region Constructors
    public UsersPageViewModel(NavigationService navigationService, UserService usersService, DialogService dialogService)
    {
        #region Serives Initialization
        _dialogService = dialogService;
        _usersService = usersService;
        _navigationService = navigationService;
        #endregion

        eventHandlerClosedWindowDialog = async (s, e) =>
        {
            await LoadUsers();
        };
        Initialized = ReactiveCommand.CreateFromTask(LoadUsers);
    }
    #endregion

    #region Public Methods
    public async Task OpenAddUserWindow()
    {
        await _navigationService.ShowUserWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
    }
    public async Task OpenEditUserWindow(int id)
    {
        await _navigationService.ShowUserWindowDialog(WindowType.Edit, eventHandlerClosedWindowDialog, id);
    }
    public async Task RefreshUsers()
    {
        await LoadUsers();
    }
    public async Task RemoveUser(int id)
    {
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить пользователя?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removeUserResult = await _usersService.RemoveUserById(id);
        if (!removeUserResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления пользователя! Ошибка: {removeUserResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadUsers();
    }
    public void SearchBoxKeyUp()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            Users = new(UsersFromDataBase);
            return;
        }
        Users = new(UsersFromDataBase.Where(x => x.LastName.ToLower().Contains(SearchText.ToLower())));
    }
    #endregion

    #region Private Methods
    private async Task LoadUsers()
    {
        var usersResult = await _usersService.GetUsers();
        if (!usersResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! Ошибка: {usersResult.Message}", icon: Icon.Error);
            return;
        }
        Users = new(usersResult.Object);
        UsersFromDataBase = Users;
    }
    #endregion
}