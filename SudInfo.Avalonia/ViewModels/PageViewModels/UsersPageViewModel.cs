namespace SudInfo.Avalonia.ViewModels.PageViewModels;
public class UsersPageViewModel : BaseRoutableViewModel
{
    #region Services
    private readonly IUsersService _usersService;
    private readonly IDialogService _dialogService;
    #endregion

    #region Commands
    public ICommand OpenAddUserWindow { get; private init; }
    public ICommand OpenEditUserWindow { get; private init; }
    public ICommand RefreshUsers { get; private init; }
    public ICommand RemoveUser { get; private init; }
    public ICommand SearchBoxKeyUp { get; private init; }
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
    public UsersPageViewModel(INavigationService navigationService, IUsersService usersService, IDialogService dialogService)
    {
        #region Serives Initialization
        _dialogService = dialogService;
        _usersService = usersService;
        #endregion

        eventHandlerClosedWindowDialog = async (s, e) =>
        {
            await LoadUsers();
        };

        #region Commands Initialization
        OpenAddUserWindow = ReactiveCommand.Create(async () =>
        {
            await navigationService.ShowUserWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
        });
        OpenEditUserWindow = ReactiveCommand.Create(async (int id) =>
        {
            await navigationService.ShowUserWindowDialog(WindowType.Save, eventHandlerClosedWindowDialog, id);
        });
        Initialized = ReactiveCommand.CreateFromTask(LoadUsers);
        RefreshUsers = ReactiveCommand.CreateFromTask(LoadUsers);
        RemoveUser = ReactiveCommand.CreateFromTask(async (int id) =>
        {
            var dialogResult = await dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить пользователя?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
            if (dialogResult == ButtonResult.No)
                return;
            var removeUserResult = await usersService.RemoveUserById(id);
            if (!removeUserResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления пользователя! Ошибка: {removeUserResult.Message}", icon: Icon.Error);
                return;
            }
            await LoadUsers();
        });
        SearchBoxKeyUp = ReactiveCommand.Create(() =>
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                Users = new(UsersFromDataBase);
                return;
            }
            Users = new(UsersFromDataBase.Where(x => x.LastName.ToLower().Contains(SearchText.ToLower())));
        });
        #endregion
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
        Users = new(usersResult.Result);
        UsersFromDataBase = Users;
    }
    #endregion
}