namespace SudInfo.Avalonia.ViewModels.PageViewModels;
public class RutokensPageViewModel : BaseRoutableViewModel
{
    #region Services
    private readonly RutokenService _rutokensService;
    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;
    #endregion

    #region Collections
    [Reactive]
    public ObservableCollection<Rutoken> Rutokens { get; set; }
    #endregion

    #region Constructors
    public RutokensPageViewModel(NavigationService navigationService, RutokenService rutokensService, DialogService dialogService)
    {
        #region Serives Initialization
        _dialogService = dialogService;
        _rutokensService = rutokensService;
        _navigationService = navigationService;
        #endregion

        _eventHandlerClosedWindowDialog = async (s, e) =>
        {
            await LoadRutokens();
        };

        Initialized = ReactiveCommand.CreateFromTask(LoadRutokens);
    }
    #endregion

    #region Private Variables
    private EventHandler _eventHandlerClosedWindowDialog;
    #endregion

    #region Private Methods
    private async Task LoadRutokens()
    {
        var rutokensResult = await _rutokensService.GetRutokens();
        if (!rutokensResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! Ошибка: {rutokensResult.Message}", icon: Icon.Error);
            return;
        }
        Rutokens = new(rutokensResult.Object);
    }
    #endregion

    #region Public Methods
    public async Task OpenAddRutokenWindow()
    {
        await _navigationService.ShowRutokenWindowDialog(WindowType.Add, _eventHandlerClosedWindowDialog);
    }
    public async Task OpenEditRutokenWindow(int id)
    {
        await _navigationService.ShowRutokenWindowDialog(WindowType.Edit, _eventHandlerClosedWindowDialog, id);
    }
    public async Task RefreshRutokens()
    {
        await LoadRutokens();
    }
    public async Task RemoveRutoken(int id)
    {
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить рутокен?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removeRutokenResult = await _rutokensService.RemoveRutokenById(id);
        if (!removeRutokenResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления рутокена! Ошибка: {removeRutokenResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadRutokens();
    }
    #endregion
}