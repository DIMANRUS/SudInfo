namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class ServerWindowViewModel : BaseViewModel
{
    #region Services
    private readonly ServerService _serverService;
    private readonly DialogService _dialogService;
    private readonly ValidationService _validationService;
    #endregion

    #region Private Fields
    private WindowType _windowType;
    #endregion

    #region Properties
    [Reactive]
    public Server Server { get; set; } = new();
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить сервер";
    #endregion

    #region Constructors
    public ServerWindowViewModel(ServerService serverService, DialogService dialogService, ValidationService validationService)
    {
        #region Service Set
        _validationService = validationService;
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
        if (serverRack != null)
        {
            Server.ServerRackId = serverRack.Id;
            Server.PosiitionInServerRack = serverRack.Servers.Count + 1;
        }
        if (id != null)
        {
            SaveButtonText = "Сохранить сервер";
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
        if (Server.Name.Length < 5 || !ValidationService.ValidationIp4(Server.IpAddress))
        {
            await _dialogService.ShowMessageBox(title: "Ошибка", "Проверьте правильность заполнения полей!", icon: Icon.Error);
            return;
        }
        Result serverResult = _windowType switch
        {
            WindowType.Add => await _serverService.AddServer(Server),
            _ => await _serverService.UpdateServer(Server)
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