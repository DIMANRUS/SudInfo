namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class WorkplacesPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly NavigationService _navigationService;

    #endregion

    #region Properties

    [Reactive]
    public string SearchText { get; set; } = string.Empty;

    #endregion

    #region Collections

    [Reactive]
    public IReadOnlyCollection<User>? Users { get; set; }

    public IReadOnlyCollection<User>? UsersFromDatabase { get; set; }

    #endregion

    #region Ctors

    public WorkplacesPageViewModel(
        NavigationService navigationService)
    {
        _navigationService = navigationService;
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