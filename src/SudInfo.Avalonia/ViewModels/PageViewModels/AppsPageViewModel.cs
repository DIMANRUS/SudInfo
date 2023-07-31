namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class AppsPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly AppService _appService;

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
        NavigationService navigationService)
    {
        #region Services initialization
        _appService = appService;

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
        Apps = await _appService.Get();
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
        var dialogResult = await DialogService.ShowQuestionMessageBox("Вы действительно хотите удалить приложение?");
        if (dialogResult == ButtonResult.No)
            return;
        var removeComputerResult = await _appService.Remove(SelectedApp.Id);
        if (!removeComputerResult.Success)
        {
            await DialogService.ShowErrorMessageBox(removeComputerResult.Message);
            return;
        }
        await LoadApps();
    }

    public async Task CreateExcelTable()
    {
        if (Apps == null || Apps.Count == 0)
            return;
        await ExcelService.CreateExcelTableFromEntity(Apps);
    }

    #endregion
}