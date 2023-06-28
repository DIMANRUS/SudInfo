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
                                                              x.Computers.Where(c => c.Name.ToLower().Contains(searchTextLower) || c.InventarNumber.Contains(searchTextLower)
                                                                || c.Monitors.Where(m => m.Name.ToLower().Contains(searchTextLower) || m.InventarNumber.Contains(searchTextLower)).Any() ||
                                                                 c.Printers.Where(p => p.Name.ToLower().Contains(searchTextLower) || p.InventarNumber.Contains(searchTextLower)).Any()).Any()
                                                              ));
    }
    public async Task OpenViewComputerWindow(int id)
    {
        await _navigationService.ShowComputerWindowDialog(WindowType.View, computerId: id);
    }
    public async Task OpenViewPrinterWindow(int id)
    {
        await _navigationService.ShowPrinterWindowDialog(WindowType.View, printerId: id);
    }
    public async Task OpenViewMonitorWindow(int id)
    {
        await _navigationService.ShowMonitorWindowDialog(WindowType.View, monitorId: id);
    }
    public async Task OpenViewPeripheryWindow(int id)
    {
        await _navigationService.ShowPeripheryWindowDialog(WindowType.View, peripheryId: id);
    }
    public async Task LoadWorkplaces()
    {
        var usersLoadResult = await UserService.GetUsersWithComputers();
        Users = usersLoadResult;
        UsersFromDatabase = Users;
    }

    #endregion
}