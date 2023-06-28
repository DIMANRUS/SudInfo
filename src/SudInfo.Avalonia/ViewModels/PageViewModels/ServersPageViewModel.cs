namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class ServersPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly ServerRackService _serverRackService;
    private readonly ServerService _serverService;
    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;

    #endregion

    #region Collections

    [Reactive]
    public IReadOnlyList<ServerRack>? ServerRacks { get; set; }

    #endregion

    #region Initialization

    public ServersPageViewModel(
        ServerRackService serverRackService,
        DialogService dialogService,
        NavigationService navigationService,
        ServerService serverService)
    {
        #region Services Initialization
        _navigationService = navigationService;
        _serverRackService = serverRackService;
        _dialogService = dialogService;
        _serverService = serverService;
        #endregion

        eventHandlerClosedWindowDialog = async (s, e) => await LoadServerRacks();
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
    public async Task RemoveServer(int id)
    {
        var result = await ServerService.RemoveServer(id);
        if (!result.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления сервера. Ошибка: {result.Message}", icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Сообщение", $"Сервер удалён", icon: Icon.Success);
        await LoadServerRacks();
    }
    public async Task ViewServer(int id)
    {
        await _navigationService.ShowServerWindowDialog(WindowType.View, id: id);
    }
    public async Task EditServer(int id)
    {
        await _navigationService.ShowServerWindowDialog(WindowType.Edit, eventHandlerClosedWindowDialog, id);
    }
    public async Task RemoveServerRack(int id)
    {
        var result = await ServerRackService.RemoveServerRack(id);
        if (!result.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления серверной стойки. Ошибка: {result.Message}", icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Сообщение", $"Серверная стойка удалена", icon: Icon.Success);
        await LoadServerRacks();
    }
    public async Task LoadServerRacks()
    {
        var serverRacksResult = await ServerRackService.GetServerRacksWithServers();
        if (!serverRacksResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! Ошибка: {serverRacksResult.Message}", icon: Icon.Error);
            return;
        }
        ServerRacks = serverRacksResult.Object;
    }

    #endregion
}