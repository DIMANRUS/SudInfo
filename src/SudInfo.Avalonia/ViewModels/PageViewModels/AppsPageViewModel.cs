namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class AppsPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly AppService _appService;
    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;

    #endregion

    #region Properties

    [Reactive]
    public int SelectedIndex { get; set; } = -1;

    public AppEntity? SelectedApp { get; set; }

    #endregion

    #region Collections

    [Reactive]
    public IReadOnlyCollection<AppEntity>? Apps { get; set; }

    #endregion

    #region Ctors

    public AppsPageViewModel(
        AppService appService,
        DialogService dialogService,
        NavigationService navigationService)
    {
        #region Services initialization
        _appService = appService;
        _dialogService = dialogService;
        _navigationService = navigationService;
        #endregion

        eventHandlerClosedWindowDialog = async (s, e) => await LoadApps();
    }

    #endregion

    #region Public Methods

    public void CloseRowDetail()
    {
        SelectedIndex = -1;
    }

    public async Task LoadApps()
    {
        Apps = await AppService.GetApps();
    }

    public async Task OpenAddAppWindow()
    {
        await _navigationService.ShowAppWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
    }

    public async Task OpenEditAppWindow()
    {
        if (SelectedApp == null)
            return;
        await _navigationService.ShowAppWindowDialog(WindowType.Edit, eventHandlerClosedWindowDialog, SelectedApp.Id);
    }

    public async Task RemoveApp()
    {
        if (SelectedApp == null)
            return;
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить приложение?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removeComputerResult = await AppService.RemoveApp(SelectedApp.Id);
        if (!removeComputerResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления приложения! Ошибка: {removeComputerResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadApps();
    }

    #endregion
}