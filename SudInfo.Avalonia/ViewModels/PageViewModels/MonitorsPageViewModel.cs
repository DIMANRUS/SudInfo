namespace SudInfo.Avalonia.ViewModels.PageViewModels;
public class MonitorsPageViewModel : BaseRoutableViewModel
{
    #region Services
    private readonly IDialogService _dialogService;
    private readonly IMonitorsService _monitorsService;
    #endregion

    #region Commands
    public ICommand OpenAddMonitorWindow { get; private init; }
    public ICommand OpenEditMonitorWindow { get; private init; }
    public ICommand RefreshMonitors { get; private init; }
    public ICommand RemoveMonitor { get; private init; }
    public ICommand SearchBoxKeyUp { get; private init; }
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
    public MonitorsPageViewModel(INavigationService navigationService, IDialogService dialogService, IMonitorsService monitorsService)
    {
        #region Serives Initialization
        _dialogService = dialogService;
        _monitorsService = monitorsService;
        #endregion

        eventHandlerClosedWindowDialog = async (s, e) =>
        {
            await LoadMonitors();
        };

        #region Commands Realizations
        Initialized = ReactiveCommand.CreateFromTask(LoadMonitors);
        RefreshMonitors = ReactiveCommand.CreateFromTask(LoadMonitors);
        RemoveMonitor = ReactiveCommand.CreateFromTask(async (int id) =>
        {
            var dialogResult = await dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить монитор?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
            if (dialogResult == ButtonResult.No)
                return;
            var removeMonitorResult = await monitorsService.RemoveMonitorById(id);
            if (!removeMonitorResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления монитора! Ошибка: {removeMonitorResult.Message}", icon: Icon.Error);
                return;
            }
            await LoadMonitors();
        });
        OpenAddMonitorWindow = ReactiveCommand.Create(async () =>
        {
            await navigationService.ShowMonitorWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
        });
        OpenEditMonitorWindow = ReactiveCommand.Create(async (int id) =>
        {
            await navigationService.ShowMonitorWindowDialog(WindowType.Save, eventHandlerClosedWindowDialog, id);
        });
        SearchBoxKeyUp = ReactiveCommand.Create(() =>
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                Monitors = new(MonitorsFromDataBase);
                return;
            }
            Monitors = new(MonitorsFromDataBase.Where(x => x.Name.ToLower().Contains(SearchText.ToLower())));
        });
        #endregion
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
        Monitors = new(monitorsResult.Result);
        MonitorsFromDataBase = Monitors;
    }
    #endregion
}