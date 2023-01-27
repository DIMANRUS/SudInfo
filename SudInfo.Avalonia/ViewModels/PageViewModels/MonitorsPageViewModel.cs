namespace SudInfo.Avalonia.ViewModels.PageViewModels;
public class MonitorsPageViewModel : BaseRoutableViewModel
{
    #region Services
    private readonly IDialogService _dialogService;
    private readonly IMonitorService _monitorsService;
    private readonly INavigationService _navigationService;
    #endregion

    #region Collections
    [Reactive]
    public ObservableCollection<Monitor> Monitors { get; set; }
    private IEnumerable<Monitor> MonitorsFromDataBase { get; set; }
    #endregion

    #region Public properties
    [Reactive]
    public string SearchText { get; set; } = string.Empty;
    #endregion

    #region Constructors
    public MonitorsPageViewModel(INavigationService navigationService, IDialogService dialogService, IMonitorService monitorsService)
    {
        #region Serives Initialization
        _dialogService = dialogService;
        _monitorsService = monitorsService;
        _navigationService = navigationService;
        #endregion

        eventHandlerClosedWindowDialog = async (s, e) =>
        {
            await LoadMonitors();
        };
        Initialized = ReactiveCommand.CreateFromTask(LoadMonitors);
    }
    #endregion

    #region Public Methods
    public async Task OpenAddMonitorWindow()
    {
        await _navigationService.ShowMonitorWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
    }
    public async Task OpenEditMonitorWindow(int id)
    {
        await _navigationService.ShowMonitorWindowDialog(WindowType.Edit, eventHandlerClosedWindowDialog, id);
    }
    public async Task RefreshMonitors()
    {
        await LoadMonitors();
    }
    public async Task RemoveMonitor(int id)
    {
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить монитор?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removeMonitorResult = await _monitorsService.RemoveMonitorById(id);
        if (!removeMonitorResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления монитора! Ошибка: {removeMonitorResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadMonitors();
    }
    public void SearchBoxKeyUp()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            Monitors = new(MonitorsFromDataBase);
            return;
        }
        Monitors = new(MonitorsFromDataBase.Where(x => x.Name.ToLower().Contains(SearchText.ToLower())));
    }
    #endregion

    #region Private Methods
    private async Task LoadMonitors()
    {
        var monitorsResult = await _monitorsService.GetMonitors();
        if (!monitorsResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! Ошибка: {monitorsResult.Message}", icon: Icon.Error);
            return;
        }
        Monitors = new(monitorsResult.Object);
        MonitorsFromDataBase = Monitors;
    }
    #endregion
}