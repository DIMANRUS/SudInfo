namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class ServerWindowViewModel : BaseViewModel
{
    #region Services
    private readonly ServerService _serverService;
    private readonly DialogService _dialogService;

    #endregion

    #region Private Fields
    private WindowType _windowType;
    #endregion

    #region Properties
    [Reactive]
    public Server Server { get; set; } = new();
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить сервер";
    [Reactive]
    public bool ButtonIsVisible { get; private set; } = false;
    #endregion

    #region Constructors
    public ServerWindowViewModel(ServerService serverService, DialogService dialogService)
    {
        #region Service Set

        _serverService = serverService;
        _dialogService = dialogService;
        #endregion
    }
    public ServerWindowViewModel() { }
    #endregion

    #region Public Methods
    public async void InitializationData(WindowType windowType, int? id = null, ServerRack serverRack = null)
    {
        _windowType = windowType;
        if (windowType != WindowType.View)
        {
            ButtonIsVisible = true;
        }
        if (serverRack != null)
        {
            Server.ServerRackId = serverRack.Id;
            Server.PosiitionInServerRack = serverRack.Servers.Count + 1;
        }
        if (id != null)
        {
            if (windowType != WindowType.View)
            {
                ButtonIsVisible = true;
                SaveButtonText = "Сохранить сервер";
            }
            var server = await _serverService.GetServer(id.GetValueOrDefault());
            if (!server.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения сервера! Ошибка: {server.Message}", true, icon: Icon.Error);
                return;
            }
            Server = server.Object;
        }
    }
    public async Task SaveServer()
    {
        if (!ValidationModel(Server))
            return;
        Result serverResult = _windowType switch
        {
            WindowType.Add => await _serverService.Add(Server),
            _ => await _serverService.Update(Server)
        };
        if (!serverResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", serverResult.Message, icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Сообщение", "Успешно!", true, icon: Icon.Success);
    }
    #endregion
}