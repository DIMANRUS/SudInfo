namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class MonitorWindowViewModel : BaseViewModel
{
    #region Services
    private readonly IMonitorService _monitorService;
    private readonly IDialogService _dialogService;
    #endregion

    #region Collections
    [Reactive]
    public IEnumerable<User> Users { get; set; }
    #endregion

    #region Properties
    [Reactive]
    public Monitor Monitor { get; set; } = new();
    public bool IsUser { get; set; }
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить компьютер";
    #endregion

    #region Constructors
    public MonitorWindowViewModel(IMonitorService monitorService, IUserService usersService, IDialogService dialogService)
    {
        #region Service Set
        _monitorService = monitorService;
        _dialogService = dialogService;
        #endregion

        #region Commands Initialization
        Initialized = ReactiveCommand.CreateFromTask(async () =>
        {
            var usersResult = await usersService.GetUsers();
            if (!usersResult.Success)
            {
                await dialogService.ShowMessageBox("Ошибка", "Ошибка загрузки Сотрудников! Монитор можно добавить, но без сотрудника.", icon: Icon.Error);
                return;
            }
            Users = usersResult.Object;
        });
        #endregion
    }
    #endregion

    #region Private Fields
    private WindowType _windowType;
    #endregion

    #region Public Methods
    public async void InitializationData(WindowType windowType, int? id = null)
    {
        _windowType = windowType;
        if (id != null)
        {
            SaveButtonText = "Сохранить монитор";
            var monitorResult = await _monitorService.GetMonitorById(id.GetValueOrDefault());
            if (!monitorResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения монитора! Ошибка: {monitorResult.Message}", true, icon: Icon.Error);
                return;
            }
            Monitor = monitorResult.Object;
        }
    }
    public async Task SaveMonitor()
    {
        if (!IsUser)
            Monitor.User = null;
        Result monitorResult = _windowType switch
        {
            WindowType.Add => await _monitorService.AddMonitor(Monitor),
            _ => await _monitorService.UpdateMonitor(Monitor)
        };
        if (!monitorResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", monitorResult.Message, icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Сообщение", "Успешно!", true, icon: Icon.Success);
    }
    #endregion
}