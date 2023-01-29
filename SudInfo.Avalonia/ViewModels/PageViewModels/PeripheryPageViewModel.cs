namespace SudInfo.Avalonia.ViewModels.PageViewModels;
public class PeripheryPageViewModel : BaseRoutableViewModel
{
    #region Services
    private readonly IPeripheryService _peripheryService;
    private readonly IDialogService _dialogService;
    private readonly INavigationService _navigationService;
    #endregion

    #region Collections
    [Reactive]
    public ObservableCollection<Periphery> Peripheries { get; set; }
    private IEnumerable<Periphery> PeripheriesFromDatabase { get; set; }
    #endregion

    #region Public properties
    [Reactive]
    public string SearchText { get; set; } = string.Empty;
    #endregion

    #region Constructors
    public PeripheryPageViewModel(IPeripheryService peripheryService, IDialogService dialogService, INavigationService navigationService)
    {
        #region Services Initialization
        _peripheryService = peripheryService;
        _dialogService = dialogService;
        _navigationService = navigationService;
        #endregion

        eventHandlerClosedWindowDialog = async (s, e) =>
        {
            await LoadPeripheries();
        };

        Initialized = ReactiveCommand.CreateFromTask(LoadPeripheries);
    }
    #endregion

    #region Public Methods
    public async Task RefreshPrinters()
    {
        await LoadPeripheries();
    }
    public async Task OpenAddPrinterWindow()
    {
        //await _navigationService.ShowPrinterWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
    }
    public async Task OpenEditPrinterWindow(int id)
    {
        //await _navigationService.ShowPrinterWindowDialog(WindowType.Edit, eventHandlerClosedWindowDialog, id);
    }
    public async Task RemovePrinter(int id)
    {
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить периферию?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removePeripheryResult = await _peripheryService.RemovePeripheryById(id);
        if (!removePeripheryResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления принтера! Ошибка: {removePeripheryResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadPeripheries();
    }
    public void SearchBoxKeyUp()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            Peripheries = new(PeripheriesFromDatabase);
            return;
        }
        Peripheries = new(PeripheriesFromDatabase.Where(x => x.Name.ToLower().Contains(SearchText.ToLower())));
    }
    #endregion

    #region Private Methods
    private async Task LoadPeripheries()
    {
        var peripheriesResult = await _peripheryService.GetPeripheryList();
        if (!peripheriesResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! Ошибка: {peripheriesResult.Message}", icon: Icon.Error);
            return;
        }
        Peripheries = new(peripheriesResult.Object);
        PeripheriesFromDatabase = Peripheries;
    }
    #endregion
}