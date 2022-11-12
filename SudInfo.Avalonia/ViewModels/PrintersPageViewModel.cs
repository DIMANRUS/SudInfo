namespace SudInfo.Avalonia.ViewModels;
public class PrintersPageViewModel : BaseViewModel, IRoutableViewModel
{
    #region IRoutableViewModel Realization
    public string UrlPathSegment => string.Empty;
    public IScreen HostScreen { get; }
    #endregion

    #region Commands
    public ICommand RefreshPrinters { get; private init; }
    public ICommand OpenPrinterWindow { get; private init; }
    #endregion

    #region Services
    private readonly IPrintersService _printerService;
    private readonly IDialogService _dialogService;
    #endregion

    #region Collections
    [Reactive]
    public ObservableCollection<Printer> Printers { get; set; }
    #endregion

    #region Constructors
    public PrintersPageViewModel(IPrintersService printersService, IDialogService dialogService, INavigationService navigationService)
    {
        #region Services Initialization
        _printerService = printersService;
        _dialogService = dialogService;
        #endregion

        #region Commands Initialization
        Initialized = ReactiveCommand.CreateFromTask(LoadPrinters);
        RefreshPrinters = ReactiveCommand.CreateFromTask(LoadPrinters);
        OpenPrinterWindow = ReactiveCommand.CreateFromTask(async () =>
            await navigationService.ShowPrinterWindowDialog(WindowType.Add));
        #endregion
    }
    public PrintersPageViewModel() { }
    #endregion

    #region Private Methods
    private async Task LoadPrinters()
    {
        var printersResult = await _printerService.GetPrinters();
        if (!printersResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! Ошибка: {printersResult.Message}", icon: Icon.Error);
            return;
        }
        Printers = new(printersResult.Result);
    }
    #endregion
}