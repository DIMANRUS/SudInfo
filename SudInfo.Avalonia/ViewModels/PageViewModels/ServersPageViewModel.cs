namespace SudInfo.Avalonia.ViewModels.PageViewModels;
public class ServersPageViewModel : BaseRoutableViewModel
{
    #region Services
    private readonly ServerRackService _serverRackService;
    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;
    #endregion
    [Reactive]
    public List<ServerRack> ServerRacks { get; set; }

    public ServersPageViewModel(ServerRackService serverRackService, DialogService dialogService, NavigationService navigationService)
    {
        _navigationService = navigationService;
        _serverRackService = serverRackService;
        _dialogService = dialogService;

        Initialized = ReactiveCommand.CreateFromTask(LoadServerRacks);

        eventHandlerClosedWindowDialog = async (s, e) =>
        {
            await LoadServerRacks();
        };
    }

    #region Private Methods
    private async Task LoadServerRacks()
    {
        var serverRacksResult = await _serverRackService.GetServerRacks();
        if (!serverRacksResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! Ошибка: {serverRacksResult.Message}", icon: Icon.Error);
            return;
        }
        ServerRacks = serverRacksResult.Object;
        foreach (var serverRack in ServerRacks)
            serverRack.Servers = serverRack.Servers.OrderBy(x => x.PosiitionInServerRack).ToList();
    }
    #endregion

    #region Public Methods
    public async Task OpenAddServerWindow(ServerRack serverRack)
    {
        await _navigationService.ShowServerWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog, serverRack: serverRack);
    }
    public async Task OpenAddServerRackWindow()
    {
        await _navigationService.ShowServerRackWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
    }
    public async Task RemoveServer()
    {

    }
    public async Task RemoveServerRack()
    {

    }
    #endregion
}