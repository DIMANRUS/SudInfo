namespace SudInfo.Avalonia.ViewModels.WindowViewModels;

public class ServerRackWindowViewModel : BaseViewModel
{
    #region Services
    private readonly ServerRackService _serverRackService;
    
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

    public ServerRackWindowViewModel(ServerRackService serverRackService)
    {
        #region Service Set
        _serverRackService = serverRackService;
        
        #endregion
    }

    #endregion

    #region Public Methods
    public async void InitializationData(WindowType windowType, int? id = null, ServerRack serverRack = null)
    {
        _windowType = windowType;
        if (id != null)
        {
            SaveButtonText = "Сохранить серверную стойку";
            var server = await _serverRackService.Get(id.GetValueOrDefault());
            if (!server.Success)
            {
                await DialogService.ShowErrorMessageBox(server.Message);
                return;
            }
            ServerRack = server.Object;
        }
    }
    public async Task SaveServerRack()
    {
        if (_windowType == WindowType.Add)
        {
            var result = await _serverRackService.GetNumberServerRacks();
            ServerRack.Position = ++result;
        }
        Result serverRackResult = _windowType switch
        {
            WindowType.Add => await _serverRackService.Add(ServerRack),
            _ => await _serverRackService.Update(ServerRack)
        };
        if (!serverRackResult.Success)
        {
            await DialogService.ShowErrorMessageBox(serverRackResult.Message);
            return;
        }
    }
    #endregion
}