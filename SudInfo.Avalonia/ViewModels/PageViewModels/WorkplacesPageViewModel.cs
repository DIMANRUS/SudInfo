namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class WorkplacesPageViewModel : BaseRoutableViewModel
{
    #region Private fields
    private readonly NavigationService _navigationService;
    #endregion

    #region Public Properties
    [Reactive]
    public List<User> Users { get; set; }
    public IReadOnlyList<User> UsersFromDatabase { get; set; }
    [Reactive]
    public string SearchText { get; set; } = string.Empty;
    #endregion

    #region Constructors
    public WorkplacesPageViewModel(UserService userService, DialogService dialogService, NavigationService navigationService)
    {
        Initialized = ReactiveCommand.CreateFromTask(async () =>
        {
            var usersLoadResult = await userService.GetUsersWithComputers();
            if (!usersLoadResult.Success)
            {
                await dialogService.ShowMessageBox("Ошибка", "Ошибка загрузки!", icon: Icon.Error);
                return;
            }
            Users = usersLoadResult.Object;
            UsersFromDatabase = usersLoadResult.Object;
        });
        _navigationService = navigationService;
    }
    #endregion

    #region Public methods
    public void SearchBoxKeyUp()
    {
        string searchTextLower = SearchText.ToLower();
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            Users = new(UsersFromDatabase);
            return;
        }
        Users = new(UsersFromDatabase.Where(x => x.FIO.ToLower().Contains(searchTextLower) ||
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
    #endregion
}