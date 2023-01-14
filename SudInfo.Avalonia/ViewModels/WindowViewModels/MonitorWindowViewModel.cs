namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class MonitorWindowViewModel : BaseViewModel
{
    #region Services
    private readonly IMonitorsService _monitorService;
    private readonly IDialogService _dialogService;
    #endregion

    #region Commands
    public ICommand SaveMonitor { get; set; }
    #endregion

    #region Collections
    public IEnumerable<User> Users { get; set; }
    #endregion

    #region Properties
    [Reactive]
    public Monitor Monitor { get; set; } = new();
    public bool IsEmployee { get; set; }
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить компьютер";
    #endregion

    #region Constructors
    public MonitorWindowViewModel(IMonitorsService monitorService, IUsersService usersService, IDialogService dialogService)
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
            Users = usersResult.Result;
        });
        SaveMonitor = ReactiveCommand.CreateFromTask(async () =>
        {
            try
            {
                if (!IsEmployee)
                    Monitor.Employee = null;
                switch (_windowType)
                {
                    case WindowType.Save:
                        await monitorService.SaveMonitor(Monitor);
                        await dialogService.ShowMessageBox("Сообщение", "Монитор сохранён!", true, icon: Icon.Success);
                        break;
                    case WindowType.Add:
                        await monitorService.AddMonitor(Monitor);
                        await dialogService.ShowMessageBox("Сообщение", "Монитор добавлен!", icon: Icon.Success);
                        break;
                }
            }
            catch (Exception ex)
            {
                await dialogService.ShowMessageBox("Ошибка", $"Ошибка: {ex.Message}", icon: Icon.Error);
            }
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
            Monitor = monitorResult.Result;
        }
    }
    #endregion
}