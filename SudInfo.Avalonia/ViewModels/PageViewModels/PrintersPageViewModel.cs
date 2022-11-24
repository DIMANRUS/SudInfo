namespace SudInfo.Avalonia.ViewModels.PageViewModels;
public class PrintersPageViewModel : BaseRoutableViewModel
{
    #region Commands
    public ICommand RefreshPrinters { get; private init; }
    public ICommand OpenAddPrinterWindow { get; private init; }
    public ICommand OpenEditPrinterWindow { get; private init; }
    public ICommand RemovePrinter { get; private init; }
    #endregion

    #region Services
    private readonly IPrintersService _printerService;
    private readonly IDialogService _dialogService;
    #endregion

    #region Collections
    [Reactive]
    public ObservableCollection<Printer> Printers { get; set; }
    #endregion

    #region Private Variables
    private EventHandler eventHandlerClosedWindowDialog;
    #endregion

    #region Constructors
    public PrintersPageViewModel(IPrintersService printersService, IDialogService dialogService, INavigationService navigationService)
    {
        #region Services Initialization
        _printerService = printersService;
        _dialogService = dialogService;
        #endregion

        eventHandlerClosedWindowDialog = async (s, e) =>
        {
            await LoadPrinters();
        };

        #region Commands Initialization
        Initialized = ReactiveCommand.CreateFromTask(LoadPrinters);
        RefreshPrinters = ReactiveCommand.CreateFromTask(LoadPrinters);
        OpenAddPrinterWindow = ReactiveCommand.CreateFromTask(async () =>
            await navigationService.ShowPrinterWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog));
        OpenEditPrinterWindow = ReactiveCommand.Create(async (int id) =>
            await navigationService.ShowPrinterWindowDialog(WindowType.Save, eventHandlerClosedWindowDialog, id));
        RemovePrinter = ReactiveCommand.CreateFromTask(async (int id) =>
        {
            var dialogResult = await dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить принтер?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
            if (dialogResult == ButtonResult.No)
                return;
            var removePrinterResult = await printersService.RemovePrinterById(id);
            if (!removePrinterResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления принтера! Ошибка: {removePrinterResult.Message}", icon: Icon.Error);
                return;
            }
            await LoadPrinters();
        });
        #endregion
    }
    //public PrintersPageViewModel() { }
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