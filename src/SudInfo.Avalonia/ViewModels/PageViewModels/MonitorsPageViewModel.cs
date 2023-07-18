namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class MonitorsPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly DialogService _dialogService;
    private readonly MonitorService _monitorsService;
    private readonly NavigationService _navigationService;

    #endregion

    #region Collections

    [Reactive]
    public IReadOnlyCollection<Monitor>? Monitors { get; set; }

    private IReadOnlyCollection<Monitor>? MonitorsFromDataBase { get; set; }

    #endregion

    #region Properties

    [Reactive]
    public string SearchText { get; set; } = string.Empty;

    [Reactive]
    public Monitor? SelectedMonitor { get; set; }

    #endregion

    #region Initialization

    public MonitorsPageViewModel(
        NavigationService navigationService,
        DialogService dialogService,
        MonitorService monitorsService)
    {
        #region Serives Initialization
        _dialogService = dialogService;
        _monitorsService = monitorsService;
        _navigationService = navigationService;
        #endregion

        eventHandlerClosedWindowDialog = async (s, e) => await LoadMonitors();
    }

    #endregion

    #region Public Methods

    public async Task OpenAddMonitorWindow()
    {
        await _navigationService.ShowMonitorWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
    }

    public async Task OpenEditMonitorWindow()
    {
        if (SelectedMonitor == null)
            return;
        await _navigationService.ShowMonitorWindowDialog(WindowType.Edit, eventHandlerClosedWindowDialog, SelectedMonitor.Id);
    }

    public async Task RemoveMonitor()
    {
        if (SelectedMonitor == null)
            return;
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить монитор?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removeMonitorResult = await MonitorService.RemoveMonitorById(SelectedMonitor.Id);
        if (!removeMonitorResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления монитора! Ошибка: {removeMonitorResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadMonitors();
    }

    public void SearchBoxKeyUp()
    {
        if (MonitorsFromDataBase == null)
            return;
        if (string.IsNullOrEmpty(SearchText))
        {
            Monitors = MonitorsFromDataBase;
            return;
        }
        Monitors = MonitorsFromDataBase.Where(x => x.Name!.ToLower().Contains(SearchText.ToLower()) ||
                                                    x.InventarNumber!.Contains(SearchText) ||
                                                    x.SerialNumber!.Contains(SearchText) ||
                                                    x.Computer != null &&
                                                    x.Computer.User != null &&
                                                    x.Computer.User.FIO.ToLower().Contains(SearchText.ToLower()))
                                       .ToList();
    }

    public async Task LoadMonitors()
    {
        Monitors = await MonitorService.GetMonitors();
        MonitorsFromDataBase = Monitors;
    }

    #endregion
}