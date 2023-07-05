namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class WorkplacesPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly NavigationService _navigationService;
    private readonly DialogService _dialogService;
    private readonly UserService _userService;

    #endregion

    #region Properties

    [Reactive]
    public IReadOnlyList<User>? Users { get; set; }

    public IReadOnlyList<User>? UsersFromDatabase { get; set; }

    [Reactive]
    public string SearchText { get; set; } = string.Empty;

    #endregion

    #region Initialization

    public WorkplacesPageViewModel(
        UserService userService,
        DialogService dialogService,
        NavigationService navigationService)
    {
        #region Services Initialization
        _navigationService = navigationService;
        _userService = userService;
        _dialogService = dialogService;
        #endregion
    }

    #endregion

    #region Public methods

    public void SearchBoxKeyUp()
    {
        if (UsersFromDatabase == null)
            return;
        string searchTextLower = SearchText.ToLower();
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            Users = new List<User>(UsersFromDatabase);
            return;
        }
        Users = new List<User>(UsersFromDatabase.Where(x => x.FIO.ToLower().Contains(searchTextLower) ||
                                                            x.Computers.Any(c =>
                                                                c.Name!.ToLower().Contains(searchTextLower) ||
                                                                c.InventarNumber!.Contains(searchTextLower) ||
                                                                c.SerialNumber!.Contains(searchTextLower) ||
                                                                c.Monitors != null &&
                                                                c.Monitors.Any(m =>
                                                                    m.Name!.ToLower().Contains(searchTextLower) ||
                                                                    m.InventarNumber!.Contains(searchTextLower) ||
                                                                    m.SerialNumber!.Contains(searchTextLower)
                                                                ) ||
                                                                c.Printers != null &&
                                                                c.Printers.Any(p =>
                                                                    p.Name!.ToLower().Contains(searchTextLower) ||
                                                                    p.InventarNumber!.Contains(searchTextLower) ||
                                                                    p.SerialNumber!.Contains(searchTextLower)
                                                                ))));
    }

    public async Task OpenViewComputerWindow(object id)
    {
        await _navigationService.ShowComputerWindowDialog(WindowType.View, computerId: (int)id);
    }

    public async Task OpenViewPrinterWindow(object id)
    {
        await _navigationService.ShowPrinterWindowDialog(WindowType.View, printerId: (int)id);
    }

    public async Task OpenViewMonitorWindow(object id)
    {
        await _navigationService.ShowMonitorWindowDialog(WindowType.View, monitorId: (int)id);
    }

    public async Task OpenViewPeripheryWindow(object id)
    {
        await _navigationService.ShowPeripheryWindowDialog(WindowType.View, peripheryId: (int)id);
    }

    public async Task LoadWorkplaces()
    {
        var usersLoadResult = await UserService.GetUsersWithComputers();
        Users = usersLoadResult;
        UsersFromDatabase = Users;
    }

    #endregion
}