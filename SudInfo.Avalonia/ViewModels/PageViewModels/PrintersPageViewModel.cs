namespace SudInfo.Avalonia.ViewModels.PageViewModels;
public class PrintersPageViewModel : BaseRoutableViewModel
{
    #region Services
    private readonly IPrinterService _printersService;
    private readonly IDialogService _dialogService;
    private readonly INavigationService _navigationService;
    #endregion

    #region Collections
    [Reactive]
    public ObservableCollection<Printer> Printers { get; set; }
    private IEnumerable<Printer> PrintersFromDataBase { get; set; }
    #endregion

    #region Public properties
    [Reactive]
    public string SearchText { get; set; } = string.Empty;
    #endregion

    #region Constructors
    public PrintersPageViewModel(IPrinterService printersService, IDialogService dialogService, INavigationService navigationService)
    {
        #region Services Initialization
        _printersService = printersService;
        _dialogService = dialogService;
        _navigationService = navigationService;
        #endregion

        eventHandlerClosedWindowDialog = async (s, e) =>
        {
            await LoadPrinters();
        };

        Initialized = ReactiveCommand.CreateFromTask(LoadPrinters);
    }
    #endregion

    #region Public Methods
    public async Task RefreshPrinters()
    {
        await LoadPrinters();
    }
    public async Task OpenAddPrinterWindow()
    {
        await _navigationService.ShowPrinterWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
    }
    public async Task OpenEditPrinterWindow(int id)
    {
        await _navigationService.ShowPrinterWindowDialog(WindowType.Edit, eventHandlerClosedWindowDialog, id);
    }
    public async Task RemovePrinter(int id)
    {
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить принтер?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removePrinterResult = await _printersService.RemovePrinterById(id);
        if (!removePrinterResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления принтера! Ошибка: {removePrinterResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadPrinters();
    }
    public void SearchBoxKeyUp()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            Printers = new(PrintersFromDataBase);
            return;
        }
        Printers = new(PrintersFromDataBase.Where(x => x.Name.ToLower().Contains(SearchText.ToLower())));
    }
    #endregion

    #region Private Methods
    private async Task LoadPrinters()
    {
        var printersResult = await _printersService.GetPrinters();
        if (!printersResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! Ошибка: {printersResult.Message}", icon: Icon.Error);
            return;
        }
        Printers = new(printersResult.Object);
        PrintersFromDataBase = Printers;
    }
    #endregion
}