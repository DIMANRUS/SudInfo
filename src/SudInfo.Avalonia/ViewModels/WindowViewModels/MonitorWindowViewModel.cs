namespace SudInfo.Avalonia.ViewModels.WindowViewModels;

public class MonitorWindowViewModel : BaseViewModel
{
    #region Services

    private readonly MonitorService _monitorService;
    
    private readonly ComputerService _computerService;

    #endregion

    #region Collections

    [Reactive]
    public IReadOnlyCollection<Computer>? Computers { get; set; }

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

    #region Initialization

    public MonitorWindowViewModel(
        MonitorService monitorService,
        ComputerService computerService)
    {
        #region Service Initialization
        _monitorService = monitorService;
        
        _computerService = computerService;
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
            var monitorResult = await _monitorService.Get(id.GetValueOrDefault());
            if (!monitorResult.Success)
            {
                await DialogService.ShowErrorMessageBox(monitorResult.Message);
                return;
            }
            Monitor = monitorResult.Object;
        }
    }
    public async Task SaveMonitor()
    {
        if (!ValidationModel(Monitor))
            return;
        if (!IsComputer)
            Monitor.Computer = null;
        Result monitorResult = _windowType switch
        {
            WindowType.Add => await _monitorService.Add(Monitor),
            _ => await _monitorService.Update(Monitor)
        };
        if (!monitorResult.Success)
        {
            await DialogService.ShowErrorMessageBox(monitorResult.Message);
            return;
        }
    }
    public async Task LoadComputer()
    {
        var computersResult = await _computerService.GetComputerNamesWithUser();
        Computers = computersResult;
    }

    #endregion
}