namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class PrintersPageViewModel : BaseRoutableViewModel
{
    #region Services
    private readonly PrinterService _printersService;
    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;
    #endregion

    #region Collections

    [Reactive]
    public ObservableCollection<Printer>? Printers { get; set; }
    private IEnumerable<Printer>? PrintersFromDataBase { get; set; }

    #endregion

    #region Properties

    [Reactive]
    public string SearchText { get; set; } = string.Empty;

    [Reactive]
    public Printer? SelectedPrinter { get; set; }

    #endregion

    #region Initialization

    public PrintersPageViewModel(
        PrinterService printersService,
        DialogService dialogService,
        NavigationService navigationService)
    {
        #region Services Initialization
        _printersService = printersService;
        _dialogService = dialogService;
        _navigationService = navigationService;
        #endregion

        eventHandlerClosedWindowDialog = async (s, e) => await LoadPrinters();
    }

    #endregion

    #region Public Methods

    public async Task OpenAddPrinterWindow()
    {
        await _navigationService.ShowPrinterWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
    }

    public async Task OpenEditPrinterWindow()
    {
        if (SelectedPrinter == null)
            return;
        await _navigationService.ShowPrinterWindowDialog(WindowType.Edit, eventHandlerClosedWindowDialog, SelectedPrinter.Id);
    }

    public async Task RemovePrinter()
    {
        if (SelectedPrinter == null)
            return;
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить принтер?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removePrinterResult = await PrinterService.RemovePrinterById(SelectedPrinter.Id);
        if (!removePrinterResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления принтера! Ошибка: {removePrinterResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadPrinters();
    }

    public void SearchBoxKeyUp()
    {
        if (PrintersFromDataBase == null)
            return;
        if (string.IsNullOrEmpty(SearchText))
        {
            Printers = new(PrintersFromDataBase);
            return;
        }
        Printers = new(PrintersFromDataBase.Where(x => x.Name!.ToLower().Contains(SearchText.ToLower()) ||
                                                       x.InventarNumber!.Contains(SearchText) ||
                                                       x.SerialNumber!.Contains(SearchText) ||
                                                       x.Computer != null &&
                                                       x.Computer.User != null &&
                                                       x.Computer.User.FIO.ToLower().Contains(SearchText.ToLower())));
    }

    public async Task LoadPrinters()
    {
        var printersResult = await PrinterService.GetPrinters();
        Printers = new(printersResult);
        PrintersFromDataBase = Printers;
    }

    #endregion
}