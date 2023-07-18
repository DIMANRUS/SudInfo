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
    public IReadOnlyCollection<Rutoken>? Rutokens { get; set; }

    #endregion

    #region Properties

    [Reactive]
    public Rutoken? SelectedRutoken { get; set; }

    #endregion

    #region Ctors

    public RutokensPageViewModel(
        NavigationService navigationService,
        RutokenService rutokensService,
        DialogService dialogService)
    {
        #region Serives Initialization
        _dialogService = dialogService;
        _rutokensService = rutokensService;
        _navigationService = navigationService;
        #endregion

        _eventHandlerClosedWindowDialog = async (s, e) => await LoadRutokens();
    }

    #endregion

    #region Private Variables

    private readonly EventHandler _eventHandlerClosedWindowDialog;

    #endregion

    #region Public Methods

    public async Task OpenAddRutokenWindow()
    {
        await _navigationService.ShowRutokenWindowDialog(WindowType.Add, _eventHandlerClosedWindowDialog);
    }

    public async Task OpenEditRutokenWindow()
    {
        if (SelectedRutoken == null)
            return;
        await _navigationService.ShowRutokenWindowDialog(WindowType.Edit, _eventHandlerClosedWindowDialog, SelectedRutoken.Id);
    }

    public async Task RemoveRutoken()
    {
        if (SelectedRutoken == null)
            return;
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить рутокен?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removeRutokenResult = await RutokenService.RemoveRutokenById(SelectedRutoken.Id);
        if (!removeRutokenResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления рутокена! Ошибка: {removeRutokenResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadRutokens();
    }

    public async Task LoadRutokens()
    {
        Rutokens = await RutokenService.GetRutokens();
    }

    #endregion
}