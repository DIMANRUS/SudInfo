
namespace SudInfo.Avalonia.ViewModels.PageViewModels;
public class MonitorsPageViewModel : BaseRoutableViewModel
{
    #region Services
    private IDialogService _dialogService;
    #endregion

    #region Commands
    public ICommand OpenAddMonitorWindow { get; private init; }
    public ICommand OpenEditMonitorWindow { get; private init; }
    public ICommand RefreshMonitors { get; private init; }
    public ICommand RemoveMonitor { get; private init; }
    #endregion

    #region Collections
    [Reactive]
    public ObservableCollection<Monitor> Monitors { get; set; }
    #endregion

    #region Private Variables
    private EventHandler _eventHandlerClosedWindowDialog;
    #endregion

    #region Constructors
    public MonitorsPageViewModel(INavigationService navigationService, IDialogService dialogService)
    {
        #region Serives Initialization
        _dialogService = dialogService;
        #endregion

        _eventHandlerClosedWindowDialog = async (s, e) =>
        {
            await LoadMonitors();
        };
        Initialized = ReactiveCommand.CreateFromTask(LoadMonitors);
        RefreshMonitors = ReactiveCommand.CreateFromTask(LoadMonitors);
    }
    #endregion

    #region Private Methods
    private async Task LoadMonitors()
    {
    }
}