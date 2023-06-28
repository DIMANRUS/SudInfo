namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class ServerRackWindowViewModel : BaseViewModel
{
    #region Services
    private readonly ServerRackService _serverRackService;
    private readonly DialogService _dialogService;
    #endregion

    #region Private Fields
    private WindowType _windowType;
    #endregion

    #region Properties
    [Reactive]
    public ServerRack ServerRack { get; set; } = new();
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить серверную стойку";
    #endregion

    #region Constructors
    public ServerRackWindowViewModel(ServerRackService serverRackService, DialogService dialogService)
    {
        #region Service Set
        _serverRackService = serverRackService;
        _dialogService = dialogService;
        #endregion
    }
    public ServerRackWindowViewModel() { }
    #endregion

    #region Public Methods
    public async void InitializationData(WindowType windowType, int? id = null, ServerRack serverRack = null)
    {
        _windowType = windowType;
        if (id != null)
        {
            SaveButtonText = "Сохранить серверную стойку";
            var server = await ServerRackService.GetServerRack(id.GetValueOrDefault());
            if (!server.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения серверной стойки! Ошибка: {server.Message}", true, icon: Icon.Error);
                return;
            }
            ServerRack = server.Object;
        }
    }
    public async Task SaveServerRack()
    {
        if (_windowType == WindowType.Add)
        {
            var result = await ServerRackService.GetNumberServerRacks();
            ServerRack.Position = ++result;
        }
        Result serverRackResult = _windowType switch
        {
            WindowType.Add => await _serverRackService.Add(ServerRack),
            _ => await _serverRackService.Update(ServerRack)
        };
        if (!serverRackResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", serverRackResult.Message, icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Сообщение", "Успешно!", true, icon: Icon.Success);
    }
    #endregion
}