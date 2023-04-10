namespace SudInfo.Avalonia.ViewModels.PageViewModels;
public class AppsPageViewModel : BaseRoutableViewModel
{
    #region Services
    private readonly AppService _appService;
    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;
    #endregion

    [Reactive]
    public int SelectedIndex { get; set; } = -1;

    [Reactive]
    public ObservableCollection<AppEntity>? Apps { get; set; }
    public AppsPageViewModel(AppService appService, DialogService dialogService, NavigationService navigationService)
    {
        _appService = appService;
        _dialogService = dialogService;
        _navigationService = navigationService;

        eventHandlerClosedWindowDialog = async (s,e) => await LoadApps();
    }

    public void CloseRowDetail()
    {
        SelectedIndex = -1;
    }
    public async Task LoadApps()
    {
        var result = await _appService.GetApps();
        if (!result.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка загрузки! Ошибка: {result.Message}", icon: Icon.Error);
            return;
        }
        Apps = new ObservableCollection<AppEntity>(result.Object);
    }

    public async Task OpenAddAppWindow()
    {
        await _navigationService.ShowAppWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
    }

    public async Task OpenEditAppWindow(int id)
    {
        await _navigationService.ShowAppWindowDialog(WindowType.Edit, eventHandlerClosedWindowDialog, id);
    }

    public async Task RemoveApp(int id)
    {
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить приложение?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removeComputerResult = await _appService.RemoveApp(id);
        if (!removeComputerResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления приложения! Ошибка: {removeComputerResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadApps();
    }
}