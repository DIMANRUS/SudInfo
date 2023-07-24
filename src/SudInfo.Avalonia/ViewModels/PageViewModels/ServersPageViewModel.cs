namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class ServersPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly DialogService _dialogService;

    private readonly NavigationService _navigationService;

    #endregion

    #region Collections

    [Reactive]
    public IReadOnlyCollection<ServerRack>? ServerRacks { get; set; }

    #endregion

    #region Ctors

    public ServersPageViewModel(
        ServerRackService serverRackService,
        DialogService dialogService,
        NavigationService navigationService,
        ServerService serverService)
    {
        #region Services Initialization
        _navigationService = navigationService;
        _dialogService = dialogService;
        #endregion

        eventHandlerClosedWindowDialog = async (s, e) => await LoadServerRacks();

        #region Commands Initialization

        OpenAddServerWindowCommand = ReactiveCommand.Create<ServerRack>(async (ServerRack serverRack) =>
{
    await _navigationService.ShowServerWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog, serverRack: serverRack);
});

        RemoveServerCommand = ReactiveCommand.Create<int>(async (int id) =>
        {
            var result = await ServerService.RemoveServer(id);
            if (!result.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления сервера. Ошибка: {result.Message}", icon: Icon.Error);
                return;
            }
            await _dialogService.ShowMessageBox("Сообщение", $"Сервер удалён", icon: Icon.Success);
            await LoadServerRacks();
        });

        RemoveServerRackCommand = ReactiveCommand.Create<int>(async (int id) =>
        {
            var result = await ServerRackService.RemoveServerRack(id);
            if (!result.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления серверной стойки. Ошибка: {result.Message}", icon: Icon.Error);
                return;
            }
            await _dialogService.ShowMessageBox("Сообщение", $"Серверная стойка удалена", icon: Icon.Success);
            await LoadServerRacks();
        });

        EditServerCommand = ReactiveCommand.Create<int>(async (int id) =>
        {
            await _navigationService.ShowServerWindowDialog(WindowType.Edit, eventHandlerClosedWindowDialog, id);
        });

        ViewServerCommand = ReactiveCommand.Create<int>(async (int id) =>
        {
            await _navigationService.ShowServerWindowDialog(WindowType.View, id: id);
        });

        UpServerPositionInServerRack = ReactiveCommand.Create<int>(async (int id) =>
        {
            Result result = await ServerService.UpServerPositionInServerRack(id);
            if (!result.Success)
                await MessageBoxManager.GetMessageBoxStandard("Ошибка!", result.Message, ButtonEnum.Ok, Icon.Error)
                                       .ShowAsync();
            await LoadServerRacks();
        });

        DownServerPositionInServerRack = ReactiveCommand.Create<int>(async (int id) =>
        {
            Result result = await ServerService.DownServerPositionInServerRack(id);
            if (!result.Success)
                await MessageBoxManager.GetMessageBoxStandard("Ошибка!", result.Message, ButtonEnum.Ok, Icon.Error)
                                       .ShowAsync();
            await LoadServerRacks();
        });

        #endregion
    }

    #endregion

    #region Public Methods

    public async Task OpenAddServerRackWindow()
    {
        await _navigationService.ShowServerRackWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
    }

    public async Task LoadServerRacks()
    {
        IsLoading = true;
        var serverRacksResult = await ServerRackService.GetServerRacksWithServers();
        if (!serverRacksResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! Ошибка: {serverRacksResult.Message}", icon: Icon.Error);
            return;
        }
        ServerRacks = serverRacksResult.Object;
        IsLoading = false;
    }

    #endregion

    #region Commands

    public ReactiveCommand<ServerRack, Unit> OpenAddServerWindowCommand { get; init; }

    public ReactiveCommand<int, Unit> RemoveServerCommand { get; init; }

    public ReactiveCommand<int, Unit> RemoveServerRackCommand { get; init; }

    public ReactiveCommand<int, Unit> ViewServerCommand { get; init; }

    public ReactiveCommand<int, Unit> EditServerCommand { get; init; }

    public ReactiveCommand<int, Unit> UpServerPositionInServerRack { get; init; }

    public ReactiveCommand<int, Unit> DownServerPositionInServerRack { get; init; }

    #endregion
}