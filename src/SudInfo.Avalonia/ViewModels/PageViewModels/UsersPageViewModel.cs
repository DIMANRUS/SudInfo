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
    public IReadOnlyCollection<User>? Users { get; set; }

    private IReadOnlyCollection<User>? UsersFromDataBase { get; set; }

    #endregion

    #region Properties

    [Reactive]
    public string SearchText { get; set; } = string.Empty;

    [Reactive]
    public User? SelectedUser { get; set; }

    #endregion

    #region Ctors

    public UsersPageViewModel(
        NavigationService navigationService,
        UserService usersService,
        DialogService dialogService)
    {
        #region Serives Initialization
        _dialogService = dialogService;
        _usersService = usersService;
        _navigationService = navigationService;
        #endregion

        eventHandlerClosedWindowDialog = async (s, e) => await LoadUsers();
    }

    #endregion

    #region Public Methods

    public async Task OpenAddUserWindow()
    {
        await _navigationService.ShowUserWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
    }

    public async Task OpenEditUserWindow()
    {
        if (SelectedUser == null)
            return;
        await _navigationService.ShowUserWindowDialog(WindowType.Edit, eventHandlerClosedWindowDialog, SelectedUser.Id);
    }

    public async Task RemoveUser()
    {
        if (SelectedUser == null)
            return;
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить пользователя?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removeUserResult = await UserService.RemoveUserById(SelectedUser.Id);
        if (!removeUserResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления пользователя! Ошибка: {removeUserResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadUsers();
    }

    public void SearchBoxKeyUp()
    {
        if (UsersFromDataBase == null)
            return;
        if (string.IsNullOrEmpty(SearchText))
        {
            Users = UsersFromDataBase;
            return;
        }
        Users = UsersFromDataBase.Where(x => x.FIO!.ToLower().Contains(SearchText.ToLower())).ToList();
    }

    public async Task LoadUsers()
    {
        Users = await UserService.GetUsers();
        UsersFromDataBase = Users;
    }

    #endregion
}