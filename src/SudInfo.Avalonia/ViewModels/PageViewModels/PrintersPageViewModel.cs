namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class PrintersPageViewModel : BaseRoutableViewModel
{
    #region Services
    private readonly PrinterService _printersService;

    private readonly NavigationService _navigationService;
    #endregion

    #region Collections

    [Reactive]
    public IReadOnlyCollection<Printer>? Printers { get; set; }

    private IReadOnlyCollection<Printer>? PrintersFromDataBase { get; set; }

    #endregion

    #region Properties

    [Reactive]
    public string SearchText { get; set; } = string.Empty;

    [Reactive]
    public Printer? SelectedPrinter { get; set; }

    #endregion

    #region Ctors

    public PrintersPageViewModel(
        PrinterService printersService,
        NavigationService navigationService)
    {
        #region Services Initialization

        _printersService = printersService;
        _navigationService = navigationService;

        #endregion

        eventHandlerClosedWindowDialog = async (s, e) => await LoadPrinters();
    }

    #endregion

    #region Public Methods

    public async Task CreateExcelTable()
    {
        if (Printers == null || Printers.Count == 0)
            return;
        await ExcelService.CreateExcelTableFromEntity(Printers);
    }

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
        var dialogResult = await DialogService.ShowQuestionMessageBox("Вы действительно хотите удалить принтер?");
        if (dialogResult == ButtonResult.No)
            return;
        var removePrinterResult = await _printersService.Remove(SelectedPrinter.Id);
        if (!removePrinterResult.Success)
        {
            await DialogService.ShowErrorMessageBox(removePrinterResult.Message);
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
            Printers = PrintersFromDataBase;
            return;
        }
        Printers = PrintersFromDataBase.Where(x => x.Name!.ToLower().Contains(SearchText.ToLower()) ||
                                                       x.InventarNumber!.Contains(SearchText) ||
                                                       x.SerialNumber!.Contains(SearchText) ||
                                                       x.Computer != null &&
                                                       x.Computer.User != null &&
                                                       x.Computer.User.FIO.ToLower().Contains(SearchText.ToLower())).ToList();
    }

    public async Task LoadPrinters()
    {
        Printers = await _printersService.Get();
        PrintersFromDataBase = Printers;
    }

    #endregion
}