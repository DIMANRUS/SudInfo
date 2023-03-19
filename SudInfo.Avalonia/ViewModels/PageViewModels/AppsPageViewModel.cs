namespace SudInfo.Avalonia.ViewModels.PageViewModels;
public class AppsPageViewModel : BaseRoutableViewModel
{
    private readonly AppService _appService;
    private readonly DialogService _dialogService;

    [Reactive] public int SelectedIndex { get; set; } = -1;

    [Reactive] public ObservableCollection<AppEntity> Apps { get; set; }
    public AppsPageViewModel(AppService appService, DialogService dialogService)
    {
        _appService = appService;
        _dialogService = dialogService;
        
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
        Apps = new(result.Object);
    }
}