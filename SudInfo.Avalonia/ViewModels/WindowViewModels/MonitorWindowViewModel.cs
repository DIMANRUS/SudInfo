namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class MonitorWindowViewModel : BaseViewModel
{
    #region Services
    private readonly MonitorService _monitorService;
    private readonly DialogService _dialogService;
    #endregion

    #region Collections
    [Reactive]
    public IEnumerable<Computer> Computers { get; set; }
    #endregion

    #region Properties
    [Reactive]
    public Monitor Monitor { get; set; } = new();
    public bool IsComputer { get; set; }
    [Reactive]
    public bool IsButtonVisible { get; set; } = false;
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить монитор";
    #endregion

    #region Constructors
    public MonitorWindowViewModel(MonitorService monitorService, ComputerService computerService, DialogService dialogService)
    {
        #region Service Set
        _monitorService = monitorService;
        _dialogService = dialogService;
        #endregion

        #region Commands Initialization
        Initialized = ReactiveCommand.CreateFromTask(async () =>
        {
            var computersResult = await computerService.GetComputerNames();
            if (!computersResult.Success)
            {
                await dialogService.ShowMessageBox("Ошибка", "Ошибка загрузки компьютеров!", icon: Icon.Error);
                return;
            }
            Computers = computersResult.Object;
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
        if (windowType != WindowType.View)
            IsButtonVisible = true;
        if (id != null)
        {
            if (windowType != WindowType.View)
            {
                SaveButtonText = "Сохранить монитор";
            }
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
        if (!IsComputer)
            Monitor.Computer = null;
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